using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public abstract class Image : IDrawableObject, IDisposable
    {

        #region//Content
        public SpriteEffects SpriteEffect { get; set; }
        public virtual Color Color { get; set; } = Color.White;
        public float Rotation { get; set; }
        public float Alpha { get; set; } = 1f;
        public float LayerDepth { get; set; }

        private Vector2 scale = Vector2.One;
        public virtual Vector2 Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }


        #endregion

        #region//Dimensions & Positioning
        protected BoundingRect boundingRect;
        public virtual BoundingRect BoundingRect
        {
            get
            {
                if(PositionDependency!= null && PositionDependency.DependencyActive)
                {
                    PositionDependency.GetPositionOverride(ref boundingRect.CenterPosition);
                }

                if(DimensionsDependency != null && DimensionsDependency.DependencyActive)
                {
                    DimensionsDependency.GetDimensionsOverride(ref boundingRect.Dimensions);
                }

                return boundingRect;
            }

        }


        public virtual Vector2 Position
        {
            get => BoundingRect.CenterPosition - OriginDifferential;
            set => boundingRect.CenterPosition = value + OriginDifferential;
        }

        private Vector2 origin;
        public virtual Vector2 Origin
        {
            get => origin;

            set
            {
                origin = value;
                OriginDifferential = BoundingRect.Dimensions.Center - value;
            }
        }
        public Vector2 OriginDifferential { get; protected set; }
        

        protected abstract void InitializeDimensions();
        protected PositionDependency positionDependency;
        public virtual PositionDependency PositionDependency
        {
            get => positionDependency;
            set
            {
                positionDependency = value;
                
            }
        }

        protected DimensionsDependency dimensionsDependency;
        public virtual DimensionsDependency DimensionsDependency
        {
            get => dimensionsDependency;

            set
            {
                dimensionsDependency = value;
                InitializeOrigin();
            }
        }
        protected virtual void InitializeOrigin()
        {
            Origin = BoundingRect.Dimensions.Center;
        }

        #endregion

        #region//Draw
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawMethod.Invoke(spriteBatch);
        }

        protected abstract DrawDelegate DrawMethod { get; set; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region//Public Methods

        public void SetOriginByReferencePoint(Alignment alignment)
        {
            float x = 0;
            float y = 0;

            switch (alignment.X)
            {
                case HorizontalAlignment.Left: x = 0; break;
                case HorizontalAlignment.Center: x = BoundingRect.Dimensions.HorizontalCenter;  break;
                case HorizontalAlignment.Right: x = BoundingRect.Dimensions.Width;  break;
            }
            switch (alignment.Y)
            {
                case VerticalAlignment.Top: y = 0; break;
                case VerticalAlignment.Center: y = BoundingRect.Dimensions.VerticalCenter; break;
                case VerticalAlignment.Bottom: y = BoundingRect.Dimensions.Height; break;
            }

            Origin = new Vector2(x, y);
        }

        #endregion

        #region//Debug



        #endregion

    }
}
