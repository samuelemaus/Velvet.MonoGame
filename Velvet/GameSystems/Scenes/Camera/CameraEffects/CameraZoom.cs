using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.GameSystems
{
    
    public class CameraZoom : CameraEffect
    {

        public CameraZoom(OrthoCamera camera)
        {
            Camera = camera;
            UpdateMethod = BaseUpdate;
            EffectActive = true;
        }

        private bool gradualZoomInitialized = false;
        private double zoomTimeElapsed = 0;
        private float initialZoom;
        private float targetZoomValue;
        private float timeinMilliseconds;
        private float acceleration;
        public bool Zooming { get; private set; } = false;



        #region//Public Settings

        private bool snapToZoom;
        public bool SnapToZoom
        {
            get
            {
                return snapToZoom;
            }

            set
            {
                snapToZoom = value;

                if(value == true)
                {
                    UpdateMethod = UpdateSnapToZoom;
                }
            }
        }

        private float snapTolerance = 0.25f;
        public float SnapTolerance { get; set; } = 0.25f;


        

        #endregion


        #region//Public Methods
        public void SetGradualZoom(float _targetZoomValue, float _timeInMilliseconds = 500f, float _acceleration = 0)
        {
            targetZoomValue = _targetZoomValue;
            timeinMilliseconds = _timeInMilliseconds;
            acceleration = _acceleration;
            Zooming = true;
        }
        public void IncrementZoom(float _incrementZoomValue, float _timeInMilliseconds = 500f, float _acceleration = 0)
        {
            targetZoomValue = ValueRange.Enforce(targetZoomValue + _incrementZoomValue, Camera.ZoomRange);
            timeinMilliseconds = _timeInMilliseconds;
            acceleration = _acceleration;
            Zooming = true;
        }
        protected void GradualZoom(float _targetZoomValue, double deltaTime, float _timeInMilliseconds = 200f, float _acceleration = 0)
        {
            if (!gradualZoomInitialized)
            {
                gradualZoomInitialized = true;
                initialZoom = Camera.Zoom;
            }


            bool targetHigherThanInitial = _targetZoomValue > initialZoom;
            bool targetLowerThanInitial = _targetZoomValue < initialZoom;

            float totalDifference = _targetZoomValue - initialZoom;
            float currentDifference = _targetZoomValue - Camera.Zoom;
            float accelerationCoefficient = Camera.Zoom * acceleration;

            float zoomAmt = (float)((totalDifference + (accelerationCoefficient * 2)) * (deltaTime / _timeInMilliseconds));

            if ((targetHigherThanInitial && (Camera.Zoom + zoomAmt > _targetZoomValue)) | (targetLowerThanInitial && (Camera.Zoom + zoomAmt < _targetZoomValue)))
            {
                Camera.Zoom = _targetZoomValue;
            }

            bool targetReached = Camera.Zoom == _targetZoomValue;

            if (!targetReached)
            {
                zoomTimeElapsed += deltaTime;
                Camera.Zoom += zoomAmt;

            }

            else
            {
                gradualZoomInitialized = false;
                zoomTimeElapsed = 0;
                Zooming = false;
            }



        }

        protected void SnapZoomToIntegers(float tolerance)
        {
            int closestInt = (int)Math.Round(Camera.Zoom);

            bool withinTolerance()
            {
                if (Camera.Zoom < closestInt)
                {
                    return closestInt - Camera.Zoom >= tolerance;
                }

                else
                {
                    return Camera.Zoom - closestInt >= tolerance;
                }
            }

            if (withinTolerance())
            {
                Camera.Zoom = closestInt;
            }
        }

        #endregion

        public UpdateMethod UpdateMethod { get; set; }
        public override void Update(GameTime gameTime)
        {
            UpdateMethod.Invoke(gameTime);
        }

        private void BaseUpdate(GameTime gameTime)
        {
            if (Zooming)
            {
                GradualZoom(targetZoomValue, gameTime.ElapsedGameTime.TotalMilliseconds, timeinMilliseconds, acceleration);
            }
        }

        private void UpdateSnapToZoom(GameTime gameTime)
        {
            BaseUpdate(gameTime);

            if (!Zooming)
            {
                SnapZoomToIntegers(0.25f);
            }

        }
    }
}
