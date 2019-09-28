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


        public VelvetTestGame()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //this.Components.Add(new FrameRateCounter(this));
            this.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            this.IsMouseVisible = true;

            

            int hudWidth = internalWidth * 2;
            int hudHeight = internalHeight / 3;

            UIController.Initialize(Services, Content.RootDirectory, new RenderTarget2D(GraphicsDevice, hudWidth, hudHeight), Vector2.Zero);
            
            SceneController.Initialize(Services, "Content/Scenes", new RenderTarget2D(GraphicsDevice, internalWidth, internalHeight/* - hudHeight*/), Vector2.Zero);

            GameRenderer.Configure(this, graphics, new Dimensions2D(internalWidth, internalHeight), new List<IRenderer2D>() { SceneController.Renderer, UIController.Renderer});
            GameRenderer.SetResolution(GameRenderer.DisplayResolution / 1.5f);


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

            GameRenderer.LoadContent();

            TestTopDownScene DefaultScene = new TestTopDownScene();

            SceneController.LoadContent(DefaultScene);
            UIController.LoadContent(new TestHud(this, DefaultScene));
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

            UIController.Update(gameTime);
            SceneController.Update(gameTime);
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GameRenderer.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }

}
