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
    public class Window : IBoundingRect
    {
        public Window()
        {

        }


        #region//Content

        public WindowBackground WindowBackground { get; private set; }

        private Viewport viewport;
        public Viewport Viewport => viewport;
        bool AddScrolling { get; }

        public BoundingRect BoundingRect => throw new NotImplementedException();

        public Vector2 Origin => throw new NotImplementedException();

        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public PositionDependency PositionDependency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Dimensions2D Dimensions => throw new NotImplementedException();

        public DimensionsDependency DimensionsDependency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void UpdateViewport()
        {

        }

        public void SetWidth(float value)
        {
            throw new NotImplementedException();
        }

        public void SetHeight(float value)
        {
            throw new NotImplementedException();
        }

        #endregion






    }
}
