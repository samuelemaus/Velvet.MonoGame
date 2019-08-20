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
    public class UIRenderer
    {

        #region//Content
        public ContentManager ContentManager { get; private set; }
        public RenderTarget2D RenderTarget;
        public SpriteFont DefaultFont;
        #endregion
        
        public void LoadContent(ContentManager content, RenderTarget2D renderTarget)
        {
            ContentManager = content;
            RenderTarget = renderTarget;

            DefaultFont = ContentManager.Load<SpriteFont>("Fonts/Consolas");

        }


        public void DrawString(string text, float fontScale, Vector2 position, Color color)
        {

        }

        public void DrawStrings(SpriteBatch spriteBatch, string[] texts, float fontScale, Vector2[] positions, Color[] colors)
        {

        }
        

    }
}
