using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{
    public class SceneTransition
    {
        #region//Content
        public virtual float TransitionSpeedMilliseconds { get; set; } = 650f;
        public TransitionContext Context { get; set; }
        protected virtual DrawDelegate InDrawMethod { get; set; }
        protected virtual DrawDelegate OutDrawMethod { get; set; }
        protected virtual IDrawableObject Image { get; set; }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Context == TransitionContext.Out)
            {
                OutDrawMethod.Invoke(spriteBatch);
            }

            else
            {
                InDrawMethod.Invoke(spriteBatch);
            }
        }

        public virtual void LoadContent(IRenderer2D renderer)
        {
            if(Image is IDrawableTexture)
            {
                renderer.LoadImageContent(Image as IDrawableTexture);
            }

            
        }

        #endregion

        #region//Constructors

        public SceneTransition()
        {

        }

        public SceneTransition(IDrawableObject image)
        {
            Image = image;
        }

        #endregion

        #region//Basic Transitions

        


        #endregion





    }

    public class FadeTransition : SceneTransition
    {
        public FadeTransition(Dimensions2D screenDimensions)
        {
            ScreenRectImage = new RectImage(new Rectangle(0, 0, (int)screenDimensions.Width, (int)screenDimensions.Height));


        }

        protected override IDrawableObject Image => ScreenRectImage;

        public RectImage ScreenRectImage;
        public float Opacity { get; set; }

        private void FadeInDrawMethod(SpriteBatch spriteBatch)
        {

        }

    }

    public class WipeTransition : SceneTransition
    {

    }
}
