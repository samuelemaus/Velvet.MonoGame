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
    public static class UIController
    {
        #region//Content
        public static ContentManager Content { get; private set; }
        public static UIRenderer Renderer { get; private set; }

        private static Menu currentMenu;
        public static Menu CurrentMenu { get { return currentMenu; }
            set
            {
                currentMenu = value;

                

            }
        }

        #endregion

        #region//Public Methods
        public static void Initialize(ContentManager content, Viewport viewport, RenderTarget2D renderTarget, Menu defaultMenu)
        {
            Content = content;

            UIResources.LoadContent(Content);

            Renderer = new UIRenderer();
            Renderer.LoadContent(viewport, content, renderTarget);


            

            CurrentMenu = defaultMenu;

            if(CurrentMenu != null)
            {
                CurrentMenu.LoadContent();
            }

        }
        public static void Update(GameTime gameTime)
        {
            CurrentMenu.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            CurrentMenu.Draw(spriteBatch);
        }
        #endregion






    }

}
