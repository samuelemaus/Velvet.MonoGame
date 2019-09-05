using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public interface IRenderer2D
    {
        ContentManager ContentManager { get; }
        RenderTarget2D RenderTarget { get; }
        SpriteBatch SpriteBatch { get; }
        SpriteFont DefaultFont { get; }

        void LoadContent(ContentManager content, RenderTarget2D renderTarget);
        void UnloadContent();
        void LoadImageContent(IDrawableTexture drawableTexture);
        void LoadSpriteFonts(IEnumerable<SpriteFont> fonts);


    }
}
