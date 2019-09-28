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
            bool value = false;

            if (CurrentKeyState.IsKeyDown(key) && PrevKeyState.IsKeyUp(key))
            {
                value = true;
            }

            return value;


        }
        public bool KeyDown(Keys key)
        {
            bool value = false;

            if (CurrentKeyState.IsKeyDown(key))
            {
                value = true;
            }

            return value;
        }
        public bool KeyReleased(Keys key)
        {
            bool value = false;

            if (CurrentKeyState.IsKeyUp(key) && PrevKeyState.IsKeyDown(key))
            {
                value = true;
            }

            return value;


        }



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
