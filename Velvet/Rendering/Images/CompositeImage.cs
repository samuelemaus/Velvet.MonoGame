using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet
{
    public class CompositeImage : IDrawableComposite
    {
        #region//Constructors

        public CompositeImage()
        {

        }

        public CompositeImage(IDrawableObject[] images, Vector2 position = default, Color color = default, Vector2 scale = default, float alpha = 0, float rotation = 0, float layerDepth = 0, SpriteEffects spriteEffect = default)
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
        public IDrawableObject[] Images { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public BoundingRect BoundingRect { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public float Alpha { get; set; }
        public float LayerDepth { get; set; }
        public Vector2 Scale { get; set; }
        public Dimensions2D Dimensions { get; set; }
        public Vector2 Origin { get; set; }

        public PositionDependency PositionDependency { get; set; }
        #endregion

        #region//Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var img in Images)
            {
                img.Draw(spriteBatch);
            }
        }



        public void SetWidth(float value)
        {
            throw new NotImplementedException();
        }

        public void SetHeight(float value)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
