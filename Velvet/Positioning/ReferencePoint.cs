using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Velvet
{
    public struct ReferencePoint
    {


        public ReferencePoint(XReference x, YReference y)
        {
            X = x;
            Y = y;

        }



        public XReference X { get; set; }
        public YReference Y { get; set; }

        public BindType BindType
        {
            get
            {
                if (X != XReference.Unbound && Y != YReference.Unbound)
                {
                    return BindType.FullyBound;
                }

                else if (X == XReference.Unbound)
                {
                    return BindType.X_Unbound;
                }

                else if (Y == YReference.Unbound)
                {
                    return BindType.Y_Unbound;
                }

                else
                {
                    return BindType.Unbound;
                }

            }
        }




        //Fully Bound
        public static ReferencePoint Centered = new ReferencePoint(XReference.Center, YReference.Center);
        public static ReferencePoint LeftCentered = new ReferencePoint(XReference.Left, YReference.Center);
        public static ReferencePoint RightCentered = new ReferencePoint(XReference.Right, YReference.Center);
        public static ReferencePoint TopCentered = new ReferencePoint(XReference.Center, YReference.Top);
        public static ReferencePoint BottomCentered = new ReferencePoint(XReference.Center, YReference.Bottom);
        public static ReferencePoint TopLeft = new ReferencePoint(XReference.Left, YReference.Top);
        public static ReferencePoint TopRight = new ReferencePoint(XReference.Right, YReference.Top);
        public static ReferencePoint BottomLeft = new ReferencePoint(XReference.Left, YReference.Bottom);
        public static ReferencePoint BottomRight = new ReferencePoint(XReference.Right, YReference.Bottom);

        //Partially Unbound
        public static ReferencePoint UnboundLeft = new ReferencePoint(XReference.Left, YReference.Unbound);
        public static ReferencePoint UnboundRight = new ReferencePoint(XReference.Right, YReference.Unbound);
        public static ReferencePoint UnboundTop = new ReferencePoint(XReference.Unbound, YReference.Top);
        public static ReferencePoint UnboundBottom = new ReferencePoint(XReference.Unbound, YReference.Bottom);

        private static List<ReferencePoint> referencePoints = new List<ReferencePoint>()
        {
            Centered,
            LeftCentered,
            RightCentered,
            TopCentered,
            BottomCentered,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            UnboundLeft,
            UnboundRight,
            UnboundTop,
            UnboundBottom
        };

        //Fully Unbound
        public static ReferencePoint UnboundReference = new ReferencePoint(XReference.Unbound, YReference.Unbound);


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
            { BottomLeft,TopRight },

            //Partially Unbound
            { UnboundLeft, UnboundRight },
            { UnboundRight, UnboundLeft },
            { UnboundTop, UnboundBottom },
            { UnboundBottom, UnboundTop },

            {UnboundReference,UnboundReference }
        };

        public ReferencePoint ToInverted()
        {
            ReferencePoint returnValue = new ReferencePoint();

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
