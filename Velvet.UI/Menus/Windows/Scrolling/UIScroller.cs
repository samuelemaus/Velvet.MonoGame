using System;
using System.Collections.Generic;
using System.Text;

using Velvet.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet.UI
{
    public class UIScroller
    {
        public bool IsScrolling { get; private set; }
        public ValueRange ScrollRange { get; protected set; }
        public float CurrentScrollValue { get; protected set; }
        public void SetCurrentScrollByPercentage(float pct)
        {
            CurrentScrollValue = (pct / ScrollRange.MaximumValue);
        }
        public void ToggleScrolling()
        {
            IsScrolling = !IsScrolling;
        }
        public void IncrementalScroll(Direction direction, float speed)
        {
            var moveVector = direction.ToVector2(speed);

            foreach(var obj in MovableObjects)
            {
                obj.Move(moveVector);
            }
        }
        public void ContinuousScroll(Direction direction, float speed)
        {
            if (IsScrolling)
            {
                IncrementalScroll(direction, speed);
            }
        }
        public void LiveScroll()
        {
            IsScrolling = true;



        }

        public IMovable[] MovableObjects;

    }
}
