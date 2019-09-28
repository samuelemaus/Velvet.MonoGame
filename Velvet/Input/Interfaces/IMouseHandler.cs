using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velvet.Input
{
    public interface IMouseHandler : IInputHandler
    {
        MouseStateExtended CurrentMouseState { get; }
        MouseStateExtended PrevMouseState { get; }

        Pointer Pointer { get; }
        
    }
}
