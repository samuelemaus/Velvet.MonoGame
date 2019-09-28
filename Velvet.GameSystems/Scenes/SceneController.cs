using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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
            set
            {
                currentScene = value;
            }
        }
        public static AudioEngine AudioEngine { get; set; }

        public static ContentManager Content { get; set; }
        public static SceneRenderer Renderer { get; set; }

        public static void LoadContent(GameScene scene)
        {
            CurrentScene = scene;
            Renderer.Camera = CurrentScene.Camera;

            CurrentScene.LoadContent();
        }

        private static void Transition(GameScene next)
        {
            
        }

        public static void ChangeScene(GameScene next)
        {

        }
        
        public static void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime);
        }



    }
}
