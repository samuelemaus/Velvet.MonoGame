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
    public class SceneController
    {
        public SceneController()
        {

        }

        

        private GameScene currentScene;
        public GameScene CurrentScene
        {
            get { return currentScene; }
            set
            {
                currentScene = value;
            }
        }
        public AudioEngine AudioEngine { get; set; }


        protected void Transition(GameScene next)
        {
            
        }

        public void ChangeScene(GameScene next)
        {

        }
        
        

    }
}
