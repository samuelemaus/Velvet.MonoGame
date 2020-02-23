using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.UI
{
    public class ListView : MenuView
    {

        #region//Constructors

        public ListView()
        {
            boundingRect = TargetRect;

        }

        #endregion

        #region//Content

        private BoundingRect TargetRect { get; set; }
        public List<IMenuObject> MenuObjects { get; set; }

        


        #endregion

        #region//Overrides
        #endregion

        #region//Settings
        private Dimensions2D boundariesOffset;

        /// <summary>
        /// The offset between the <see cref="BoundingRect"/> and <see cref="MenuObjects"/>
        /// </summary>
        public Dimensions2D BoundariesOffset
        {
            get
            {
                return boundariesOffset;
            }

            set
            {
                boundariesOffset = value;
            }
        }

        private Dimensions2D menuObjectOffset;
        /// <summary>
        /// The offset between each <see cref="IMenuObject"/>
        /// </summary>
        public Dimensions2D MenuObjectOffset
        {
            get
            {
                return menuObjectOffset;
            }

            set
            {
                menuObjectOffset = value;
            }
        }

        private ResizeViewBehavior resizeBehavior;
        public ResizeViewBehavior ResizeBehavior
        {
            get
            {
                return resizeBehavior;
            }

            set
            {
                resizeBehavior = value;
            }
        }



        #endregion
    }
}
