using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Velvet.UI
{
    public class UIController2
    {
        #region//Content
        public static ContentManager Content { get; private set; }
        public static UIRenderer Renderer { get; private set; }

        private static Menu currentMenu;
        public static Menu CurrentMenu
        {
            get { return currentMenu; }
            set
            {
                currentMenu = value;

            }
        }

        public static List<Menu> Menus = new List<Menu>();

        #endregion

        #region//Public Methods
        public static void Initialize(IServiceProvider serviceProvider, string rootDirectory, RenderTarget2D renderTarget, Vector2 position = default)
        {
            Content = new ContentManager(serviceProvider, rootDirectory);

            Renderer = new UIRenderer(Content, renderTarget, position);

        }

        public static void ChangeMenu(Menu next)
        {
            if (next != currentMenu)
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

        public static void LoadContent()
        {
            UIResources.LoadContent(Content);

            Renderer.LoadContent();

        }

        public static void Update(GameTime gameTime)
        {
            CurrentMenu.Update(gameTime);
        }

        #endregion





        #region//Settings

        public static float HoverDelayInMilliseconds { get; set; } = 900f;



        #endregion


    }
}
