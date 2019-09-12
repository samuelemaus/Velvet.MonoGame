using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet
{
    public static class Renderer
    {
        public static ContentManager ContentManager { get; set; }
        public static RenderTarget2D RenderTarget { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static SpriteFont DefaultFont { get; set; }

        public static void LoadContent(ContentManager content, RenderTarget2D renderTarget)
        {
            
        }

        public static void LoadImageContent(IDrawableTexture drawableTexture)
        {
            
        }

        public static void LoadSpriteFonts(IEnumerable<SpriteFont> fonts)
        {
            
        }

        public static void UnloadContent()
        {
            
        }
    }
}
