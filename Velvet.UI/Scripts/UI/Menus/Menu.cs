using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velvet.UI
{
    public class Menu
    {
        #region//Content

        public Window FocusedWindow { get; private set; }

        public List<Window> Windows = new List<Window>();

        //TODO: pre-processor directive for VelvetInput
        protected BasicInputHandler InputHandler = new BasicInputHandler();


        #endregion


        #region//XNA Methods

        public virtual void LoadContent()
        {
            
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            //TODO: pre-processor directive for VelvetInput
            InputHandler.Update(gameTime);

            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        #endregion

        #region//Base Menu Methods

        public virtual void InitializeMenu()
        {

        }

        public virtual void ActivateMenuControls()
        {

        }

        //TODO: pre-processor directive for VelvetInput
        protected sealed class BasicInputHandler
        {
            KeyboardState currentKeystate;
            KeyboardState prevKeystate;


            public void Update(GameTime gameTime)
            {
                prevKeystate = currentKeystate;
                currentKeystate = Keyboard.GetState();
            }

            #region//Key Booleans

            public bool KeyPressed(Keys key)
            {
                bool value = false;

                if (currentKeystate.IsKeyDown(key) && prevKeystate.IsKeyUp(key))
                {
                    value = true;
                }
                
                return value;


            }
            public bool KeyDown(Keys key)
            {
                bool value = false;

                if (currentKeystate.IsKeyDown(key))
                {
                    value = true;
                }
                
                return value;
            }
            public bool KeyReleased(Keys key)
            {
                bool value = false;

                if (currentKeystate.IsKeyUp(key) && prevKeystate.IsKeyDown(key))
                {
                    value = true;
                }
                
                return value;


            }

            #endregion

        }





        #endregion

    }
}
