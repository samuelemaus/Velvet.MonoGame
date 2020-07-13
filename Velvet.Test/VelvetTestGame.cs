using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velvet.UI;
using Velvet.GameSystems;
using Velvet.DataIO;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;

namespace Velvet
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class VelvetTestGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int internalWidth = 640;
        private int internalHeight = 360;
        private FrameRateCounter frameRateCounter;
        


        public VelvetTestGame()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any componentsf
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //this.Components.Add(new FrameRateCounter(this));
            this.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            this.IsMouseVisible = true;

            frameRateCounter = new FrameRateCounter(this);
            Components.Add(frameRateCounter);
            //graphics.SynchronizeWithVerticalRetrace = true;

            int hudWidth = internalWidth * 2;
            int hudHeight = internalHeight * 2;

            UIController.Instance.Initialize(Services, Content.RootDirectory, new RenderTarget2D(GraphicsDevice, hudWidth, hudHeight), Vector2.Zero);
            SceneController.Instance.Initialize(Services, "Content/Scenes", new RenderTarget2D(GraphicsDevice, internalWidth, internalHeight), Vector2.Zero);
            TileSetManager.Initialize(SceneController.Instance.Content, "Content/Scenes/Tilesets");
            GameRenderer.Instance.SetResolution(GameRenderer.Instance.DisplayResolution / 2f);


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

            GameScene DefaultScene = null;
            //var ZeldaScene = new ZeldaTestScene();


            SceneController.Instance.LoadContent(DefaultScene);
            SceneController.Instance.GameScenes.Add(DefaultScene);
            //if(DefaultScene is ZeldaTestScene z)
            //{
            //    z.GameSettingsMenu = new GameSettingsMenu(this);
            //}
            
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

            UIController.Instance.Update(gameTime);
            SceneController.Instance.Update(gameTime);
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
