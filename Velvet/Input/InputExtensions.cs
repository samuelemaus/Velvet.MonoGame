using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Velvet.Input
{
    

    public static class InputExtensions
    {
        public static bool BtnDown(this IMouseHandler handler, MouseButtons button)
        {
            return handler.CurrentMouseState.IsButtonDown(button) && handler.PrevMouseState.IsButtonDown(button);
        }

        public static bool BtnReleased(this IMouseHandler handler, MouseButtons button)
        {
            return handler.CurrentMouseState.IsButtonUp(button) && handler.PrevMouseState.IsButtonDown(button);
        }

        public static bool BtnClicked(this IMouseHandler handler, MouseButtons button)
        {
            return handler.CurrentMouseState.IsButtonUp(button) && handler.PrevMouseState.IsButtonDown(button);
        }

        
    }
}
