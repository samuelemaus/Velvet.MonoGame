using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velvet;
using Velvet.FETest.FE.Systems.Scenes;
using Velvet.GameSystems;
using Velvet.UI;

namespace Velvet.FETest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FEGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int internalWidth = 576;
        private int internalHeight = 324;

        public FEGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;
            

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            int hudWidth = (int)(internalWidth * 2.5f);
            int hudHeight = (int)(internalHeight / 1.75f);


            graphics.SynchronizeWithVerticalRetrace = false;
            GameRenderer.Instance.Initialize(this, graphics, new Dimensions2D(internalWidth, internalHeight));

            SceneController.Instance.Initialize(Services, "Content", new RenderTarget2D(GraphicsDevice, internalWidth, internalHeight), Vector2.Zero);
            UIController.Instance.Initialize(Services, Content.RootDirectory, new RenderTarget2D(GraphicsDevice, hudWidth, hudHeight), Vector2.Zero);

            TileSetManager.Initialize(SceneController.Instance.Content, "Content/Maps");

            Dimensions2D resolution = GameRenderer.Instance.DisplayResolution / 2f;

            if(GameRenderer.Instance.DisplayAspectRatio != GameRenderer.Instance.InternalAspectRatio)
            {
                int newHeight = (int)(resolution.Width / GameRenderer.Instance.InternalAspectRatio);

                resolution = new Dimensions2D(resolution.Width, newHeight);
            }

            GameRenderer.Instance.SetResolution(resolution);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameRenderer.Instance.LoadContent();
            UIController.Instance.LoadContent();

            string tileMap = "fe7maps/Ch2931SandsofTime.tmx";

            MapScene DefaultScene = new MapScene(tileMap);
            TestMapSceneUI Menu = new TestMapSceneUI(DefaultScene);

            SceneController.Instance.LoadContent(DefaultScene);
            SceneController.Instance.GameScenes.Add(DefaultScene);

            UIController.Instance.ChangeMenu(Menu);


            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SceneController.Instance.Update(gameTime);
            UIController.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GameRenderer.Instance.Draw(spriteBatch);
        }
    }
}
