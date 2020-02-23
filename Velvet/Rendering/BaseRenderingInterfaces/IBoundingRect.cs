using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet
{
    public interface IBoundingRect : IMovable, IDimensions2D
    {
        BoundingRect BoundingRect { get; }

    }
}
