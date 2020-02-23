using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Velvet.Input;
using System.Xml.Linq;

namespace Velvet.GameSystems
{
    public abstract class GameScene : IContentManager
    {
        protected GameScene()
        {
            InitializeContent();
            InitializeCamera();
        }

        protected GameScene(string rootDirectory)
        {
            InitializeContent(rootDirectory);
            InitializeCamera();
        }

        public OrthoCamera Camera { get; set; }
        public GameEventController EventController { get; set; }
        public IRenderer2D SceneRenderer { get; set; }
        public PlaylistCollection AudioPlaylists { get; set; }
        public SceneTransition DefaultInTransition { get; set; }
        public SceneTransition DefaultOutTransition { get; set; }
        public InputManager Input { get; set; } = InputManager.CreateInputManager();
        public ContentManager Content { get; set; }
        public virtual string RootDirectory { get; set; }
        public virtual void ActivateSceneControls()
        {
            ChangeScenes();
            CheckSomething();
        }
        protected virtual void MoveObjectWithKey(IMovable movable, float speed)
        {
            if (Input.Keyboard.KeyDown(Keys.Up))
            {
                movable.MoveY(-speed);
            }

            if (Input.Keyboard.KeyDown(Keys.Down))
            {
                movable.MoveY(speed);
            }

            if (Input.Keyboard.KeyDown(Keys.Left))
            {
                movable.MoveX(-speed);
            }

            if (Input.Keyboard.KeyDown(Keys.Right))
            {
                movable.MoveX(speed);
            }

        }
        protected virtual void ChangeScenes()
        {
            if(Input.Keyboard.KeyDown(Keys.LeftControl) && Input.Keyboard.KeyPressed(Keys.Q))
            {
                int indexOfNext = ValueRange.Enforce(SceneController.CurrentSceneIndex + 1, new ValueRange(0, SceneController.GameScenes.Count - 1), true);

                SceneController.ChangeScene(SceneController.GameScenes[indexOfNext]);

            }

            if (Input.Keyboard.KeyDown(Keys.LeftControl) && Input.Keyboard.KeyPressed(Keys.A))
            {
                int indexOfPrevious = ValueRange.Enforce(SceneController.CurrentSceneIndex - 1, new ValueRange(0, SceneController.GameScenes.Count - 1), true);

                SceneController.ChangeScene(SceneController.GameScenes[indexOfPrevious]);

            }

        }
        protected virtual void CheckSomething()
        {
            if (Input.Keyboard.KeyPressed(Keys.N))
            {
                SceneController.CheckSomething();
            }
        }
        protected void InitializeContent(string rootDirectory = null)
        {
            if(rootDirectory != null)
            {
                RootDirectory = rootDirectory;
            }

            else
            {
                RootDirectory = GetDefaultRootDirectory();
            }

            Content = new ContentManager(SceneController.Content.ServiceProvider, RootDirectory);
        }
        protected void InitializeCamera()
        {
            Camera = new OrthoCamera(SceneController.Renderer.Viewport);
        }

        #region//Events



        #endregion
        #region//XNA Methods
        public virtual void LoadContent()
        {
            
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update(gameTime);
            ActivateSceneControls();
        }
        #endregion

        private string GetDefaultRootDirectory()
        {
            return "Content/Scenes" + "/" + this.GetType().Name;
        }

    }
}
