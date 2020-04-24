using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.GameSystems
{
    public class CameraScroller : CameraEffect
    {
        public CameraScroller(OrthoCamera camera)
        {
            Camera = camera;
            EffectActive = true;
            scrollMethod = ScrollToTarget;
            targetReached = FullTargetReached;
        }

        public bool Scrolling { get; private set; }
        public float DefaultSpeed { get; set; } = 95f;
        public bool TargetReached => targetReached.Invoke();
        public Vector2 targetPosition;

        #region//Private Members
        
        private bool scrollInitialized = false;
        private PositionDependency bufferedDependency;
        private Vector2 initialPosition;
        private Vector2 currentPosition;
        private Vector2 nextPosition = default;
        
        private float timeInMilliseconds;
        private float xSpeed;
        private float ySpeed;
        private float acceleration;
        private PositionOverrideType positionOverrideType = PositionOverrideType.FullOverride;
        
        bool XLowerThanInitial => targetPosition.X < initialPosition.X;
        bool YLowerThanInitial => targetPosition.Y < initialPosition.Y;
        bool XHigherThanInitial => targetPosition.X > initialPosition.X;
        bool YHigherThanInitial => targetPosition.Y > initialPosition.Y;

        bool XIncrementExceedsTarget(float _xAmt)
        {
            return ((XHigherThanInitial && (Camera.Position.X + _xAmt > targetPosition.X))) | ((XLowerThanInitial && (Camera.Position.X + _xAmt < targetPosition.X)));
        }
        bool YIncrementExceedsTarget(float _yAmt)
        {
            return ((YHigherThanInitial && (Camera.Position.Y + _yAmt > targetPosition.Y))) | ((YLowerThanInitial && (Camera.Position.Y + _yAmt < targetPosition.Y)));
        }


        private delegate bool TargetReachedDelegate();
        private TargetReachedDelegate targetReached;

        private bool XTargetReached()
        {
            return Camera.Position.X == targetPosition.X;
        }

        private bool YTargetReached()
        {
            return Camera.Position.Y == targetPosition.Y;
        }
        
        private bool FullTargetReached()
        {
            return Camera.Position == targetPosition;
        }



        #endregion
        #region//Public Methods
        public override void Update(GameTime gameTime)
        {
            if (Scrolling)
            {
                scrollMethod.Invoke(targetPosition, timeInMilliseconds, acceleration, gameTime.ElapsedGameTime.TotalMilliseconds);
            }
        }
        public void InitiateScroll(Vector2 targetPosition, float timeInMilliseconds, float acceleration = 1f)
        {
            if (Scrolling)
            {
                Scrolling = false;
            }

            float x = ValueRange.Enforce(targetPosition.X, Camera.HorizontalRange);
            float y = ValueRange.Enforce(targetPosition.Y, Camera.VerticalRange);

            Vector2 boundaryCorrectedTarget = new Vector2(x, y);

            this.targetPosition = boundaryCorrectedTarget;
            this.timeInMilliseconds = timeInMilliseconds;
            this.acceleration = acceleration;
            targetReached = FullTargetReached;

            scrollMethod = ScrollToTarget;
            Scrolling = true;

        }
        public void InitiateInfiniteScroll(Direction direction, float speed, bool keepPartialFocus = true, float acceleration = 1f)
        {
            if (Scrolling)
            {
                Scrolling = false;
            }

            Vector2 targetSide = default;

            switch (direction)
            {
                case Direction.Right: targetSide = new Vector2(Camera.WorldBounds.Right, 0);
                    positionOverrideType = PositionOverrideType.XOverride;
                    targetReached = XTargetReached;
                    break;
                case Direction.Left: targetSide = new Vector2(Camera.WorldBounds.Left, 0);
                    positionOverrideType = PositionOverrideType.XOverride;
                    targetReached = XTargetReached;
                    break;
                case Direction.Up: targetSide = new Vector2(0, Camera.WorldBounds.Top);
                    positionOverrideType = PositionOverrideType.YOverride;
                    targetReached = YTargetReached;
                    break;
                case Direction.Down: targetSide = new Vector2(0, Camera.WorldBounds.Bottom);
                    positionOverrideType = PositionOverrideType.YOverride;
                    targetReached = YTargetReached;
                    break;
            }

            if (!keepPartialFocus)
            {
                positionOverrideType = PositionOverrideType.FullOverride;
            }

            float x = ValueRange.Enforce(targetSide.X, Camera.HorizontalRange);
            float y = ValueRange.Enforce(targetSide.Y, Camera.VerticalRange);

            Vector2 boundaryCorrectedTarget = new Vector2(x, y);

            this.targetPosition = boundaryCorrectedTarget;
            this.timeInMilliseconds = 15000 / speed;
            this.acceleration = acceleration;

            scrollMethod = InfiniteScroll;

            Scrolling = true;
            
        }
        #endregion
        #region//Private Methods
        private delegate void ScrollMethod(Vector2 target, float time, float acceleration, double delta);

        private ScrollMethod scrollMethod;
        private void ScrollToTarget(Vector2 targetPosition, float timeInMilliseconds, float acceleration, double deltaTime)
        {

            if (!scrollInitialized)
            {
                scrollInitialized = true;
                initialPosition = Camera.Position;
                currentPosition = initialPosition;
                Camera.PositionDependency.DependencyActive = false;
            }

            Vector2 currentDifferential = targetPosition - Camera.Position;
            Vector2 totalDifferential = targetPosition - initialPosition;

            float xAmt = (float)(totalDifferential.X * (deltaTime / timeInMilliseconds));
            float yAmt = (float)(totalDifferential.Y * (deltaTime / timeInMilliseconds));

            nextPosition = new Vector2(Camera.Position.X + xAmt, Camera.Position.Y + xAmt);

            if (XIncrementExceedsTarget(xAmt))
            {
                nextPosition.X = targetPosition.X;
            }

            if (YIncrementExceedsTarget(yAmt))
            {
                nextPosition.Y = targetPosition.Y;
            }

            

            if (!TargetReached)
            {
                Camera.Position = nextPosition;
            }


            else
            {
                scrollInitialized = false;
                Scrolling = false;
                Camera.PositionDependency.DependencyActive = true;
            }

        }
        private void InfiniteScroll(Vector2 targetPosition, float timeInMilliseconds, float acceleration, double deltaTime)
        {

            if (!scrollInitialized)
            {
                scrollInitialized = true;
                bufferedDependency = Camera.PositionDependency;
                initialPosition = Camera.Position;
                currentPosition = initialPosition;

                var dep = new MethodPositionDependency(positionReturnMethod);

                switch (positionOverrideType)
                {

                    case PositionOverrideType.FullOverride:
                        Camera.SetPositionDependencyWithInterrupt(dep, false);
                        Camera.PositionDependency.DependencyActive = true;
                        break;

                    case PositionOverrideType.XOverride:

                        var splitDepX = new SplitTypeDualPositionDependency(dep, Camera.PositionDependency);
                        Camera.SetPositionDependencyWithInterrupt(splitDepX, false);
                        Camera.PositionDependency.DependencyActive = true;
                        break;

                    case PositionOverrideType.YOverride:

                        var splitDepY = new SplitTypeDualPositionDependency(Camera.PositionDependency, dep);
                        Camera.SetPositionDependencyWithInterrupt(splitDepY, false);
                        Camera.PositionDependency.DependencyActive = true;

                        break;
                }


            }

            Vector2 currentDifferential = targetPosition - currentPosition;
            Vector2 totalDifferential = targetPosition - initialPosition;

            float xAmt = (float)(totalDifferential.X * (deltaTime / timeInMilliseconds));
            float yAmt = (float)(totalDifferential.Y * (deltaTime / timeInMilliseconds));

            nextPosition = new Vector2(currentPosition.X + xAmt, currentPosition.Y + xAmt);

            if (XIncrementExceedsTarget(xAmt))
            {
                nextPosition.X = targetPosition.X;
            }

            if (YIncrementExceedsTarget(yAmt))
            {
                nextPosition.Y = targetPosition.Y;
            }

            currentPosition = nextPosition;

            if (TargetReached)
            {
                scrollInitialized = false;
                Scrolling = false;
                Camera.PositionDependency = bufferedDependency;
                Camera.PositionDependency.DependencyActive = true;
            }



            //if (!TargetReached)
            //{
            //    Camera.CenterPosition = nextPosition;
            //}


            //else
            //{
            //    scrollInitialized = false;
            //    Scrolling = false;
            //    Camera.PositionDependency = bufferedDependency;
            //    Camera.PositionDependency.DependencyActive = true;
            //}

        }
        Vector2 positionReturnMethod()
        {
            return currentPosition;
        }
        #endregion
        
       

        

    }
}
