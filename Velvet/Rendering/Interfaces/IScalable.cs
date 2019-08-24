using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Rendering
{
    public interface IScalable
    {
        Vector2 Scale { get; }
        void SetScale(Vector2 scale);

    }
}
