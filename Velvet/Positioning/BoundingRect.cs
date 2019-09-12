using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework;


namespace Velvet
{
    public struct BoundingRect : IEquatable<BoundingRect>
    {

        #region//Constructors

        public BoundingRect(Vector2 position, Dimensions2D dimensions)
        {
            Position = position;
            Dimensions = dimensions;
        }
        public BoundingRect(IMovable movable, IDimensions2D dimensions)
        {
            Position = movable.Position;
            Dimensions = dimensions.Dimensions;
        }
        public BoundingRect(float x, float y, float width, float height)
        {
            Position = new Vector2(x, y);
            Dimensions = new Dimensions2D(width, height);
        }
        public BoundingRect(Rectangle rectangle)
        {
            Position = rectangle.Center.ToVector2();
            Dimensions = new Dimensions2D(rectangle.Width, rectangle.Height);
        }

        #endregion

        #region//Private Fields
        private static BoundingRect empty = new BoundingRect(0,0,0,0);
        #endregion

        #region//Public Fields

        /// <summary>
        /// The X and Y coordinates of this <see cref="BoundingRect"/> represented as <see cref="Vector2"/> .
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The Height and Width of this <see cref="BoundingRect"/> represented as <see cref="Dimensions2D"/> .
        /// </summary>
        public Dimensions2D Dimensions;
        #endregion

        #region//Public Properties

        /// <summary>
        /// Checks if this <see cref="BoundingRect"/> is empty.
        /// </summary>
        public bool IsEmpty => (Position == Vector2.Zero & Dimensions.Equals(Dimensions2D.Empty));

        public static BoundingRect Empty => empty;

        //Sides

        /// <summary>
        /// The X coordinate of the Left side of this <see cref="BoundingRect"/>
        /// </summary>
        public float Left => Position.X - (Dimensions.Width / 2);
        /// <summary>
        /// The X coordinate of the Right side of this <see cref="BoundingRect"/>
        /// </summary>
        public float Right => Position.X + (Dimensions.Width / 2);
        /// <summary>
        /// The Y coordinate of the Top side of this <see cref="BoundingRect"/>
        /// </summary>
        public float Top => Position.Y - (Dimensions.Height / 2);
        /// <summary>
        /// The Y coordinate of the Bottom side of this <see cref="BoundingRect"/>
        /// </summary>
        public float Bottom => Position.Y + (Dimensions.Height / 2);

        //Corners

        /// <summary>
        /// The TopLeft corner of this <see cref="BoundingRect"/>
        /// </summary>
        public Vector2 TopLeft => new Vector2(Left, Top);
        /// <summary>
        /// The TopRight corner of this <see cref="BoundingRect"/>
        /// </summary>
        public Vector2 TopRight => new Vector2(Right, Top);
        /// <summary>
        /// The BottomLeft corner of this <see cref="BoundingRect"/>
        /// </summary>
        public Vector2 BottomLeft => new Vector2(Left, Bottom);
        /// <summary>
        /// The BottomRight corner of this <see cref="BoundingRect"/>
        /// </summary>
        public Vector2 BottomRight => new Vector2(Right, Bottom);

        /// <summary>
        /// Returns the Area (Width * Height) of this <see cref="Dimensions"/>.
        /// </summary>
        public float Area => Dimensions.Width * Dimensions.Height;




        #endregion

        #region//Public Methods

        #region//Contains Methods

        public bool Contains(float x, float y)
        {
            return ((Left <= x) && (x < Right) && (Top <= y) && (y < Bottom));
        }
        public bool Contains(int x, int y)
        {
            return ((Left <= x) && (x < Right) && (Top <= y) && (y < Bottom));
        }
        public bool Contains(Vector2 value)
        {
            float x = value.X;
            float y = value.Y;

            return ((Left <= x) && (x < Right) && (Top <= y) && (y < Bottom));

        }
        public void Contains(ref Vector2 value, out bool result)
        {
            float x = value.X;
            float y = value.Y;

            result = ((Left <= x) && (x < Right) && (Top <= y) && (y < Bottom));


        }
        public bool Contains(Point value)
        {
            float x = value.X;
            float y = value.Y;

            return ((Left <= x) && (x < Right) && (Top <= y) && (y < Bottom));

        }
        public bool Contains(Rectangle value)
        {
            return (this.Contains(value.Location) && value.Right < this.Right && value.Bottom < this.Bottom);
        }
        public bool Contains(BoundingRect value)
        {
            return (this.Contains(value.Position) && value.Area < this.Area);
        }


