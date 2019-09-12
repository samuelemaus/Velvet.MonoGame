using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet.GameSystems
{
    public class SceneRenderer : IRenderer2D
    {
        public ContentManager ContentManager { get; set; }

        public RenderTarget2D RenderTarget { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public SpriteFont DefaultFont { get; set; }

        public float TargetScale => throw new NotImplementedException();

        public Dimensions2D TargetDimensions => throw new NotImplementedException();

        public void LoadContent(Viewport viewport, ContentManager content, RenderTarget2D renderTarget)
        {
            throw new NotImplementedException();
        }

        public void LoadImageContent(IDrawableTexture drawableTexture)
        {
            throw new NotImplementedException();
        }

        public void LoadSpriteFonts(IEnumerable<SpriteFont> fonts)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
