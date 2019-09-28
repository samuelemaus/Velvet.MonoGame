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

namespace Velvet.GameSystems
{
    public class GameScene
    {
        public OrthoCamera Camera;
        public GameEventController EventController { get; set; }

        public IRenderer2D SceneRenderer { get; set; }
        public PlaylistCollection Playlists { get; set; }

        public SceneTransition InTransition { get; set; }
        public SceneTransition OutTransition { get; set; }


        public InputManager Input { get; set; } = InputManager.CreateInputManager();

        public virtual void ActivateSceneControls()
        {
            
        }

        protected virtual void MoveObject(IMovable movable, float speed)
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

        public virtual void LoadContent()
        {
            //InTransition.LoadContent(SceneRenderer);
            //OutTransition.LoadContent(SceneRenderer);


        }

        public virtual void UnloadContent()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update(gameTime);
            ActivateSceneControls();
        }

    }
}
