using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Rendering
{
    public struct Dimensions2D : IEquatable<Dimensions2D>
    {
        

        public Dimensions2D(float width, float height)
        {
            Width = width;
            Height = height;
        }



        public readonly float Width;
        public readonly float Height;

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

        public static Dimensions2D operator / (Dimensions2D first, Dimensions2D second)
        {
            float width = first.Width * second.Width;
            float height = first.Height * second.Height;

            return new Dimensions2D(width, height);

        }
        #endregion

        public bool Equals(Dimensions2D other)
        {
            if(this.Width == other.Width && this.Height == other.Height)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static Dimensions2D Empty = new Dimensions2D(0, 0);

        public static implicit operator Dimensions2D(Vector2 vector2)
        {
            return new Dimensions2D(vector2.X, vector2.Y);
        }
        
        
    }
}
