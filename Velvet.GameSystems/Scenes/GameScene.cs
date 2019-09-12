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
    public class GameScene
    {
        public GameEventController EventController { get; set; }

        

        public IRenderer2D SceneRenderer { get; set; }
        public PlaylistCollection Playlists { get; set; }

        public SceneTransition InTransition { get; set; }
        public SceneTransition OutTransition { get; set; }

        public void LoadContent()
        {
            //InTransition.LoadContent(SceneRenderer);
            //OutTransition.LoadContent(SceneRenderer);


        }

        public void UnloadContent()
        {
            SceneRenderer.ContentManager.Unload();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}
