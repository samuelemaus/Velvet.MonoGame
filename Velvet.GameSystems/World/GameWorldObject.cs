using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.GameSystems
{

    public abstract class GameWorldObject : GameData, IBoundingRect
    {

        private BoundingRect boundingRect;
        public BoundingRect BoundingRect => boundingRect;


        private Vector2 origin;
        public Vector2 Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
            }
        }


        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                boundingRect.Position = value;
            }
        }


        public PositionDependency PositionDependency { get; set; }
        public abstract Dimensions2D Dimensions { get; }
        public abstract DimensionsDependency DimensionsDependency { get; set; }

        public abstract void SetWidth(float value);
        public abstract void SetHeight(float value);
    }
}
