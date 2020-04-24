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
    public static class SceneController
    {
        public static void Initialize(IServiceProvider serviceProvider, string rootDirectory, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = new ContentManager(serviceProvider, rootDirectory);
            Renderer = new SceneRenderer(Content, renderTarget, position);
        }

        private static GameScene currentScene;
        public static GameScene CurrentScene
        {
            get { return currentScene; }
            private set
            {
                currentScene = value;
            }
        }

        private static GameScene nextScene;
        public static AudioEngine AudioEngine { get; set; }
        public static ContentManager Content { get; set; }
        public static SceneRenderer Renderer { get; set; }

        public static bool SceneTransitioning { get; private set; }

        public static void LoadContent(GameScene scene)
        {
            CurrentScene = scene;
            if(CurrentScene is GameWorldScene gameWorldScene)
            {
                Renderer.Camera = gameWorldScene.World.Camera;
            }

            else
            {
                Renderer.Camera = null;
            }

            CurrentScene.LoadContent();
        }

        private static void Transition(GameScene next)
        {
            
            var previousScene = CurrentScene;
            LoadContent(next);
            if (previousScene != null)
            {
                previousScene.UnloadContent();
            }

            SceneTransitioning = false;
        }

        public static void ChangeScene(GameScene next)
        {
            if (!SceneTransitioning)
            {
                SceneTransitioning = true;
                nextScene = next;
            }         
        }
        
        public static void Update(GameTime gameTime)
        {
            if (SceneTransitioning)
            {
                Transition(nextScene);
            }
            CurrentScene.Update(gameTime);
        }

        public static List<GameScene> GameScenes = new List<GameScene>();

        public static int CurrentSceneIndex => GameScenes.IndexOf(CurrentScene);

        public static void CheckSomething()
        {
            var list = GameScenes;
        }

    }
}
