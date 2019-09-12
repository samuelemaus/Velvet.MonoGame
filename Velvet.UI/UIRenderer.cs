using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Velvet.UI
{

    public class UIRenderer : IRenderer2D
    {

        #region//Content
        public ContentManager ContentManager { get; private set; }
        public RenderTarget2D RenderTarget { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public SpriteFont DefaultFont { get; private set; }
        public Dimensions2D TargetDimensions => new Dimensions2D(RenderTarget.Width, RenderTarget.Height);

        public Dimensions2D ScreenDimensions => new Dimensions2D(ScreenViewport.Width, ScreenViewport.Height);

        public Viewport ScreenViewport { get; private set; }
        public float TargetScale => ScreenDimensions.Width / TargetDimensions.Width;

        #endregion

        public void LoadContent(Viewport viewport, ContentManager content, RenderTarget2D renderTarget)
        {
            ContentManager = content;
            RenderTarget = renderTarget;
            ScreenViewport = viewport;
            DefaultFont = UIResources.DefaultFont;
        }

        public void UnloadContent()
        {
            ContentManager.Unload();
        }

        public void LoadImageContent(IDrawableTexture drawableTexture)
        {
            bool hasFilePath = drawableTexture.FilePath != "";

            if (hasFilePath)
            {
               drawableTexture.Texture = UIController.Content.Load<Texture2D>(drawableTexture.FilePath);
            }
        }

        public void LoadImageContentFromPath(IDrawableTexture drawableTexture, string filePath)
        {
            drawableTexture.Texture = UIController.Content.Load<Texture2D>(filePath);
        }

        public string DefaultFontDirectory = "Fonts";
        public void LoadSpriteFonts(IEnumerable<SpriteFont> fonts)
        {
            
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            UIController.CurrentMenu.Draw(spriteBatch);


        }


    }
}
