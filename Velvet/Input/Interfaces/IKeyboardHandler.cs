using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Velvet
{
    public interface IKeyboardHandler
    {
        KeyboardState CurrentKeyState { get; }
        KeyboardState PrevKeyState { get; }

        bool KeyPressed(Keys key);
        bool KeyDown(Keys key);
        bool KeyReleased(Keys key);




    }


}