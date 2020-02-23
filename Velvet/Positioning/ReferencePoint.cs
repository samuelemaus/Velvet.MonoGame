using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Velvet
{
    public class ReferencePoint
    {


        public ReferencePoint(XReference x, YReference y)
        {
            X = x;
            Y = y;

        }



        public XReference X { get; set; }
        public YReference Y { get; set; }

        //Fully Bound
        public static readonly ReferencePoint Centered = new ReferencePoint(XReference.Center, YReference.Center);
        public static readonly ReferencePoint LeftCentered = new ReferencePoint(XReference.Left, YReference.Center);
        public static readonly ReferencePoint RightCentered = new ReferencePoint(XReference.Right, YReference.Center);
        public static readonly ReferencePoint TopCentered = new ReferencePoint(XReference.Center, YReference.Top);
        public static readonly ReferencePoint BottomCentered = new ReferencePoint(XReference.Center, YReference.Bottom);
        public static readonly ReferencePoint TopLeft = new ReferencePoint(XReference.Left, YReference.Top);
        public static readonly ReferencePoint TopRight = new ReferencePoint(XReference.Right, YReference.Top);
        public static readonly ReferencePoint BottomLeft = new ReferencePoint(XReference.Left, YReference.Bottom);
        public static readonly ReferencePoint BottomRight = new ReferencePoint(XReference.Right, YReference.Bottom);

        public static List<ReferencePoint> ReferencePoints = new List<ReferencePoint>()
        {
            TopLeft, TopCentered, TopRight, LeftCentered, Centered, RightCentered, BottomLeft, BottomCentered, BottomRight
        };

        //Fully Unbound
        


        private static Dictionary<ReferencePoint, ReferencePoint> ReferencePointInversions = new Dictionary<ReferencePoint, ReferencePoint>()
        {
            //Fully Bound
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

        public ReferencePoint Invert()
        {
            ReferencePoint returnValue = default;

            ReferencePoint value = this;

            foreach (KeyValuePair<ReferencePoint, ReferencePoint> entry in ReferencePointInversions)
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
            if (!(obj is ReferencePoint))
            {
                return false;
            }

            var point = (ReferencePoint)obj;
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

        public static bool operator ==(ReferencePoint value1, ReferencePoint value2)
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

        public static bool operator !=(ReferencePoint value1, ReferencePoint value2)
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
