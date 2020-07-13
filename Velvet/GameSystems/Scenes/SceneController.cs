using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Xml.Linq;

namespace Velvet.GameSystems
{
    public class SceneController
    {

        #region//Singleton

        private static SceneController instance;
        public static SceneController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneController();
                }

                return instance;
            }
        }

        #endregion
        public void Initialize(IServiceProvider serviceProvider, string rootDirectory, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = new ContentManager(serviceProvider, rootDirectory);
            Renderer = new SceneRenderer(Content, renderTarget, position);
        }

        private GameScene currentScene;
        public GameScene CurrentScene
        {
            get { return currentScene; }
            private set
            {
                currentScene = value;
            }
        }

        private GameScene nextScene;
        public AudioEngine AudioEngine { get; set; }
        public ContentManager Content { get; set; }
        public SceneRenderer Renderer { get; set; }

        public bool SceneTransitioning { get; private set; }

        public void LoadContent(GameScene scene)
        {
            CurrentScene = scene;
            //if(CurrentScene is GameWorldScene gameWorldScene)
            //{
            //    Renderer.Camera = gameWorldScene.World.Camera;
            //}

            if(CurrentScene.Camera != null)
            {
                Renderer.Camera = CurrentScene.Camera;
            }

            else
            {
                Renderer.Camera = null;
            }

            CurrentScene.LoadContent();
        }

        private void Transition(GameScene next)
        {
            
            var previousScene = CurrentScene;
            LoadContent(next);
            if (previousScene != null)
            {
                previousScene.UnloadContent();
            }

            SceneTransitioning = false;
        }

        public void ChangeScene(GameScene next)
        {
            if (!SceneTransitioning)
            {
                SceneTransitioning = true;
                nextScene = next;
            }         
        }
        public void Update(GameTime gameTime)
        {
            if (SceneTransitioning)
            {
                Transition(nextScene);
            }
            CurrentScene.Update(gameTime);
        }

        public List<GameScene> GameScenes { get; set; } = new List<GameScene>();

        public int CurrentSceneIndex => GameScenes.IndexOf(CurrentScene);

        public void CheckSomething()
        {
            var list = GameScenes;
        }

    }
}
