using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet
{
    public class GameRenderer
    {

        #region//Content
        private Game Game { get; set; }
        public ContentManager BaseContentManager => Game.Content;
        public RenderTarget2D BaseRenderTarget { get; set; }

        private Vector2 renderPosition = Vector2.Zero;
        public Vector2 RenderPosition
        {
            get => renderPosition;

            set
            {
                renderPosition = value;
            }
        }
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager Graphics { get; private set; }
        public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;
        public List<IRenderer2D> Renderers { get; set; } = new List<IRenderer2D>();

        #region//Singleton
        private static GameRenderer instance;
        public static GameRenderer Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameRenderer();
                }

                return instance;
            }
        }
        #endregion

        #endregion

        #region//Resolutions
        public Dimensions2D OutputResolution => new Dimensions2D(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        public Dimensions2D DisplayResolution => new Dimensions2D(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        private Dimensions2D internalResolution;
        public Dimensions2D InternalResolution
        {
            get => internalResolution;
            set
            {
                internalResolution = value;
                OnInternalResolutionChanged();
            }
        }
        public List<Dimensions2D> AvailableScreenResolutions
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

        #endregion

        #region//Aspect Ratios
        public float InternalAspectRatio => InternalResolution.Width / InternalResolution.Height;
        public float DisplayAspectRatio => DisplayResolution.Width / DisplayResolution.Height;
        #endregion

        #region//Defaults
        public float DefaultAspectRatio { get; private set; }
        public Dimensions2D DefaultInternalResolution { get; private set; }
        #endregion

        #region//Initialization
        public void Initialize(Game game, GraphicsDeviceManager graphics, Dimensions2D internalResolution)
        {
            Game = game;
            Graphics = graphics;

            InternalResolution = internalResolution;

            InitializeDefaults(internalResolution);

            Graphics.HardwareModeSwitch = false;

            InitializeRenderers();

            BaseRenderTarget = new RenderTarget2D(GraphicsDevice, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

            Game.Window.ClientSizeChanged += OnClientSizeChanged;
        }

        private void InitializeDefaults(Dimensions2D internalResolution)
        {
            DefaultInternalResolution = internalResolution;
            DefaultAspectRatio = InternalAspectRatio;
        }
        private void InitializeRenderers()
        {
            if(Renderers != null)
            {
                foreach (var r in Renderers)
                {
                    r.SpriteBatch = new SpriteBatch(GraphicsDevice);
                    InternalResolutionChanged += r.OnInternalResolutionChanged;
                }
            }
        }
        #endregion

        #region//Public Methods

        public void AddRenderer(IRenderer2D renderer)
        {
            renderer.SpriteBatch = new SpriteBatch(GraphicsDevice);
            InternalResolutionChanged += renderer.OnInternalResolutionChanged;
            Renderers.Add(renderer);
        }

        public void ToggleFullScreen()
        {
            //bool buffer = ConstrainToAspectRatio;
            //ConstrainToAspectRatio = false;
            Graphics.ToggleFullScreen();
            //ConstrainToAspectRatio = buffer;

        }
        public void SetResolution(Dimensions2D dimensions)
        {
            Graphics.PreferredBackBufferWidth = (int)dimensions.Width;
            Graphics.PreferredBackBufferHeight = (int)dimensions.Height;
            BaseRenderTarget = new RenderTarget2D(GraphicsDevice, (int)dimensions.Width, (int)dimensions.Height);
            Graphics.ApplyChanges();
        }
        public void SetInternalResolution(Dimensions2D dimensions)
        {
            InternalResolution = dimensions;
        }

        #endregion

        #region//Events
        public event EventHandler<EventArgs> InternalResolutionChanged;
        private void OnInternalResolutionChanged()
        {
            InternalResolutionChanged?.Invoke(nameof(GameRenderer), EventArgs.Empty);
        }

        
        #endregion

        #region//Private Methods
        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (ConstrainToAspectRatio/* && !Graphics.IsFullScreen*/ && !GameWindowMatchesAspectRatio())
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

                if (Graphics.IsFullScreen)
                {
                    RenderPosition = GetFullScreenRenderPosition();
                }

            }

        }
        private bool GameWindowMatchesAspectRatio()
        {
            return InternalAspectRatio == DisplayAspectRatio;
        }
        private Vector2 GetFullScreenRenderPosition()
        {
            int x = 0;
            int y = 0;

            Dimensions2D difference = DisplayResolution - OutputResolution;

            //Display's aspect ratio is taller than internal
            if (InternalAspectRatio > DisplayAspectRatio)
            {
                y = (int)(difference.Height / 2);
                return new Vector2(x, y);
            }

            //Display's aspect ratio is wider than internal
            else if(InternalAspectRatio < DisplayAspectRatio)
            {
                x = (int)(difference.Width / 2);
                return new Vector2(x, y);
            }

            return Vector2.Zero;


        }
        #endregion

        #region//Public Settings

        /// <summary>
        /// If true, the Game's <see cref="InternalResolution"/> cannot run at an aspect ratio other than its default <see cref="InternalAspectRatio"/>.
        /// If running in Fullscreen, the Game will be letterboxed or pillarboxed if the Fullscreen aspect ratio is different than the internal aspect ratio.
        /// If running in Windowed mode, the <see cref="GameWindow"/>'s proportions will be constrained as well.
        /// </summary>
        public bool ConstrainToAspectRatio { get; set; } = true;

        #endregion

        #region//XNA Methods
        public void LoadContent()
        {
            BaseResources.LoadContent(BaseContentManager);
        }

        public void UnloadContent()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //Draw to individual Renderer's RenderTargets
            for (int i = 0; i < Renderers.Count; i++)
            {
                Renderers[i].DrawToRenderTarget(GraphicsDevice);
            }

            //Set BaseRenderTarget
            GraphicsDevice.SetRenderTarget(BaseRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);

            //Draw contents of individual Renderer's RenderTargets to BaseRenderTarget
            for (int i = 0; i < Renderers.Count; i++)
            {
                spriteBatch.Begin(Renderers[i].SpriteSortMode, Renderers[i].BlendState, Renderers[i].SamplerState, null, null, null, null);
                spriteBatch.Draw(Renderers[i].RenderTarget, Renderers[i].RenderPosition, null, Color.White, 0, Vector2.Zero, Renderers[i].TargetScale, SpriteEffects.None, 0);
                spriteBatch.End();
            }

            //Draw BaseRenderTarget
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, RasterizerState.CullNone, null, null);
            spriteBatch.Draw(BaseRenderTarget, RenderPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.End();

        }

        #endregion
    }
}
