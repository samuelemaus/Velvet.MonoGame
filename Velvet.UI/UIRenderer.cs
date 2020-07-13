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

    public class UIRenderer : Renderer2D
    {
        public UIRenderer()
        {

        }

        public UIRenderer(ContentManager content, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = content;
            RenderTarget = renderTarget;

            DrawMethod = DrawCurrentMenu;

            if(position == default)
            {
                RenderPosition = Vector2.Zero;
            }

            RenderPosition = position;
            RendererInitialized = true;

            GameRenderer.Instance.AddRenderer(this);

        }

        #region//Content
        public ContentManager Content { get; private set; }
        public SpriteFont DefaultFont { get; private set; }
        #endregion

        public override void LoadContent()
        {
            DefaultFont = UIResources.DefaultFont;
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public string DefaultFontDirectory = "Fonts";
        public void LoadSpriteFonts(IEnumerable<SpriteFont> fonts)
        {
            
        }

        public void DrawCurrentMenu()
        {
            SpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, null, RasterizerState, null, null);
            UIController.Instance.CurrentMenu.Draw(SpriteBatch);
            SpriteBatch.End();
        }

        public override void OnInternalResolutionChanged(object sender, EventArgs e)
        {
/*            base.OnInternalResolutionChanged(sender, e);
*/
        }

        public override string ToString()
        {
            return $"{nameof(UIRenderer)}:  {this.GetString()}";
        }

    }
}
