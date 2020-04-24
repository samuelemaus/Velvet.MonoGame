using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet.GameSystems
{

    public abstract class GameObject : GameData, IBoundingRect
    {

        private BoundingRect boundingRect;
        public BoundingRect BoundingRect => boundingRect;


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
                boundingRect.CenterPosition = value;
            }
        }

        public PositionDependency PositionDependency { get; set; }

        protected Dimensions2D dimensions;
        public Dimensions2D Dimensions => dimensions;
        public virtual DimensionsDependency DimensionsDependency { get; set; }

        public virtual void SetWidth(float value)
        {
            dimensions.Width = value;
        }
        public virtual void SetHeight(float value) 
        {
            dimensions.Height = value;
        }

        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
