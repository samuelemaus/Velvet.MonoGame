using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;


namespace Velvet.UI
{
    
    public abstract class MenuView : IMenuObject
    {

        protected List<IMenuObject> MenuObjects { get; set; } = new List<IMenuObject>();

        #region//Dimensions & Positioning
        protected BoundingRect boundingRect;
        public BoundingRect BoundingRect => boundingRect;
        public virtual Dimensions2D Dimensions
        {
            get => BoundingRect.Dimensions;
            set
            {
                boundingRect.Dimensions = value;
            }
        }
        public virtual Vector2 Position
        {
            get => BoundingRect.CenterPosition;

            set => boundingRect.CenterPosition = value;
        }
        public PositionDependency PositionDependency { get; set; }
        public DimensionsDependency DimensionsDependency { get; set; }

        
        public bool IsActive { get; protected set; }
        public bool IsTargeted { get; protected set; }

        #endregion

        #region//MenuView Logic

        public bool IsVisible { get; protected set; }

        public abstract void Initialize();


        #endregion

        #region//Settings



        #endregion


    }
}
