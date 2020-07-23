using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public struct Dimensions2D : IEquatable<Dimensions2D>
    {
        public Dimensions2D(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public float Width;
        public float Height;

        public bool IsEmpty => (Width == 0 && Height == 0);

        #region//Operators
        public static Dimensions2D operator + (Dimensions2D first, Dimensions2D second)
        {
            float width = first.Width + second.Width;
            float height = first.Height + second.Height;

            return new Dimensions2D(width, height);

        }

        public static Dimensions2D operator -(Dimensions2D first, Dimensions2D second)
        {
            float width = first.Width - second.Width;
            float height = first.Height - second.Height;

            return new Dimensions2D(width, height);

        }

        public static Dimensions2D operator * (Dimensions2D first, Dimensions2D second)
        {
            float width = first.Width * second.Width;
            float height = first.Height * second.Height;

            return new Dimensions2D(width, height);

        }

        public static Dimensions2D operator *(Dimensions2D first, Vector2 second)
        {
            float width = first.Width * second.X;
            float height = first.Height * second.Y;

            return new Dimensions2D(width, height);

        }

        public static Dimensions2D operator / (Dimensions2D first, Dimensions2D second)
        {
            float width = first.Width / second.Width;
            float height = first.Height / second.Height;

            return new Dimensions2D(width, height);

        }
        public static Dimensions2D operator /(Dimensions2D first, Vector2 second)
        {
            float width = first.Width / second.X;
            float height = first.Height / second.Y;

            return new Dimensions2D(width, height);

        }

        public static Dimensions2D operator / (Dimensions2D first, int second)
        {
            return new Dimensions2D(first.Width / second, first.Height / second);
        }
        public static Dimensions2D operator *(Dimensions2D first, int second)
        {
            return new Dimensions2D(first.Width * second, first.Height * second);
        }

        public static Dimensions2D operator /(Dimensions2D first, float second)
        {
            return new Dimensions2D(first.Width / second, first.Height / second);
        }
        public static Dimensions2D operator *(Dimensions2D first, float second)
        {
            return new Dimensions2D(first.Width * second, first.Height * second);
        }

        public static bool operator ==(Dimensions2D first, Dimensions2D second)
        {
            return (first.Width == second.Width && first.Height == second.Height);
        }

        public static bool operator !=(Dimensions2D first, Dimensions2D second)
        {
            return first.Equals(second) == false;
        }




        #endregion

        #region//Public Methods

        public float HorizontalCenter => Width / 2;
        public float VerticalCenter => Height / 2;

        public Vector2 Center => new Vector2(HorizontalCenter, VerticalCenter);

        public static Dimensions2D Empty = new Dimensions2D(0, 0);
        public Dimensions2D Flipped()
        {
            return new Dimensions2D(Height, Width);
        }

        public bool IsSquare => Width == Height;


        #endregion

        public bool Equals(Dimensions2D other)
        {
            return (this.Width == other.Width && this.Height == other.Height);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"w:{Width}, h:{Height}";
        }

        public Vector2 ToVector2()
        {
            return new Vector2(Width, Height);
        }



        public static implicit operator Dimensions2D(Vector2 vector2)
        {
            return new Dimensions2D(vector2.X, vector2.Y);
        }

        public static implicit operator Dimensions2D(int num)
        {
            return new Dimensions2D(num, num);
        }

        public static implicit operator Dimensions2D(Rectangle rectangle)
        {
            return new Dimensions2D(rectangle.Width, rectangle.Height);
        }
        

        
    }
}
