using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Velvet
{

    public delegate void TimeThresholdEventHandler(object sender, object _value, EventArgs e);

    public class Timer : GameData
    {

        #region//Constructors


        public Timer(float low, float high, Order type)
        {
            TimerType = type;
            TimeRange = new ValueRange(low, high);

            CurrentValue = initialTimeValue();

            

            //timeSpan = TimeSpan.FromSeconds(TimeRange.CurrentValue);

        }

        public Timer(float threshold, Order type)
        {
            TimerType = type;

        }

        #endregion

        #region//Content

        private float currentValue;
        public float CurrentValue
        {
            get
            {
                return currentValue;
            }

            set
            {
                //float returnValue = ValueRange.EnforceValueRange(value, TimeRange);
                SetField(ref currentValue, ValueRange.EnforceValueRange(value, TimeRange));
                TimeSpan = TimeSpan.FromSeconds(currentValue);

            }
        }
        
        private TimeSpan timeSpan;
        public TimeSpan TimeSpan
        {
            get
            {
                return timeSpan;
            }

            private set
            {
                SetField(ref timeSpan, value);
            }
        }



        private ValueRange timeRange;
        public ValueRange TimeRange
        {
            get
            {
                return timeRange;
            }

            set
            {
                SetField(ref timeRange, value);
            }
        }

      

        public Order TimerType;

        private float initialTimeValue()
        {
             if (TimerType == Order.Ascending)
             {
                 return TimeRange.MinimumValue;
             }
            
             else
            {
                return TimeRange.MaximumValue;
            }
        }



        private float targetElapsedTime
        {
            get
            {
                if (TimerType == Order.Ascending)
                {
                    return TimeRange.MaximumValue;
                }

                else
                {
                    return TimeRange.MinimumValue;
                }
            }
        }
        private int multiplier
        {
            get

            {
                if (TimerType == Order.Ascending)
                {
                    return 1;
                }

                else
                {
                    return -1;
                }

            }
        }
        public float TimeElapsed { get; private set; }

        #endregion

        #region//Time Logic & Events
        public bool TimerActive { get; private set; }

        public bool ResetOnTimerEnded { get; set; }

        public void Reset()
        {
            OnTimerReset();

            CurrentValue = initialTimeValue();

            StartTimer();

        }

        public void ResetToDefault()
        {
            CurrentValue = initialTimeValue();

            TimerActive = false;
        }
        public void StartTimer()
         {
            TimerActive = true;

            OnTimerStarted();

        }
        public void EndTimer()
        {
            TimerActive = false;
            OnTimerEnded();

            if (ResetOnTimerEnded)
            {
                Reset();
            }


        }



        protected virtual void OnTimerStarted()
        {
            TimerStarted?.Invoke(this, EventArgs.Empty);

        }
        protected virtual void OnTimerEnded()
        {
            TimerEnded?.Invoke(this, EventArgs.Empty);

        }
        protected virtual void OnTimerReset()
        {
            TimerReset?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnThresholdReached(float threshold)
        {

        }

        public event EventHandler TimerStarted;
        public event EventHandler TimerEnded;
        public event EventHandler TimerReset;

        


        #endregion

        #region//XNA Methods

        public void Update(GameTime gameTime)
        {
            if (TimerActive)
            {
                TimeElapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

                CurrentValue += (TimeElapsed * multiplier);

                if (CurrentValue == targetElapsedTime)
                {
                    EndTimer();
                }

            }
        }

        #endregion

      
    }
}
