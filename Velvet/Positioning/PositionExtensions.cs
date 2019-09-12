using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace Velvet
{
    public delegate Vector2 PositionOverrideFromMovable(IMovable target);
    public delegate Vector2 PositionOverrideFromMethod();


    public static class PositionExtensions
    {
        #region//Snapping

        public static void SnapTo(this IMovable movable, BoundingRect rect, ReferencePoint refPoint, float offset)
        {
            float xPos = 0;
            float yPos = 0;

            XReference xOrigin;
            YReference yOrigin;

            switch (refPoint.X)
            {
                case XReference.Left:
                    xPos = rect.Left - offset;
                    xOrigin = XReference.Right;
                    break;

                case XReference.Right:
                    xPos = rect.Right + offset;
                    xOrigin = XReference.Left;
                    break;

                case XReference.Center:
                    xPos = rect.Position.X + offset;
                    xOrigin = XReference.Center;
                    break;
            }

            switch (refPoint.Y)
            {
                case YReference.Top:
                    yPos = rect.Top - offset;
                    yOrigin = YReference.Bottom;
                    break;

                case YReference.Bottom:
                    yPos = rect.Bottom + offset;
                    yOrigin = YReference.Top;
                    break;

                case YReference.Center:
                    yPos = rect.Position.Y;
                    yOrigin = YReference.Center;
                    break;

            }

            movable.Position = new Vector2(xPos, yPos);
            
            

        }




        #endregion

        #region//PositionDependency
        #region//SetDependency Methods
        public static void SetDependency(this IMovable movable, IMovable dependency)
        {
            if (movable.PositionDependency is MovablePositionDependency m)
            {
                m.Dependency = dependency;
                m.DependencyActive = true;
            }

            else
            {
                movable.PositionDependency = new MovablePositionDependency(dependency);
            }
        }
        public static void SetDependency(this IMovable movable, PositionOverrideFromMethod overrideMethod)
        {
            if (movable.PositionDependency is MethodPositionDependency m)
            {
                m.OverrideMethod = overrideMethod;
            }

            else
            {
                movable.PositionDependency = new MethodPositionDependency(overrideMethod);
            }
        }
        public static void SetDependency(this IMovable movable, PositionOverrideFromMethod xMethod, PositionOverrideFromMethod yMethod)
        {
            if(movable.PositionDependency is DualMethodPositionDependency d)
            {
                d.XOverrideMethod = xMethod;
                d.YOverrideMethod = yMethod;
            }

            else
            {
                movable.PositionDependency = new DualMethodPositionDependency(xMethod, yMethod);
            }
        }
        public static void SetDependency(this IMovable movable, IMovable xDependency, IMovable yDependency)
        {
            if(movable.PositionDependency is DualMovablePositionDependency d)
            {
                d.XDependency = xDependency;
                d.YDependency = yDependency;
            }

            else
            {
                movable.PositionDependency = new DualMovablePositionDependency(xDependency, yDependency);
            }
        }
        #endregion

        #region//AnchorMethods

        public static void AnchorTo(this IBoundingRect rect, IBoundingRect target, ReferencePoint refPoint, RectRelativity relativity, float offset = 0, PositionCoordinateOverride coordinateOverride = PositionCoordinateOverride.FullOverride)
        {
            var dep = new MovablePositionDependency(target);

            Vector2 origin = GetOrigin(rect.BoundingRect, refPoint.ToInverted());

            if (relativity == RectRelativity.Inside)
            {
                origin = GetOrigin(rect.BoundingRect, refPoint/*.ToInverted()*/);
            }

            rect.Origin = origin;

            var differential = GetAnchorDifferential(rect.BoundingRect, target.BoundingRect, relativity, refPoint, offset);

            Vector2 constantDifferential(IMovable _target)
            {
                return target.BoundingRect.GetRectCorner(refPoint) - differential;
            }
            
            dep.OverrideMethod = constantDifferential;
            dep.CoordinateOverride = coordinateOverride;

            rect.PositionDependency = dep;

        }
        public static void AnchorToCurrentDifferential(this IBoundingRect rect, IBoundingRect target)
        {
            var differential = target.Position - rect.Position;

            Vector2 currentDifferential()
            {
                return target.Position - differential;
            }

            MethodPositionDependency dep = new MethodPositionDependency(currentDifferential);

            rect.PositionDependency = dep;

        }

        public static void AnchorToCurrentDifferential(this IBoundingRect rect, PositionOverrideFromMethod method)
        {
            var differential = method.Invoke() - rect.Position;

            Vector2 currentDifferential()
            {
                return method.Invoke() - differential;
            }

            rect.PositionDependency = new MethodPositionDependency(currentDifferential);

        }

        private static Vector2 GetAnchorDifferential(BoundingRect rect, BoundingRect target, RectRelativity relativity, ReferencePoint refPoint, float offset)
        {
            Vector2 value = default;

            float x = 0;
            float y = 0;

            Vector2 targetPosition = target.GetRectCorner(refPoint);
            

            if(relativity == RectRelativity.Outside)
            {
                switch (refPoint.X)
                {
                    case XReference.Left:   x = targetPosition.X + (/*rect.Right + */offset); break;
                    case XReference.Right:  x = targetPosition.X - (/*rect.Left + */offset); break;
                    case XReference.Center: x = targetPosition.X; break;
                }

                switch (refPoint.Y)
                {
                    case YReference.Top: y = targetPosition.Y + (/*rect.Bottom + */offset); break;
                    case YReference.Bottom: y = targetPosition.Y - (/*rect.Top + */offset); break;
                    case YReference.Center: y = targetPosition.Y; break;
                }

                //value = targetPosition + rect.GetRectCorner(refPoint.ToInverted());

                //return value;
            }

            else
            {
                switch (refPoint.X)
                {
                    case XReference.Left: x = targetPosition.X + (rect.Right + offset); break;
                    case XReference.Right: x = targetPosition.X - (rect.Left + offset); break;
                    case XReference.Center: x = targetPosition.X; break;
                }

                switch (refPoint.Y)
                {
                    case YReference.Top: y = targetPosition.Y + (rect.Bottom + offset); break;
                    case YReference.Bottom: y = targetPosition.Y - (rect.Top + offset); break;
                    case YReference.Center: y = targetPosition.Y; break;
                }
            }

            var sourcePosition = new Vector2(x, y);

            value = targetPosition - sourcePosition;

            return value;
        }

        
        
        

        #endregion

        #endregion

        #region//Move Methods
        public static void Move(this IMovable movable, Direction dir, float speed)
        {

        }

        public static void Move(this IMovable movable, Vector2 position)
        {
            movable.Position += position;
        }

        public static void MoveY(this IMovable movable, float speed)
        {
            movable.Position = new Vector2(movable.Position.X, movable.Position.Y + speed);
        }

        public static void MoveX(this IMovable movable, float speed)
        {
            movable.Position = new Vector2(movable.Position.X + speed, movable.Position.Y);
        }
        #endregion

        public static Vector2 GetOrigin(this BoundingRect rect, ReferencePoint refPoint)
        {
            Vector2 value = default;

            var dict = OriginReferencePairs(rect);

            if (dict.TryGetValue(refPoint, out value))
            {
                return value;
            }



            return value;
        }

        public static float GetRectSide(this BoundingRect rect, XReference xRef)
        {
            float value = 0;

            switch (xRef)
            {
                case XReference.Left: value = rect.Left; break;
                case XReference.Center: value = rect.Position.X; break;
                case XReference.Right: value = rect.Right; break;
            }

            return value;
        }

        public static float GetRectSide(this BoundingRect rect, YReference yRef)
        {
            float value = 0;

            switch (yRef)
            {
                case YReference.Top: value = rect.Top; break;
                case YReference.Center: value = rect.Position.Y; break;
                case YReference.Bottom: value = rect.Bottom; break;
            }

            return value;
        }

        public static Vector2 GetRectCorner(this BoundingRect rect, ReferencePoint refPoint)
        {
            return new Vector2(rect.GetRectSide(refPoint.X), rect.GetRectSide(refPoint.Y));
        }

        private static Dictionary<ReferencePoint, Vector2> OriginReferencePairs(BoundingRect rect)
        {
            Dictionary<ReferencePoint, Vector2> returnDict = new Dictionary<ReferencePoint, Vector2>();

            returnDict.Add(ReferencePoint.Centered, new Vector2(rect.Dimensions.HorizontalCenter, rect.Dimensions.VerticalCenter));
            returnDict.Add(ReferencePoint.TopCentered, new Vector2(rect.Dimensions.HorizontalCenter, 0));
            returnDict.Add(ReferencePoint.BottomCentered, new Vector2(rect.Dimensions.HorizontalCenter, rect.Dimensions.Height));
            returnDict.Add(ReferencePoint.LeftCentered, new Vector2(0, rect.Dimensions.VerticalCenter));
            returnDict.Add(ReferencePoint.RightCentered, new Vector2(rect.Dimensions.Width, rect.Dimensions.VerticalCenter));
            returnDict.Add(ReferencePoint.TopLeft, Vector2.Zero);
            returnDict.Add(ReferencePoint.TopRight, new Vector2(rect.Dimensions.Width, 0));
            returnDict.Add(ReferencePoint.BottomLeft, new Vector2(0, rect.Dimensions.Height));
            returnDict.Add(ReferencePoint.BottomRight, new Vector2(rect.Dimensions.Width, rect.Dimensions.Height));

            return returnDict;
        }


    }
}
