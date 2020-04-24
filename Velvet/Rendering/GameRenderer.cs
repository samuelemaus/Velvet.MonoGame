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
        private static Game Game { get; set; }
        public static ContentManager BaseContentManager => Game.Content;
        public static RenderTarget2D BaseRenderTarget { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static GraphicsDevice GraphicsDevice => Game.GraphicsDevice;
        public static Dimensions2D ScreenResolution => new Dimensions2D(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        public static Dimensions2D DisplayResolution => new Dimensions2D(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        public static Dimensions2D InternalResolution { get; private set; }
        public static float InternalAspectRatio => InternalResolution.Width / InternalResolution.Height;
        public static List<Dimensions2D> AvailableScreenResolutions
        {
            get
            {
                var returnList = new List<Dimensions2D>();

                int numResolutions = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / InternalResolution.Width);

                

                for (int i = 0; i < numResolutions; i++)
                {
                    returnList.Add(InternalResolution * (i + 1));
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

        #region//Public Methods
        public static void Initialize(Game game, GraphicsDeviceManager graphics, Dimensions2D internalResolution, List<IRenderer2D> renderers)
        {
            Game = game;
            //BaseContentManager = game.Content;
            //GraphicsDevice = game.GraphicsDevice;
            Graphics = graphics;
            Renderers = renderers;
            InternalResolution = internalResolution;
            Graphics.HardwareModeSwitch = false;

            foreach (var r in Renderers)
            {
                r.SpriteBatch = new SpriteBatch(GraphicsDevice);
                
            }

            Game.Window.ClientSizeChanged += OnClientSizeChanged;
        }
        public static void ToggleFullScreen()
        {
            bool buffer = ConstrainToAspectRatio;
            ConstrainToAspectRatio = false;
            Graphics.ToggleFullScreen();
            ConstrainToAspectRatio = buffer;

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
        #endregion

        #region//Private Methods
        private static void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (ConstrainToAspectRatio && !Graphics.IsFullScreen && !GameWindowMatchesAspectRatio())
            {
                bool widthSmaller = Game.Window.ClientBounds.Width < Game.Window.ClientBounds.Height;

                if (widthSmaller)
                {
                    float width = Game.Window.ClientBounds.Height * InternalAspectRatio;


                    SetResolution(new Dimensions2D(width, Game.Window.ClientBounds.Height));
                }

                else
                {
                    float height = Game.Window.ClientBounds.Width * (InternalResolution.Height / InternalResolution.Width);

                    SetResolution(new Dimensions2D(Game.Window.ClientBounds.Width, height));

                    
                }

            }
        }
        private static bool GameWindowMatchesAspectRatio()
        {
            float aspectRatio = Game.Window.ClientBounds.Width / Game.Window.ClientBounds.Height;

            return InternalAspectRatio == aspectRatio;
        }
        #endregion

        #region//Public Settings

        /// <summary>
        /// If true, the Game's <see cref="InternalResolution"/> cannot run at an aspect ratio other than its default <see cref="InternalAspectRatio"/>.
        /// If running in Fullscreen, the Game will be letterboxed or pillarboxed if the Fullscreen aspect ratio is different than the internal aspect ratio.
        /// If running in Windowed mode, the <see cref="GameWindow"/>'s proportions will be constrained as well.
        /// </summary>
        public static bool ConstrainToAspectRatio { get; set; } = true;

        #endregion

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
                GraphicsDevice.Clear(Color.Transparent);
                renderer.DrawSpriteBatch();
                GraphicsDevice.SetRenderTarget(null);
            }

            foreach(var renderer in Renderers)
            {
                spriteBatch.Begin(renderer.SpriteSortMode, renderer.BlendState, renderer.SamplerState, null, null, null, null);
                spriteBatch.Draw(renderer.RenderTarget, renderer.RenderPosition, null, Color.White,0, Vector2.Zero, renderer.TargetScale, SpriteEffects.None, 0);
                spriteBatch.End();
            }
        }

        #endregion
    }
}
