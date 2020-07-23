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
using Velvet.EntityComponentSystem;

namespace Velvet.GameSystems
{
    public abstract class GameScene : IContentManager, IDraw
    {
        protected GameScene()
        {

        }

        public OrthoCamera Camera { get; set; }
        public InputManager Input { get; set; } = InputManager.CreateInputManager();
        public ContentManager Content { get; set; }
        public virtual string RootDirectory { get; set; }
        public virtual void ActivateSceneControls(GameTime gameTime)
        {
            ChangeScenes();
        }
        protected virtual void MoveObjectWithKey(IMovable movable, float speed, GameTime gameTime)
        {
            float deltaSpeed = (float)(speed * gameTime.ElapsedGameTime.TotalSeconds);

            if (Input.Keyboard.KeyDown(Keys.Up))
            {
                movable.MoveY(-deltaSpeed);
            }

            if (Input.Keyboard.KeyDown(Keys.Down))
            {
                movable.MoveY(deltaSpeed);
            }

            if (Input.Keyboard.KeyDown(Keys.Left))
            {
                movable.MoveX(-deltaSpeed);
            }

            if (Input.Keyboard.KeyDown(Keys.Right))
            {
                movable.MoveX(deltaSpeed);
            }

        }
        protected virtual void ChangeScenes()
        {
            if(Input.Keyboard.KeyDown(Keys.LeftControl) && Input.Keyboard.KeyPressed(Keys.Q))
            {
                int indexOfNext = ValueRange.Enforce(SceneController.Instance.CurrentSceneIndex + 1, new ValueRange(0, SceneController.Instance.GameScenes.Count - 1), true);

                SceneController.Instance.ChangeScene(SceneController.Instance.GameScenes[indexOfNext]);

            }

            if (Input.Keyboard.KeyDown(Keys.LeftControl) && Input.Keyboard.KeyPressed(Keys.A))
            {
                int indexOfPrevious = ValueRange.Enforce(SceneController.Instance.CurrentSceneIndex - 1, new ValueRange(0, SceneController.Instance.GameScenes.Count - 1), true);

                SceneController.Instance.ChangeScene(SceneController.Instance.GameScenes[indexOfPrevious]);

            }

        }

        protected virtual void InitializeContent(string rootDirectory = null)
        {
            if(rootDirectory != null)
            {
                RootDirectory = rootDirectory;
            }

            else
            {
                RootDirectory = GetDefaultRootDirectory();
            }

            Content = new ContentManager(SceneController.Instance.Content.ServiceProvider, RootDirectory);
        }
        protected virtual void InitializeCamera()
        {
            Camera = new OrthoCamera(SceneController.Instance.Renderer.Viewport);
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
            ActivateSceneControls(gameTime);
        }
        #endregion

        private string GetDefaultRootDirectory()
        {
            return "Content/Scenes" + "/" + this.GetType().Name;
        }

    }
}
