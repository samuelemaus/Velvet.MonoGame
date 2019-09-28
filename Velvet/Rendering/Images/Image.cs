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

        #region//
        public SpriteEffects SpriteEffect { get; set; }

        protected BoundingRect boundingRect;
        public virtual BoundingRect BoundingRect
        {
            get
            {
                if (DimensionsDependency != null && DimensionsDependency.DependencyActive)
                {
                    DimensionsDependency.GetDimensionsOverride(ref boundingRect.Dimensions, ref scale);
                }

                if(PositionDependency != null && PositionDependency.DependencyActive)
                {
                    PositionDependency.GetPositionOverride(ref boundingRect.Position);
                }

                return boundingRect;
            }
        }

        //protected Dimensions2D dimensions;
        public virtual Dimensions2D Dimensions
        {
            get => BoundingRect.Dimensions;
            set
            {
                boundingRect.Dimensions = value;
                InitializeOrigin();
            }
        }

        protected Vector2 position;
        public virtual Vector2 Position
        {
            get => BoundingRect.Position - OriginDifferential;

            set => boundingRect.Position = value - OriginDifferential;
        }



        private Vector2 origin;
        public virtual Vector2 Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
                
            }
        }
        public virtual Color Color { get; set; }
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

        public Vector2 OriginDifferential => Origin - BoundingRect.Dimensions.Center;
        public Vector2 DrawPosition => Position + OriginDifferential;

        protected abstract void InitializeDimensions();
        public virtual PositionDependency PositionDependency { get; set; }

        private DimensionsDependency dimensionsDependency;
        public virtual DimensionsDependency DimensionsDependency
        {
            get
            {
                return dimensionsDependency;
            }

            set
            {
                dimensionsDependency = value;
                InitializeOrigin();
            }
        }
        protected virtual void InitializeOrigin()
        {
            Origin = Dimensions.Center;
        }

        #endregion

        #region//Interface Methods
       

        public void SetWidth(float value)
        {
            boundingRect.Dimensions.Width = value;
        }

        public void SetHeight(float value)
        {
            boundingRect.Dimensions.Height = value;
        }


        #endregion

        #region//Draw & Update
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawMethod.Invoke(spriteBatch);
        }

        protected abstract DrawDelegate DrawMethod { get; set; }

        

        public virtual void Update(GameTime gameTime)
        {
            
        }

        protected virtual void UpdateBoundingRect()
        {
            //boundingRect.Position = Position - originDifferential;
            //boundingRect.Dimensions = Dimensions;

        }

        protected virtual Vector2 originDifferential
        {
            get
            {
                return Origin - Dimensions.Center;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region//Debug



        #endregion

    }
}
