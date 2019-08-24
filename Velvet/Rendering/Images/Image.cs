using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.Rendering
{
    public class Image : IDrawableObject, IDisposable
    {

        #region//Content
        protected SpriteEffects spriteEffect;
        protected Rectangle     boundingRect;
        protected Vector2       position;
        
        protected Color         color;
        protected float         rotation;
        protected float         alpha;
        protected Vector2       scale;


        public SpriteEffects SpriteEffect => spriteEffect;
        public Rectangle BoundingRect => boundingRect;
        public Vector2 Position => position;
        
        public Color Color => color;
        public float Rotation => rotation;
        public float Alpha => alpha;
        public Vector2 Scale => scale;


        #endregion

        

        public void SetAlpha(float alpha)
        {
            this.alpha = alpha;

        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public void Move(Vector2 position)
        {
            this.position += position;
        }

        public void SetScale(Vector2 scale)
        {
            this.scale = scale;
        }


        //Draw & Update
        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
