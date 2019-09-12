using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velvet.Input;

namespace Velvet.UI
{
    public abstract class Menu
    {
        #region//Content

        public IMenuObject FocusedObject { get; protected set; }

        public IEnumerable<IMenuObject> MenuObjects { get; protected set; }

        public IEnumerable<IMenuObject> ActiveMenuObjects => from m in MenuObjects where m.ObjectActive select m;


        //TODO: pre-processor directive for VelvetInput
        public BasicInputHandler InputHandler = new BasicInputHandler();

        

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

        public virtual void ActivateMenuControls(GameTime gameTime)
        {

        }

        //TODO: pre-processor directive for VelvetInput
       





        #endregion

    }
}
