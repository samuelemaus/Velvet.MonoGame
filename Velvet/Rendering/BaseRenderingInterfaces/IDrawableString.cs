using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet
{
    public interface IDrawableString : IDrawableObject
    {
        string Text { get; }
        TextAlignment Alignment { get; }
        TextCase TextCase { get; }
        void SetText(string text);
        SpriteFont Font { get; }
        



    }
}
