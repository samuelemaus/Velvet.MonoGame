using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet.Input
{
    public class BasicInputHandler : IInputHandler
    {
        public KeyboardState CurrentKeyState { get; protected set; }
        public KeyboardState PrevKeyState { get; protected set; }

        public MouseStateExtended CurrentMouseState { get; set; }
        public MouseStateExtended PrevMouseState { get; set; }

        public float ScrollWheelMovementVertical => CurrentMouseState.ScrollWheelValue - PrevMouseState.ScrollWheelValue;

        public Vector2 GetMousePosition()
        {
            return CurrentMouseState.Position / 2;
        }


        public IPointer Pointer { get; set; }

        public void Update(GameTime gameTime)
        {
            PrevKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();

            PrevMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            
        }

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



    }
}
