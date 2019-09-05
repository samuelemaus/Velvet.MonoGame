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
            float width = first.Width * second.Width;
            float height = first.Height * second.Height;

            return new Dimensions2D(width, height);

        }

        public static Dimensions2D operator /(Dimensions2D first, Vector2 second)
        {
            float width = first.Width * second.X;
            float height = first.Height * second.Y;

            return new Dimensions2D(width, height);

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

        public static Dimensions2D Empty = new Dimensions2D(0, 0);

        public static implicit operator Dimensions2D(Vector2 vector2)
        {
            return new Dimensions2D(vector2.X, vector2.Y);
        }
        

        
    }
}
