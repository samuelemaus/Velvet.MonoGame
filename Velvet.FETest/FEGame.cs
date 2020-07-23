using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Velvet;
using Velvet.FETest.FE.Systems.Scenes;
using Velvet.GameSystems;
using Velvet.UI;
using Velvet.DataIO;
using Newtonsoft.Json.Linq;
using System.IO.IsolatedStorage;

namespace Velvet.FETest
{

    public class FEGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int internalWidth = 864;
        private int internalHeight = 486;

        public FEGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            this.IsFixedTimeStep = false;
            graphics.PreferMultiSampling = false;
            //this.TargetElapsedTime = TimeSpan.FromSeconds((1/30f));

        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            int hudWidth = (int)(internalWidth * 1.5f);
            int hudHeight = (int)(internalHeight / 2.75f);

            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = false;

            GameRenderer.Instance.Initialize(this, graphics, new Dimensions2D(internalWidth, internalHeight));
            SceneController.Instance.Initialize(Services, "Content", new RenderTarget2D(GraphicsDevice, internalWidth, internalHeight), Vector2.Zero);
            UIController.Instance.Initialize(Services, Content.RootDirectory, new RenderTarget2D(GraphicsDevice, hudWidth, hudHeight), Vector2.Zero);

            TileSetManager.Initialize(SceneController.Instance.Content, "Content/Maps");

            Dimensions2D resolution = GameRenderer.Instance.DisplayResolution / 2;

            if(GameRenderer.Instance.DisplayAspectRatio != GameRenderer.Instance.InternalAspectRatio)
            {
                int newHeight = (int)(resolution.Width / GameRenderer.Instance.InternalAspectRatio);

                resolution = new Dimensions2D(resolution.Width, newHeight);
            }

            GameRenderer.Instance.SetOutputResolution(resolution);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameRenderer.Instance.LoadContent();
            UIController.Instance.LoadContent();

            string game = "fe7";
            string maps = "maps";
            string mapName = "Ch1920DragonsGate";
            string ext = ".tmx";

            string tileMap = $"{game}{maps}/{mapName}{ext}";

            MapScene DefaultScene = new MapScene(tileMap);
            TestMapSceneUI Menu = new TestMapSceneUI(DefaultScene);

            SceneController.Instance.LoadContent(DefaultScene);
            SceneController.Instance.GameScenes.Add(DefaultScene);

            UIController.Instance.ChangeMenu(Menu);

            
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SceneController.Instance.Update(gameTime);
            UIController.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameRenderer.Instance.Draw(spriteBatch);
        }
    }
}
