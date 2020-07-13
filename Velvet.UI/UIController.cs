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
    public class UIController
    {


        #region//Content
        public ContentManager Content { get; private set; }
        public UIRenderer Renderer { get; private set; }

        private Menu currentMenu;
        public Menu CurrentMenu { get { return currentMenu; }
            set
            {
                currentMenu = value;

            }
        }

        public List<Menu> Menus = new List<Menu>();

        #region//Singleton

        private static UIController instance;

        public static UIController Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new UIController();
                }

                return instance;
            }
        }

        #endregion

        #endregion

        #region//Public Methods
        public void Initialize(IServiceProvider serviceProvider, string rootDirectory, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = new ContentManager(serviceProvider, rootDirectory);

            Renderer = new UIRenderer(Content, renderTarget, position);

        }

        public void ChangeMenu(Menu next)
        {
            if(next != currentMenu)
            {
                next.LoadContent();
                var previousMenu = CurrentMenu;
                CurrentMenu = next;
                if (previousMenu != null)
                {
                    previousMenu.UnloadContent();
                }
            }            
        }

        public void LoadContent()
        {
            UIResources.LoadContent(Content);

            Renderer.LoadContent();

        }

        public void Update(GameTime gameTime)
        {
            CurrentMenu.Update(gameTime);
        }

        #endregion






    }

}
