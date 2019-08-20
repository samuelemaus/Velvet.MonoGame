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
    public class Window
    {
        #region//Content

        public virtual dynamic Content { get; set; }

        public RectImage WindowBackground;



        #endregion




        #region//Constructors

        /// <summary>
        /// Returns a single-Component Window positioned with XY coordinates.
        /// </summary>
        /// <param name="windowComponent">Window Component. List of items, Table, etc. </param>
        /// <param name="position">Vector2 XY coordinates for position on screen.</param>
        /// <param name="fontScale">Scale for font used in Window.  Will default to 1:1 if left null. </param>
        /// <returns></returns>
        public static Window CreateWindow(WindowComponent windowComponent, Vector2 position, Vector2 fontScale)
        {


            return new BasicWindow();
        }

        /// <summary>
        /// Returns a single-Component Window positioned using a PositionAnchor.
        /// </summary>
        /// <param name="windowComponent">Window Component. List of items, Table, etc. </param>
        /// <param name="positionAnchor">Anchors Window to a specific side of an object.</param>
        /// <param name="fontScale">Scale for font used in Window.  Will default to 1:1 if left null. </param>
        /// <returns></returns>
        public static Window CreateWindow(WindowComponent windowComponent, PositionAnchor positionAnchor, Vector2 fontScale)
        {


            return new BasicWindow();
        }

        public static Window CreateWindow(List<WindowComponent> windowComponents)
        {


            return new MultiComponentWindow();
        }



        protected Window()
        {
                
        }
        #endregion

        private sealed class BasicWindow : Window
        {
            private WindowComponent _content;

            public override dynamic Content { get { return _content; } }


        }

        private sealed class MultiComponentWindow : Window
        {

        }

    }
}
