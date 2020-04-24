using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Velvet
{
    public class Alignment
    {


        public Alignment(HorizontalAlignment x, VerticalAlignment y)
        {
            X = x;
            Y = y;
        }

        public HorizontalAlignment X { get; set; }
        public VerticalAlignment Y { get; set; }

        public static readonly Alignment Centered = new Alignment(HorizontalAlignment.Center, VerticalAlignment.Center);
        public static readonly Alignment LeftCentered = new Alignment(HorizontalAlignment.Left, VerticalAlignment.Center);
        public static readonly Alignment RightCentered = new Alignment(HorizontalAlignment.Right, VerticalAlignment.Center);
        public static readonly Alignment TopCentered = new Alignment(HorizontalAlignment.Center, VerticalAlignment.Top);
        public static readonly Alignment BottomCentered = new Alignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
        public static readonly Alignment TopLeft = new Alignment(HorizontalAlignment.Left, VerticalAlignment.Top);
        public static readonly Alignment TopRight = new Alignment(HorizontalAlignment.Right, VerticalAlignment.Top);
        public static readonly Alignment BottomLeft = new Alignment(HorizontalAlignment.Left, VerticalAlignment.Bottom);
        public static readonly Alignment BottomRight = new Alignment(HorizontalAlignment.Right, VerticalAlignment.Bottom);

        public static List<Alignment> ReferencePoints = new List<Alignment>()
        {
            TopLeft, TopCentered, TopRight, LeftCentered, Centered, RightCentered, BottomLeft, BottomCentered, BottomRight
        };

        


        private static Dictionary<Alignment, Alignment> ReferencePointInversions = new Dictionary<Alignment, Alignment>()
        {
            { Centered,Centered},
            { LeftCentered,RightCentered },
            { RightCentered,LeftCentered },
            { TopCentered,BottomCentered },
            { BottomCentered,TopCentered },
            { TopLeft,BottomRight },
            { BottomRight,TopLeft },
            { TopRight,BottomLeft },
            { BottomLeft,TopRight }
        };

        public Alignment Invert()
        {
            Alignment returnValue = default;

            Alignment value = this;

            foreach (KeyValuePair<Alignment, Alignment> entry in ReferencePointInversions)
            {
                if (entry.Key == value)
                {
                    returnValue = entry.Value;
                }

            }

            return returnValue;
        }


        #region//Overrides & Operators
        public override string ToString()
        {
            return "X: " + X.ToString() + ", Y: " + Y.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Alignment))
            {
                return false;
            }

            var point = (Alignment)obj;
            return X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Alignment value1, Alignment value2)
        {
            if (value1.X == value2.X && value1.Y == value2.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Alignment value1, Alignment value2)
        {
            if (value1.X == value2.X && value1.Y == value2.Y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
