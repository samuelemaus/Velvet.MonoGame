using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Rendering
{
    public interface IOrthoCamera : IUpdate, IRotatable, IScalable
    {
        Matrix Transform { get; }
        Viewport Viewport { get; }
        Vector2 Center { get; }
        float Zoom { get; }
        ValueRange ZoomRange { get; }



    }
}
