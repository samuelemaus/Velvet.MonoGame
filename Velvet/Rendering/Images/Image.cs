using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public abstract class Image : IDrawableObject, IDisposable
    {

        #region//Content
        public SpriteEffects SpriteEffect { get; set; }

        private BoundingRect boundingRect;
        public virtual BoundingRect BoundingRect => boundingRect;

        private Dimensions2D dimensions;
        public virtual Dimensions2D Dimensions
        {
            get
            {
                boundingRect.Dimensions = dimensions;
                return dimensions;
            }

            set
            {
                dimensions = value * Scale;
                boundingRect.Dimensions = value * Scale;
            }
        }
        private Vector2 position;
        public virtual Vector2 Position
        {
            get
            {
                if (PositionDependency != null && PositionDependency.DependencyActive)
                {
                    PositionDependency.GetPositionOverride(ref position);
                }

                boundingRect.Position = position;
                return position;

            }

            set
            {
                position = value;
                boundingRect.Position = position;
            }
        }
        public virtual Vector2 Origin { get; set; }
        public virtual Color Color { get; set; }
        public float Rotation { get; set; }
        public float Alpha { get; set; } = 1f;
        public float LayerDepth { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;


        #endregion

        #region//Dimensions & Positioning

        protected abstract void InitializeDimensions();

        public virtual PositionDependency PositionDependency { get; set; }





        protected abstract void SetOrigin();

        #endregion

        #region//Interface Methods
       

        public void SetWidth(float value)
        {
            dimensions.Width = value;
        }

        public void SetHeight(float value)
        {
            dimensions.Height = value;
        }


        #endregion

        #region//Draw & Update
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawMethod.Invoke(spriteBatch);
        }

        protected abstract DrawDelegate DrawMethod { get; set; }

        

        public virtual void Update(GameTime gameTime)
        {
            CheckPosition();
        }

        private void CheckPosition()
        {
            Vector2 p = Position;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
