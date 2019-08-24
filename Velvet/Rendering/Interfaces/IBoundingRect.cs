using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.Rendering
{
    public interface IBoundingRect : IMovable
    {
        Rectangle BoundingRect { get; }

        

    }
}
