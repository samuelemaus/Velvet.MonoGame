using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.UI
{
    public class ListView : ObjectArrangementView
    {

        #region//Constructors

        public ListView()
        {
            boundingRect = TargetRect;
            Initialize();
        }

        public ListView(IBoundingRect targetBoundingRectObject, Orientation _orientation, Alignment _alignment)
        {
            TargetRect = targetBoundingRectObject.BoundingRect;
            orientation = _orientation;
            alignment = _alignment;
        }

        public int Columns { get; private set; } = 1;

        private Orientation orientation = Orientation.Vertical;
        public Orientation Orientation
        {
            get => orientation;
            set
            {
                orientation = value;
                ArrangeObjects();
            }
        }

        private Alignment alignment = Alignment.TopLeft;
        public Alignment Alignment
        {
            get => alignment;
            set
            {
                alignment = value;
                ArrangeObjects();
            }
        }
        
        public override void ArrangeObjects()
        {
            
        }

        public override void Initialize()
        {
            
        }

        #endregion




    }
}
