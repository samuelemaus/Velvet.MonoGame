using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface IMovable
    {
        Vector2 Position { get; }

        void SetPosition(Vector2 position);

        void Move(Vector2 position);

    }
}
