using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{
    public struct Tile
    {
        public int ID;
        public Vector2 Position;
        public Rectangle SourceRect;

        public Tile(int id, Vector2 position, Rectangle sourceRect)
        {
            ID = id;
            Position = position;
            SourceRect = sourceRect;
        }
    }
}
