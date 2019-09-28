using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet
{
    public interface IDrawableString : IDrawableObject
    {
        string Text { get; set;  }
        TextAlignment Alignment { get; }
        TextCase TextCase { get; }
        SpriteFont Font { get; }

        


    }
}
