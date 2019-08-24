using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet.Rendering
{
    public interface IColorable
    {
        Color Color { get; }
        void SetColor(Color color);
    }
}
