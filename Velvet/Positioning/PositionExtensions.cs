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
    public delegate Dimensions2D DimensionsOverrideFromSized(IDimensions2D target);
    public delegate Dimensions2D DimensionsOverrideFromMethod();


    public static class PositionExtensions
    {

        #region//Vector2 Extensions
        public static Vector2 RoundUp(this Vector2 value)
        {
            return new Vector2((float)Math.Ceiling(value.X), (float)Math.Ceiling(value.Y));
        }

        public static Vector2 RoundDown(this Vector2 value)
        {
            return new Vector2((float)Math.Floor(value.X), (float)Math.Floor(value.Y));
        }

        public static Vector2 Round(this Vector2 value)
        {
            return new Vector2((float)Math.Round(value.X), (float)Math.Round(value.Y));
        }

        public static Vector2 Absolute(this Vector2 value)
        {
            return new Vector2(Math.Abs(value.X), Math.Abs(value.Y));
        }
        #endregion

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

        #region//DimensionsDependency

        #region//SetDependency Methods

        public static void SetDimensionsDependency(this IDimensions2D dimensions, IDimensions2D dependency, DimensionsOverrideType overrideType = DimensionsOverrideType.FullOverride)
        {
            if(dimensions.DimensionsDependency is SizedObjectDimensionsDependency s)
            {
                s.Dependency = dependency;
                s.DependencyActive = true;
                s.OverrideType = overrideType;
            }

            else
            {
                dimensions.DimensionsDependency = new SizedObjectDimensionsDependency(dependency, overrideType);
            }
        }
        public static void SetDimensionsDependency(this IDimensions2D dimensions, IDimensions2D widthDependency, IDimensions2D heightDependency)
        {
            if(dimensions.DimensionsDependency is DualObjectDimensionsDependency d)
            {
                d.WidthDependency = widthDependency;
                d.HeightDependency = heightDependency;
            }

            else
            {
                dimensions.DimensionsDependency = new DualObjectDimensionsDependency(widthDependency, heightDependency);
            }
        }
        public static void SetDimensionsDependency(this IDimensions2D dimensions, DimensionsOverrideFromMethod overrideMethod, DimensionsOverrideType overrideType = DimensionsOverrideType.FullOverride)
        {
            if(dimensions.DimensionsDependency is MethodDimensionsDependency m)
            {
                m.OverrideMethod = overrideMethod;
                m.DependencyActive = true;
                m.OverrideType = overrideType;
            }

            else
            {
                dimensions.DimensionsDependency = new MethodDimensionsDependency(overrideMethod, overrideType);

            }
        }
        public static void SetDimensionsDependency(this IDimensions2D dimensions, DimensionsOverrideFromMethod widthMethod, DimensionsOverrideFromMethod heightMethod)
        {
            if(dimensions.DimensionsDependency is DualMethodDimensionsDependency d)
            {
                d.WidthOverrideMethod = widthMethod;
                d.HeightOverrideMethod = heightMethod;
                d.DependencyActive = true;
            }

            else
            {
                dimensions.DimensionsDependency = new DualMethodDimensionsDependency(widthMethod, heightMethod);
            }

        }



        #endregion

        #endregion

        #region//PositionDependency
        #region//SetDependency Methods
        public static void SetPositionDependency(this IMovable movable, IMovable dependency)
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
        public static void SetPositionDependency(this IMovable movable, PositionOverrideFromMethod overrideMethod)
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
        public static void SetPositionDependency(this IMovable movable, PositionOverrideFromMethod xMethod, PositionOverrideFromMethod yMethod)
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
        public static void SetPositionDependency(this IMovable movable, IMovable xDependency, IMovable yDependency)
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


        /// <summary>
        /// Creates a <see cref="SplitTypeDualPositionDependency"/> for the target <see cref="IMovable"/> object.  If either the xDependency or yDependency parameters are null, the existing <see cref="PositionDependency"/> will be utilized instead for that parameter.
        /// </summary>
        /// <param name="movable">The target <see cref="IMovable"/> object. </param>
        /// <param name="xDependency">The <see cref="PositionDependency"/> for the <see cref="IMovable"/> object's X coordinate. If left null, utilizes the object's current <see cref="PositionDependency"/></param>
        /// <param name="yDependency">The <see cref="PositionDependency"/> for the <see cref="IMovable"/> object's Y coordinate. If left null, utilizes the object's current <see cref="PositionDependency"/></param>
        public static void SetSplitPositionDependency(this IMovable movable, PositionDependency xDependency, PositionDependency yDependency)
        {
            PositionDependency x = xDependency;
            PositionDependency y = yDependency;

            if(xDependency == null)
            {
                x = movable.PositionDependency;
            }

            if(yDependency == null)
            {
                y = movable.PositionDependency;
            }

            if(xDependency == null && yDependency == null)
            {
                throw new ArgumentNullException("At least one PositionDependency must be supplied");
            }

            SplitTypeDualPositionDependency dep = new SplitTypeDualPositionDependency(x, y);

            movable.PositionDependency = dep;
            

        }

        #region//Tether Methods

        public static void TetherTo(this IMovable movable, IMovable dependency, float tolerance)
        {
            MovablePositionDependency dep;




        }


        #endregion

        #region//AnchorMethods

        public static void AnchorTo(this IBoundingRect rect, IBoundingRect target, ReferencePoint refPoint, RectRelativity relativity, float offset = 0, PositionOverrideType coordinateOverride = PositionOverrideType.FullOverride)
        {
            var dep = new MovablePositionDependency(target);        

            var differential = GetAnchorDifferential(rect.BoundingRect, target.BoundingRect, relativity, refPoint, offset);

            Vector2 constantDifferential(IMovable _target)
            {
                return target.BoundingRect.GetRectCorner(refPoint) - differential;
            }
            
            dep.OverrideMethod = constantDifferential;
            dep.OverrideType = coordinateOverride;

            rect.PositionDependency = dep;
        }

        public static void AnchorTo(this IBoundingRect rect, BoundingRect target, ReferencePoint refPoint, RectRelativity relativity, float offset = 0, PositionOverrideType coordinateOverride = PositionOverrideType.FullOverride)
        {            

            var differential = GetAnchorDifferential(rect.BoundingRect, target, relativity, refPoint, offset);

            Vector2 constantDifferential()
            {
                return target.GetRectCorner(refPoint) - differential;
            }

            var dep = new MethodPositionDependency(constantDifferential);
            dep.OverrideType = coordinateOverride;

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

        public static void AnchorToCurrentDifferential(this IBoundingRect rect, BoundingRect target)
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
                    case XReference.Left:   x = targetPosition.X + ((rect.Dimensions.HorizontalCenter) + offset); break;
                    case XReference.Right:  x = targetPosition.X - ((rect.Dimensions.HorizontalCenter) + offset); break;
                    case XReference.Center: x = targetPosition.X; break;
                }

                switch (refPoint.Y)
                {
                    case YReference.Top: y = targetPosition.Y - ((rect.Dimensions.VerticalCenter) + offset); break;
                    case YReference.Bottom: y = targetPosition.Y + ((rect.Dimensions.VerticalCenter) + offset); break;
                    case YReference.Center: y = targetPosition.Y; break;
                }

            }

            else
            {
                switch (refPoint.X)
                {
                    case XReference.Left: x = targetPosition.X + ((rect.Dimensions.HorizontalCenter) + offset); break;
                    case XReference.Right: x = targetPosition.X - ((rect.Dimensions.HorizontalCenter) + offset); break;
                    case XReference.Center: x = targetPosition.X; break;
                }

                switch (refPoint.Y)
                {
                    case YReference.Top: y = targetPosition.Y + ((rect.Dimensions.VerticalCenter) + offset); break;
                    case YReference.Bottom: y = targetPosition.Y - ((rect.Dimensions.VerticalCenter) + offset); break;
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

        #region/BoundingRect
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

        public static Dictionary<ReferencePoint, Vector2> OriginReferencePairs(BoundingRect rect)
        {
            Dictionary<ReferencePoint, Vector2> returnDict = new Dictionary<ReferencePoint, Vector2>();

            returnDict.Add(ReferencePoint.Centered,                 new Vector2(rect.Dimensions.HorizontalCenter, rect.Dimensions.VerticalCenter));
            returnDict.Add(ReferencePoint.TopCentered,              new Vector2(rect.Dimensions.HorizontalCenter, 0));
            returnDict.Add(ReferencePoint.BottomCentered,           new Vector2(rect.Dimensions.HorizontalCenter, rect.Dimensions.Height));
            returnDict.Add(ReferencePoint.LeftCentered,             new Vector2(0, rect.Dimensions.VerticalCenter));
            returnDict.Add(ReferencePoint.RightCentered,            new Vector2(rect.Dimensions.Width, rect.Dimensions.VerticalCenter));
            returnDict.Add(ReferencePoint.TopLeft,                  Vector2.Zero);
            returnDict.Add(ReferencePoint.TopRight,                 new Vector2(rect.Dimensions.Width, 0));
            returnDict.Add(ReferencePoint.BottomLeft,               new Vector2(0, rect.Dimensions.Height));
            returnDict.Add(ReferencePoint.BottomRight,              new Vector2(rect.Dimensions.Width, rect.Dimensions.Height));

            return returnDict;
        }


        public static BoundingRect ToBoundingRect(this Rectangle rect)
        {
            return new BoundingRect(rect);
        }
        #endregion

        /// <summary>
        /// Creates an <see cref="Array"/> of <see cref="Rectangle"/>s positioned and sized by the number of <paramref name="divisions"/>
        /// </summary>
        /// <param name="rect">The targeted <see cref="Rectangle"/></param>
        /// <param name="divisions">Number of times Rectangle will be equally divided. </param>
        /// <returns></returns>
        public static Rectangle[] SubdivideToGrid(this Rectangle rect, int divisions)
        {
            int numRects = divisions * divisions;

            var returnArray = new Rectangle[numRects];

            int height = rect.Height / divisions;
            int width = rect.Width / divisions;

            //Rows
            for (int i = 0; i < divisions; i++)
            {
                //Columns
                for (int j = 0; j < divisions; j++)
                {
                    int x = rect.X + width * (j/* + 1*/);
                    int y = rect.Y + height * (i/* + 1*/);

                    Rectangle next = new Rectangle(x, y, width, height);

                    int addIndex = (i * divisions) + j;

                    returnArray[addIndex] = next;

                }

            }

            return returnArray;

        }

        public static Rectangle[] SubdivideToGridByCellDimensions(this Rectangle rect, Dimensions2D cellDimensions, int cellCount = 0)
        {
            if(cellCount == 0)
            {
                int rectArea = rect.Width * rect.Height;
                int cellArea = (int)(cellDimensions.Width * cellDimensions.Height);

                cellCount = rectArea / cellArea;
            }

            var returnArray = new Rectangle[cellCount];

            int rows = (int)(rect.Height / cellDimensions.Height);
            int columns = (int)(rect.Width / cellDimensions.Width);
            

            int height = (int)cellDimensions.Height;
            int width = (int)cellDimensions.Width;

            //Rows
            for (int i = 0; i < rows; i++)
            {
                //Columns
                for (int j = 0; j < columns; j++)
                {
                    int x = rect.X + width * (j/* + 1*/);
                    int y = rect.Y + height * (i/* + 1*/);

                    Rectangle next = new Rectangle(x, y, width, height);

                    int addIndex = (i * columns) + j;

                    returnArray[addIndex] = next;

                }

            }

            return returnArray;

        }

        public static Rectangle[] SubdivideToGrid(this Rectangle rect, int rows, int columns)
        {
            int numRects = rows * columns;

            var returnArray = new Rectangle[numRects];

            int height = rect.Height / rows;
            int width = rect.Width / columns;

            //Rows
            for (int i = 0; i < rows; i++)
            {
                //Columns
                for (int j = 0; j < columns; j++)
                {
                    int x = rect.X + width * (j/* + 1*/);
                    int y = rect.Y + height * (i/* + 1*/);

                    Rectangle next = new Rectangle(x, y, width, height);

                    int addIndex = (i * columns) + j;

                    returnArray[addIndex] = next;

                }

            }


            return returnArray;
        }

        public static object[] Rearrange(this object[] rects, IndexArrangement arrangement)
        {
            var returnArray = new object[rects.Length];

            return returnArray;
        }

        public static Rectangle ToRectangle(this Dimensions2D dimensions)
        {
            return new Rectangle(0, 0, (int)dimensions.Width, (int)dimensions.Height);
        }

    }
}
