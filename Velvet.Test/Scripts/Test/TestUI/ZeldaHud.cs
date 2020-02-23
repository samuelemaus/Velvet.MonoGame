using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.UI;
using Velvet.Input;
using Velvet.Rendering;
using Velvet.DataIO;
using Velvet.GameSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using C3.XNA;


namespace Velvet
{
    public class ZeldaHud : Menu
    {
        public ZeldaHud(ZeldaTestScene zeldaTestScene)
        {
            scene = zeldaTestScene;
        }

        ZeldaTestScene scene;

        public List<TextImage> ObjectDebugInfo = new List<TextImage>();

        SpriteFont font;
        public RectImage Background;

        public override void LoadContent()
        {
            font = UIController.Renderer.DefaultFont;

            Background = new RectImage(UIController.Renderer.Bounds)
            {
                Color = Color.DarkBlue,
                Alpha = 0.725f
            };
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Background.Update(gameTime);
            ActivateMenuControls(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            DrawTileInfo(spriteBatch);
        }

        public void DrawTileInfo(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"RgnIdx: {scene.RegionIndex} | Tile: {scene.Tile}", new Vector2(5, 5), Color.DeepPink);
            spriteBatch.DrawString(font, $"Camera: {scene.Camera}", new Vector2(5, 15), Color.DeepPink);
            spriteBatch.DrawString(font, scene.Camera.DisplayRanges + " | " + scene.Camera.DisplayBounds + " | " + scene.Camera.ViewableArea, new Vector2(5, 25), Color.DeepPink);
        }

    }
}
