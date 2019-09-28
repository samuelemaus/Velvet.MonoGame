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
        public UIRenderer()
        {

        }

        public UIRenderer(ContentManager content, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = content;
            RenderTarget = renderTarget;

            if(position == default)
            {
                RenderPosition = Vector2.Zero;
            }

            RenderPosition = position;

            Rectangle bounds = new Rectangle((int)position.X, (int)position.Y, renderTarget.Bounds.Width, renderTarget.Bounds.Height);
            Bounds = new BoundingRect(bounds);

            RendererInitialized = true;

        }

        public bool RendererInitialized { get; private set; }


        #region//Content
        public ContentManager Content { get; private set; }
        public RenderTarget2D RenderTarget { get; private set; }
        public SpriteBatch SpriteBatch { get; set; }
        public SpriteFont DefaultFont { get; private set; }
        public BlendState BlendState { get; set; } = BlendState.NonPremultiplied;

        public Vector2 RenderPosition { get; set; } = Vector2.Zero;
        public BoundingRect Bounds { get; private set; }
        public Dimensions2D TargetDimensions => new Dimensions2D(RenderTarget.Width, RenderTarget.Height);
        public float TargetScale => GameRenderer.ScreenResolution.Width / TargetDimensions.Width;

        #endregion

        public void LoadContent()
        {
            DefaultFont = UIResources.DefaultFont;
        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public string DefaultFontDirectory = "Fonts";
        public void LoadSpriteFonts(IEnumerable<SpriteFont> fonts)
        {
            
        }



        public void DrawSpriteBatch()
        {
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState, SamplerState.PointWrap, null, null, null, null);   
            UIController.CurrentMenu.Draw(SpriteBatch);
            SpriteBatch.End();
        }


        public override string ToString()
        {
            return $"{nameof(UIRenderer)}:  {this.GetString()}";
        }

    }
}
