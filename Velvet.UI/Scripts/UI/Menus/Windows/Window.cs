using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Velvet.Rendering;

namespace Velvet.UI
{
    public class Window
    {
        #region//Content

        public virtual dynamic Content { get; set; }

        public IDrawableTexture WindowBackground;



        #endregion




        #region//Constructors


        protected Window()
        {
                
        }
        #endregion

        private sealed class BasicWindow : Window
        {
            


        }

        private sealed class MultiComponentWindow : Window
        {

        }

    }
}
