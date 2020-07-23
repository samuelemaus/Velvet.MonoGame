using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{
    public struct TileImage
    {

        private int id;
        private int x;
        private int y;
        private Vector2 position;
        private Rectangle sourceRect;
        private Dimensions2D dimensions;

        public int ID => id;
        public int X => x;
        public int Y => y;
        public Vector2 Position => position;
        public Rectangle SourceRect => sourceRect;
        public Dimensions2D Dimensions => dimensions;
        

        public TileImage(int _id, int _x, int _y, Vector2 _position, Rectangle _sourceRect)
        {
            id = _id;
            x = _x;
            y = _y;
            position = _position;
            sourceRect = _sourceRect;
            dimensions = sourceRect.ToDimensions2D();
            

        }
    }

    public struct TileImageRect
    {

        private int id;
        private int x;
        private int y;
        private Rectangle destinationRect;
        private Rectangle sourceRect;
        private Dimensions2D dimensions;

        public int ID => id;
        public int X => x;
        public int Y => y;
        public Rectangle DestinationRect => destinationRect;
        public Rectangle SourceRect => sourceRect;
        public Dimensions2D Dimensions => dimensions;


        public TileImageRect(int _id, int _x, int _y, Rectangle _destinationRect, Rectangle _sourceRect)
        {
            id = _id;
            x = _x;
            y = _y;
            destinationRect = _destinationRect;
            sourceRect = _sourceRect;
            dimensions = sourceRect.ToDimensions2D();


        }
    }
}
