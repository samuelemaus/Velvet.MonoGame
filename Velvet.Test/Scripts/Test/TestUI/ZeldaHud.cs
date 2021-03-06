﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Velvet.UI;
//using Velvet.Input;
//using Velvet.Rendering;
//using Velvet.DataIO;
//using Velvet.GameSystems;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System.Threading;
//using C3.XNA;


//namespace Velvet
//{
//    public class ZeldaHud : Menu
//    {
//        public ZeldaHud(ZeldaTestScene zeldaTestScene)
//        {
//            scene = zeldaTestScene;
//        }

//        ZeldaTestScene scene;

//        public List<TextImage> ObjectDebugInfo = new List<TextImage>();

//        SpriteFont font;
//        public RectImage Background;

//        public override void LoadContent()
//        {
//            font = UIController.Instance.Renderer.DefaultFont;

//            Background = new RectImage(new BoundingRect(new Vector2(UIController.Instance.Renderer.Bounds.CenterPosition.X, 0), new Dimensions2D(UIController.Instance.Renderer.Bounds.Dimensions.Width, UIController.Instance.Renderer.Bounds.Dimensions.Height / 4)))
//            {
//                Color = Color.DarkBlue,
//                Alpha = 0.85f
//            };
//        }

//        public override void UnloadContent()
//        {
//            base.UnloadContent();
//        }

//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);
//            Background.Update(gameTime);
//            ActivateMenuControls(gameTime);
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            Background.Draw(spriteBatch);
//            DrawTileInfo(spriteBatch);
//        }

//        public void DrawTileInfo(SpriteBatch spriteBatch)
//        {
//            spriteBatch.DrawString(font, $"Link: {scene.Link.Image.BoundingRect.Bottom} | Circle: {scene.CirclePosition}", new Vector2(5, 5), Color.DeepPink);
//            spriteBatch.DrawString(font, $"Triangle: {scene.RightAngle}", new Vector2(5, 15), Color.DeepPink);
//            spriteBatch.DrawString(font, scene.World.Camera.DisplayRanges + " | " + scene.World.Camera.DisplayBounds + " | " + scene.World.Camera.ViewableArea, new Vector2(5, 25), Color.DeepPink);
//            spriteBatch.DrawString(font, $"sampler: {SceneController.Instance.Renderer.SamplerState}, blend: {SceneController.Instance.Renderer.BlendState}, rasterizer: {SceneController.Instance.Renderer.RasterizerState}, sort mode: {SceneController.Instance.Renderer.SpriteSortMode}", new Vector2(5,35),Color.Teal);
//        }

//    }
//}
