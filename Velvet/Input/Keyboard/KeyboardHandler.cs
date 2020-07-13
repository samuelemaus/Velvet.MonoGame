using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet.Input
{
    public class KeyboardHandler : IKeyboardHandler
    {

        public KeyboardState CurrentKeyState { get; private set; }
        public KeyboardState PrevKeyState { get; private set; }

        #region//Key Booleans

        public bool KeyPressed(Keys key)
        {
            if (CurrentKeyState.IsKeyDown(key) && PrevKeyState.IsKeyUp(key))
            {
                return true;
            }

            return false;
        }
        public bool KeyDown(Keys key)
        {
            if (CurrentKeyState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }
        public bool KeyReleased(Keys key)
        {
            if (CurrentKeyState.IsKeyUp(key) && PrevKeyState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }

        //public bool KeyHeld(Keys key, double secondsHeld)
        //{

        //}

        #endregion


        public bool HandlerActive => TimeInactive >= ActiveThreshold;
        public double TimeInactive { get; protected set; }
        public double ActiveThreshold { get; set; } = 10000d;
        private void UpdateTimeInactive(double delta)
        {

            if (CurrentKeyState == PrevKeyState)
            {

                if (TimeInactive < ActiveThreshold)
                {
                    TimeInactive += delta;
                }
                    

            }

            else
            {
                TimeInactive = 0;
            }
           
        }
        public void Update(GameTime gameTime)
        {
            PrevKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();

            UpdateTimeInactive(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

      
    }
}
