using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet
{
    public static class GameRenderer
    {
        public static ContentManager BaseContentManager { get; set; }
        public static RenderTarget2D BaseRenderTarget { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static Dimensions2D ScreenResolution => new Dimensions2D(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        public static Dimensions2D DisplayResolution => new Dimensions2D(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static Dimensions2D InternalResolution { get; private set; }
        public static List<Dimensions2D> AvailableScreenResolutions
        {
            get
            {
                var returnList = new List<Dimensions2D>();

                int numResolutions = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / InternalResolution.Width);

                

                for (int i = 1; i < numResolutions; i++)
                {
                    returnList.Add(InternalResolution * i);
                }

                return returnList;
            }
        }

        public static List<IRenderer2D> Renderers { get; set; }

        private static bool rendererConfigured = false;

        private static float Scale(IRenderer2D renderer)
        {
            return ScreenResolution.Width / renderer.TargetDimensions.Width;
        }

        public static void Configure(ContentManager content, GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, List<IRenderer2D> renderers)
        {
            BaseContentManager = content;
            Graphics = graphicsDeviceManager;
            GraphicsDevice = graphicsDevice;

            Renderers = renderers;

            foreach(var r in Renderers)
            {
                r.SpriteBatch = new SpriteBatch(GraphicsDevice);
            }

            rendererConfigured = true;
        }

        public static void Configure(Game game, GraphicsDeviceManager graphics, Dimensions2D internalResolution, List<IRenderer2D> renderers)
        {
            BaseContentManager = game.Content;
            GraphicsDevice = game.GraphicsDevice;
            Graphics = graphics;
            Renderers = renderers;
            InternalResolution = internalResolution;

            foreach (var r in Renderers)
            {


                r.SpriteBatch = new SpriteBatch(GraphicsDevice);
            }
        }

        public static void SetResolution(Dimensions2D dimensions)
        {
            Graphics.PreferredBackBufferWidth = (int)dimensions.Width;
            Graphics.PreferredBackBufferHeight = (int)dimensions.Height;

            Graphics.ApplyChanges();
        }

        public static Dimensions2D GetCurrentDisplayResolution()
        {
            return new Dimensions2D(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        }

        #region//XNA Methods

        public static void LoadContent()
        {
            BaseResources.LoadContent(BaseContentManager);
        }

        public static void UnloadContent()
        {
            
        }

        public static void Draw(SpriteBatch spriteBatch)
        {


            foreach (var renderer in Renderers)
            {
                GraphicsDevice.SetRenderTarget(renderer.RenderTarget);
                GraphicsDevice.Clear(Color.Black);
                renderer.DrawSpriteBatch();
                GraphicsDevice.SetRenderTarget(null);

                //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, null);
                //spriteBatch.Draw(renderer.RenderTarget, renderer.RenderPosition, null, Color.White, 0, Vector2.Zero, renderer.TargetScale, SpriteEffects.None, 0);
                //spriteBatch.End();
            }

            foreach(var renderer in Renderers)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, renderer.BlendState, SamplerState.PointWrap, null, null, null, null);
                spriteBatch.Draw(renderer.RenderTarget, renderer.RenderPosition, null, Color.White, 0, Vector2.Zero, renderer.TargetScale, SpriteEffects.None, 0);
                spriteBatch.End();
            }

        }

        #endregion
    }
}
