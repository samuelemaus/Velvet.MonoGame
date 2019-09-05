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
        public SpriteEffects SpriteEffect { get; protected set; }

        private BoundingRect boundingRect;
        public virtual BoundingRect BoundingRect => boundingRect;

        private Dimensions2D dimensions;
        public virtual Dimensions2D Dimensions => dimensions;
        public virtual Vector2 Position { get; protected set; }
        public virtual Vector2 Origin { get; protected set; }
        public virtual Color Color { get; protected set; }
        public float Rotation { get; protected set; }
        public float Alpha { get; protected set; } = 1f;
        public float LayerDepth { get; set; }
        public Vector2 Scale { get; protected set; } = Vector2.One;


        #endregion

        #region//Dimensions & Positioning

        protected abstract void InitializeDimensions();
        protected abstract void SetOrigin();

        #endregion

        #region//Interface Methods
        public void SetAlpha(float alpha)
        {
            Alpha = alpha;

        }

        public void SetColor(Color color)
        {
            Color = color;
        }

        public virtual void SetPosition(Vector2 position)
        {
            Position = position;
            boundingRect.Position = position;
        }

        public virtual void SetDimensions(Dimensions2D value)
        {
            dimensions = value * Scale;
            boundingRect.Dimensions = value * Scale;
        }

        public void SetWidth(float value)
        {
            dimensions.Width = value;
        }

        public void SetHeight(float value)
        {
            dimensions.Height = value;
        }

        public void Move(Vector2 position)
        {
            Position += position;
            boundingRect.Position += position;
        }

        public void SetScale(Vector2 scale)
        {
            Scale = scale;
        }

        public void UpdateBoundingRect()
        {
            boundingRect.Position = Position;
            boundingRect.Dimensions = Dimensions;
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
