using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public abstract class ObjectArrangementView : MenuView
    {
        #region//Content

        private BoundingRect targetRect;
        protected BoundingRect TargetRect
        {
            get
            {
                return targetRect;
            }

            set
            {
                targetRect = value;
            }
        }

        #endregion

        #region//Overrides
        #endregion

        #region//Settings
        private Dimensions2D boundariesOffset = new Dimensions2D(5,5);

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

        private Dimensions2D menuObjectOffset = new Dimensions2D(5,5);
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

        /// <summary>
        /// If true, the offset between each <see cref="IMenuObject"/> is evenly spaced based on the total area of the <see cref="TargetRect"/>
        /// </summary>
        public bool SetMenuObjectOffsetByTargetRectArea { get; set; }

        #endregion

        public abstract void ArrangeObjects();
        



    }
}
