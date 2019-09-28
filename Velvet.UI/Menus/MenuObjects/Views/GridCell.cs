using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.UI
{
    public struct GridCell : IBoundingRect
    {
        public GridCell(BoundingRect rect, IViewObject content, PositionDependency dependency = null)
        {
            boundingRect = rect;
            Content = content;
            PositionDependency = dependency;

            position = rect.Position;
            Origin = Vector2.Zero;
        }

        public IViewObject Content;

        private BoundingRect boundingRect;
        public BoundingRect BoundingRect => boundingRect;

        public Vector2 Origin { get; set; }

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                boundingRect.Position = position;
                return position;
            }
            set
            {
                position = value;
                boundingRect.Position = position;
            }
        }
        public PositionDependency PositionDependency { get; set; }
        public ReferencePoint OriginReference { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
