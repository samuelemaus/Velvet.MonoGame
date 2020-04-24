using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace Velvet.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class Window : IMenuObject
    {
        public Window()
        {

        }

        public Window(BoundingRect _boundingRect)
        {
            boundingRect = _boundingRect;
            WindowBackground = new WindowBackground(this);
        }

        public Window(List<MenuView> menuViews)
        {
            MenuViews = menuViews;
        }

        public Window(MenuView menuView)
        {
            MenuViews.Add(menuView);
        }


        #region//Content
        private List<MenuView> menuViews = new List<MenuView>();
        protected List<MenuView> MenuViews
        {
            get => menuViews;
            set
            {
                menuViews = value;

                foreach(var menuView in MenuViews)
                {
                    menuView.Initialize();
                }
            }
        }
        public WindowBackground WindowBackground { get; private set; }

        private Viewport viewport;
        public Viewport Viewport => viewport;

        protected BoundingRect boundingRect;
        public BoundingRect BoundingRect => boundingRect;

        public Vector2 Position { get; set; }
        public PositionDependency PositionDependency {get; set;}

        public bool IsActive { get; set; }
        public bool IsTargeted { get; set; }
        public DimensionsDependency DimensionsDependency { get; set; }

        #endregion


    }
}
