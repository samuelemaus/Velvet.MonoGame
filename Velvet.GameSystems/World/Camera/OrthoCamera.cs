using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velvet.Rendering;

namespace Velvet.GameSystems
{
    public class OrthoCamera : ICamera
    {
        public OrthoCamera(Viewport view)
        {
            Viewport = view;
            viewableArea = new BoundingRect(Viewport.Bounds);
            UpdateMethod = BaseUpdate;
            Center = viewableArea.CenterPosition;
            ZoomTool = new CameraZoom(this);
            ScrollTool = new CameraScroller(this);
        }

        #region//Dimensions & Matrix
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(Center, 0));
            }
        }

        public Matrix TransformClamped
        {
            get
            {
                return Matrix.CreateTranslation((int)-Position.X, (int)-Position.Y, 0) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                   Matrix.CreateTranslation(new Vector3(Center, 0));
            }
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
           return Vector2.Transform(worldPosition, Transform);
        }
   
        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
           return Vector2.Transform(screenPosition, Matrix.Invert(Transform));
        }

        public Viewport Viewport { get; set; }
        public Vector2 Center { get; private set; }

        int width => (int)(Viewport.Width / Zoom);
        int height => (int)(Viewport.Height / Zoom);

        private BoundingRect viewableArea;
        public BoundingRect ViewableArea => viewableArea;
        private void UpdateViewableArea()
        {
            viewableArea.CenterPosition = this.Position;
            viewableArea.Dimensions.Width = width;
            viewableArea.Dimensions.Height = height;
        }

        private BoundingRect GetViewableArea()
        {
            return new BoundingRect(this.Position, new Dimensions2D(width, height));
        }

        public BoundingRect WorldBounds { get; set; }
        public Vector2 Scale { get; set; }
        #endregion

        #region//CenterPosition
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                if(PositionDependency != null && PositionDependency.DependencyActive)
                {
                    GetCameraPositionOverride(ref position, HorizontalRange, VerticalRange);
                }
                return position.Round();
            }

            set
            {
                position = new Vector2(ValueRange.Enforce(value.X, HorizontalRange), ValueRange.Enforce(value.Y, VerticalRange));
            }
        }
     

        public CameraScroller ScrollTool { get; set; }
        #endregion
        #region//Rotation
        private float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }

            protected set
            {
                rotation = ValueRange.Enforce(value, BaseExtensions.RotationRange, true);
            }
        }
        public void InstantRotation(float value)
        {
            Rotation += value;
        }

        public float ninetyDegreeInterval = (float)Math.PI * 2 / 4;

        public bool RotationIsNinetyDegreeInterval
        {
            get
            {
                return Rotation % ninetyDegreeInterval == 0;
            }
        }
        #endregion
        #region//Zooming
        private float zoom = 2f;
        public float Zoom { get { return zoom; }
             set
            {
                zoom = ValueRange.Enforce(value, ZoomRange);
            }
        }
        public ValueRange ZoomRange { get; set; } = new ValueRange(1f, 10f);
        public CameraZoom ZoomTool { get; set; }

        public bool ZoomIsInt => Zoom % 1 == 0;

        #endregion
        #region//Effects

        public List<CameraEffect> ActiveEffects = new List<CameraEffect>();

        #endregion

        private PositionDependency positionDependency;
        public PositionDependency PositionDependency
        {
            get
            {
                return positionDependency;
            }

            set
            {
                SetPositionDependencyWithInterrupt(value);     
            }
        }
        public void SetPositionDependencyWithInterrupt(PositionDependency value, bool scrollToValue = true)
        {
            if (scrollToValue)
            {
                value.DependencyActive = false;
                positionDependency = value;
                ScrollTool.InitiateScroll(value.PositionOverride, ScrollTool.DefaultSpeed);
            }

            else
            {
                positionDependency = value;
                positionDependency.DependencyActive = true;
            }

            
        }


        #region//Updating
        protected virtual UpdateMethod UpdateMethod { get; set; }

        public void Update(GameTime gameTime)
        {
            //UpdateMethod.Invoke(gameTime);
            BaseUpdate(gameTime);
        }

        protected void BaseUpdate(GameTime gameTime)
        {            
            if(ActiveEffects.Count != 0)
            {
                foreach (var effect in ActiveEffects)
                {
                    effect.Update(gameTime);
                }
            }

            UpdateViewableArea();

        }

        


        #endregion

        #region//PositionDependencyMethods

        float highestPosition =>      (WorldBounds.Top + ViewableArea.Dimensions.VerticalCenter);
        float lowestPosition =>       (WorldBounds.Bottom - ViewableArea.Dimensions.VerticalCenter);
        float leftMostPosition =>     (WorldBounds.Left + ViewableArea.Dimensions.HorizontalCenter);
        float rightMostPosition =>    (WorldBounds.Right - ViewableArea.Dimensions.HorizontalCenter);

        public ValueRange HorizontalRange => new ValueRange(leftMostPosition, rightMostPosition);
        public ValueRange VerticalRange => new ValueRange(highestPosition, lowestPosition);

        private void GetCameraPositionOverride(ref Vector2 position, ValueRange horizontalBoundary, ValueRange verticalBoundary)
        {
            float x = ValueRange.Enforce(PositionDependency.PositionOverride.X, horizontalBoundary);
            float y = ValueRange.Enforce(PositionDependency.PositionOverride.Y, verticalBoundary);

            switch (PositionDependency.OverrideType)
            {
                case PositionOverrideType.FullOverride:
                    position.X = x;
                    position.Y = y; break;
                case PositionOverrideType.XOverride: position.X = PositionDependency.PositionOverride.X; break;
                case PositionOverrideType.YOverride: position.Y = PositionDependency.PositionOverride.Y; break;
            }
        }

        #endregion

        public override string ToString()
        {
            return $"Pos: {Position}, Bounds: (({WorldBounds})), VP: (({Viewport}))";
        }

        public string DisplayRanges => $"HR(({HorizontalRange})) | VR(({VerticalRange}))";
        public string DisplayBounds => $"Top {highestPosition}, Bottom {lowestPosition} Left {leftMostPosition} Right {rightMostPosition}";
    }
}
