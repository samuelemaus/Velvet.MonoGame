using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet
{
    public class CompositeImage2D : IDrawableComposite
    {
        #region//Constructors

        public CompositeImage2D()
        {

        }

        public CompositeImage2D(IDrawableObject[] images, Vector2 position = default, Color color = default, Vector2 scale = default, float alpha = 0, float rotation = 0, float layerDepth = 0, SpriteEffects spriteEffect = default)
        {

            Images = images;

            Position = position;

            Color = color;

            Scale = scale;

            Alpha = alpha;

            Rotation = rotation;

            LayerDepth = layerDepth;

            SpriteEffect = spriteEffect;

        }

        #endregion

        #region//Fields & Properties
        public IDrawableObject[] Images { get; protected set; }
        public SpriteEffects SpriteEffect { get; protected set; }
        public BoundingRect BoundingRect { get; protected set; }
        public Vector2 Position { get; protected set; }
        public Color Color { get; protected set; }
        public float Rotation { get; protected set; }
        public float Alpha { get; protected set; }
        public float LayerDepth { get; protected set; }
        public Vector2 Scale { get; protected set; }
        public Dimensions2D Dimensions { get; protected set; }

        public Vector2 Origin => throw new NotImplementedException();
        #endregion

        #region//Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var img in Images)
            {
                img.Draw(spriteBatch);
            }
        }

        public void Move(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public void SetAlpha(float alpha)
        {
            throw new NotImplementedException();
        }

        public void SetColor(Color color)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public void SetDimensions(Dimensions2D value)
        {

        }

        public void SetScale(Vector2 scale)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void UpdateBoundingRect()
        {
            throw new NotImplementedException();
        }

        public void SetWidth(float value)
        {
            throw new NotImplementedException();
        }

        public void SetHeight(float value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
