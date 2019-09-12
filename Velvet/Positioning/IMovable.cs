using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface IMovable
    {
        Vector2 Position { get; set;  }
        PositionDependency PositionDependency { get; set; }

    }
}
