using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.UI
{
    public struct GridCell : IBoundingRect
    {
        public GridCell(BoundingRect rect, IBindableViewObject content, PositionDependency dependency = null)
        {
            boundingRect = rect;
            Content = content;
            PositionDependency = dependency;

            position = rect.CenterPosition;
            Origin = Vector2.Zero;
        }

        public IBindableViewObject Content;

        private BoundingRect boundingRect;
        public BoundingRect BoundingRect => boundingRect;

        public Vector2 Origin { get; set; }

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                boundingRect.CenterPosition = position;
                return position;
            }
            set
            {
                position = value;
                boundingRect.CenterPosition = position;
            }
        }
        public PositionDependency PositionDependency { get; set; }
        public Alignment OriginReference { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Dimensions2D Dimensions => throw new NotImplementedException();

        public DimensionsDependency DimensionsDependency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private float percentageFilled()
        {
            return default;
        }

        public void SetWidth(float value)
        {
            throw new NotImplementedException();
        }

        public void SetHeight(float value)
        {
            throw new NotImplementedException();
        }
    }
}
