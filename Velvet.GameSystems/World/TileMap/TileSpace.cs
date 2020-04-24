using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{
    public struct TileSpace
    {
        private int x;
        private int y;
        private BoundingRect boundingRect;

        public int X => x;
        public int Y => y;
        public BoundingRect BoundingRect => boundingRect;

        public TileSpace(int _x, int _y, BoundingRect _boundingRect)
        {
            x = _x;
            y = _y;
            boundingRect = _boundingRect;
        }
        
    }
}
