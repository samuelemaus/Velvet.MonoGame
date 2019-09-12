using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet.Input
{
    public struct MouseButton
    {
        public MouseButton(MouseButtons button, ButtonState state)
        {
            Button = button;
            State = state;
        }

        public MouseButtons Button;
        public ButtonState State;



    }
}