        #endregion

        #region//Intersects Methods
        public bool Intersects(BoundingRect value)
        {
            return  value.Left < Right &&
                    Left < value.Right &&
                    value.Top < Bottom &&
                    Top < value.Bottom;
        }
        public bool Intersects(Rectangle value)
        {
            return  value.Left < Right &&
                    Left < value.Right &&
                    value.Top < Bottom &&
                    Top < value.Bottom;
        }
        public static BoundingRect Intersect(ref BoundingRect value1, ref BoundingRect value2)
        {
            BoundingRect rect = default;

            if (value1.Intersects(value2))
            {


                float width = (Math.Min(value1.Right, value2.Right)) - (Math.Max(value1.Left, value2.Left));
                float height = (Math.Min(value1.Bottom, value2.Bottom)) - (Math.Max(value1.Top, value2.Top));

                rect = new BoundingRect(new Vector2(width / 2, height / 2), new Dimensions2D(width, height));

            }

            else
            {
                rect = Empty;
            }

            return rect;
        }
        public static BoundingRect Intersect(ref BoundingRect value1, ref Rectangle value2)
        {
            BoundingRect rect = default;

            if (value1.Intersects(value2))
            {


                float width = (Math.Min(value1.Right, value2.Right)) - (Math.Max(value1.Left, value2.Left));
                float height = (Math.Min(value1.Bottom, value2.Bottom)) - (Math.Max(value1.Top, value2.Top));

                rect = new BoundingRect(new Vector2(width / 2, height / 2), new Dimensions2D(width, height));

            }

            else
            {
                rect = Empty;
            }

            return rect;
        }
        public static BoundingRect Intersect(ref Rectangle value1, ref Rectangle value2)
        {
            BoundingRect rect = default;

            if (value1.Intersects(value2))
            {


                float width = (Math.Min(value1.Right, value2.Right)) - (Math.Max(value1.Left, value2.Left));
                float height = (Math.Min(value1.Bottom, value2.Bottom)) - (Math.Max(value1.Top, value2.Top));

                rect = new BoundingRect(new Vector2(width / 2, height / 2), new Dimensions2D(width, height));

            }

            else
            {
                rect = Empty;
            }

            return rect;
        }
        #endregion
        public void Inflate(float horizontal, float vertical)
        {
            float width = this.Dimensions.Width + horizontal;
            float height = this.Dimensions.Height + vertical;

            Dimensions = new Dimensions2D(width, height);
        }
        public void Offset(float x, float y)
        {
            Vector2 newPos = new Vector2(x, y);

            this.Position += newPos;

        }
        public void Offset(Vector2 value)
        {
            this.Position += value;
        }
        public void Offset(Direction direction, float value)
        {
            
        }

        public static BoundingRect Union(ref BoundingRect value1, ref BoundingRect value2)
        {
            BoundingRect rect = default;




            return rect;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)Left, (int)Top, (int)Dimensions.Width, (int)Dimensions.Height);
        }

        #endregion

        #region//Overrides & Operators

        public static bool operator == (BoundingRect first, BoundingRect second)
        {
            return (first.Dimensions == second.Dimensions && first.Position == second.Position);
        }

        public static bool operator != (BoundingRect first, BoundingRect second)
        {
            return (first.Dimensions != second.Dimensions || first.Position != second.Position);
        }

        public static bool operator ==(BoundingRect first, Rectangle second)
        {
            return first == second.ToBoundingRect();
        }

        public static bool operator !=(BoundingRect first, Rectangle second)
        {
            return first != second.ToBoundingRect();
        }

        

        public bool Equals(BoundingRect other)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
