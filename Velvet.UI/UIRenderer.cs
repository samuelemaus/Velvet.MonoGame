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

        #endregion
        
        public void LoadContent(ContentManager content, RenderTarget2D renderTarget)
        {
            ContentManager = content;
            RenderTarget = renderTarget;

            DefaultFont = ContentManager.Load<SpriteFont>("Fonts/Consolas");
            RectImage.DefaultRectImageTexture = ContentManager.Load<Texture2D>(RectImage.DefaultPath);

        }

        public void UnloadContent()
        {
            ContentManager.Unload();
        }

        public void LoadImageContent(IDrawableTexture drawableTexture)
        {
            bool hasTexture = drawableTexture.Texture != null;

            if (hasTexture)
            {
               drawableTexture.SetTexture(ContentManager.Load<Texture2D>(drawableTexture.FilePath));
            }
        }

        public string DefaultFontDirectory = "Fonts";
        public void LoadSpriteFonts(IEnumerable<SpriteFont> fonts)
        {
            
        }

        public void DrawString(string text, float fontScale, Vector2 position, Color color)
        {

        }

        public void DrawStrings(SpriteBatch spriteBatch, string[] texts, float fontScale, Vector2[] positions, Color[] colors)
        {

        }


    }
}
