using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velvet
{
    public delegate void DrawDelegate(SpriteBatch spriteBatch);

    public static class RenderingExtensions
    {

        #region//SpriteBatch

        public static void DrawRect(this SpriteBatch spriteBatch, RectImage rectImage)
        {
               
        }

        #endregion

        #region//String
        public static string GetCasedText(this string text, TextCase textCase)
        {
            string value = text;

            if(textCase == TextCase.Default)
            {
                return value;
            }

            else if(textCase == TextCase.AllUpper)
            {
                return value.ToUpper();
            }

            else if(textCase == TextCase.AllLower)
            {
                return value.ToLower();
            }

            else if(textCase == TextCase.TitleCase)
            {
                return value.ToTitleCase();   
            }

            return value;
        }
        private static IEnumerable<char> CharsToTitleCase(string s)
        {
            bool newWord = true;
            foreach (char c in s)
            {
                if (newWord) { yield return Char.ToUpper(c); newWord = false; }
                else yield return Char.ToLower(c);
                if (c == ' ') newWord = true;
            }
        }
        public static string ToTitleCase(this string txt)
        {
            return new string(CharsToTitleCase(txt).ToArray());
        }
        #endregion

        #region //Vector2

        public static Vector2 GetOrigin(this BoundingRect rect, ReferencePoint refPoint)
        {
            Vector2 value = default;

            var dict = OriginReferencePairs(rect);

            if(dict.TryGetValue(refPoint,out value))
            {
                return value;
            }



            return value;
        }

        
        private static Dictionary<ReferencePoint, Vector2> OriginReferencePairs(BoundingRect rect)
        {
            Dictionary<ReferencePoint, Vector2> returnDict = default;

            returnDict.Add(ReferencePoint.Centered, new Vector2(rect.Dimensions.HorizontalCenter,rect.Dimensions.VerticalCenter));
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
        

        public static Vector2 GetRectDifferential(Rectangle source, Rectangle xtarget, Rectangle ytarget, RectRelativity xrel, RectRelativity yrel, ReferencePoint refPoint, int Offset)
        {
            int sourceXRef = source.Width;
            int sourceYRef = source.Height;

            int targetXRef;
            int targetYRef;

            int x = 0;
            int y = 0;

            if (xrel == RectRelativity.Outside)
            {
                switch (refPoint.X)
                {
                    case XReference.Left:

                        targetXRef = xtarget.Left;

                        x = targetXRef - Offset;

                        break;

                    case XReference.Center:

                        //sourceXRef = 0;
                        targetXRef = xtarget.Center.X;

                        x = targetXRef;

                        break;

                    case XReference.Right:

                        targetXRef = xtarget.Right;

                        x = targetXRef + Offset;

                        break;

                    case XReference.Unbound:

                        x = 0;

                        break;

                    case XReference.BoundCoordinate:

                        x = 0;

                        break;
                }



            }

            else if (xrel == RectRelativity.Inside)
            {
                switch (refPoint.X)
                {
                    case XReference.Left:

                        targetXRef = xtarget.Left;

                        x = targetXRef + Offset;

                        break;

                    case XReference.Center:

                        x = xtarget.Center.X;

                        break;

                    case XReference.Right:

                        targetXRef = xtarget.Right;

                        x = targetXRef - Offset;

                        break;

                    case XReference.Unbound:

                        x = 0;

                        break;

                    case XReference.BoundCoordinate:

                        x = 0;

                        break;
                }


            }

            if (yrel == RectRelativity.Outside)
            {
                switch (refPoint.Y)
                {
                    case YReference.Top:

                        targetYRef = ytarget.Top;

                        y = targetYRef - Offset;

                        break;

                    case YReference.Center:

                        y = ytarget.Center.Y;

                        break;

                    case YReference.Bottom:

                        targetYRef = ytarget.Bottom;

                        y = targetYRef + (Offset);

                        break;


                    case YReference.Unbound:

                        y = 0;

                        break;

                    case YReference.BoundCoordinate:

                        y = 0;

                        break;
                }
            }

            else if (yrel == RectRelativity.Inside)
            {
                switch (refPoint.Y)
                {
                    case YReference.Top:

                        targetYRef = ytarget.Top;

                        y = targetYRef + Offset;

                        break;

                    case YReference.Center:

                        y = ytarget.Center.Y;

                        break;

                    case YReference.Bottom:

                        targetYRef = ytarget.Bottom;

                        y = targetYRef - Offset;

                        break;

                    case YReference.Unbound:

                        y = 0;

                        break;

                    case YReference.BoundCoordinate:

                        y = 0;

                        break;
                }
            }

            return new Vector2(x, y);
        }

        //public static Vector2 ScreenCenter()
        //{

        //}

        //public static Vector2 SetPositionBy<T>


        #endregion

        #region//Color
        public static Color GetInterpolatedColor(Color color1, Color color2, float color2Percentage)
        {
            Color final;

            if (color2Percentage >= 1.0f)
            {
                return color2;
            }

            else if (color2Percentage <= 0)
            {
                return color1;
            }

            else
            {
                float color1Percentage = 1.0f - color2Percentage;

                int finalR = (int)(color1.R * color1Percentage) + (int)(color2.R * color2Percentage);
                int finalG = (int)(color1.G * color1Percentage) + (int)(color2.G * color2Percentage);
                int finalB = (int)(color1.B * color1Percentage) + (int)(color2.B * color2Percentage);

                final = new Color(finalR, finalG, finalB);

                return final;
            }


        }



        #endregion



        //public static void AnchorTo(this RectImage img, Image2D targImg, RectRelativity rel, ReferencePoint refPoint, int offset)
        //{
        //    PositionAnchor anchor = new PositionAnchor(img.TargetRect, targImg.CurrentRect, rel, refPoint, offset);

        //    if (anchor.XRelativity == RectRelativity.Outside)
        //    {
        //        img.DrawOrigin.X = anchor.ReferencePoint.ToInverted().X;
        //    }
        //    else
        //    {
        //        img.DrawOrigin.X = anchor.ReferencePoint.X;
        //    }

        //    if (anchor.YRelativity == RectRelativity.Outside)
        //    {
        //        img.DrawOrigin.Y = anchor.ReferencePoint.ToInverted().Y;
        //    }
        //    else
        //    {
        //        img.DrawOrigin.Y = anchor.ReferencePoint.Y;
        //    }
        //    img.TargetRect.Anchor = anchor;
        //    img.TargetRect.IsAnchored = true;

        //}

        //public static void AnchorTo(this RectImage img, ReferenceRect rect, RectRelativity rel, ReferencePoint refPoint, int offset)
        //{
        //    PositionAnchor anchor = new PositionAnchor(img.TargetRect, rect, rel, refPoint, offset);

        //    if (anchor.XRelativity == RectRelativity.Outside)
        //    {
        //        img.DrawOrigin.X = anchor.ReferencePoint.ToInverted().X;
        //    }
        //    else
        //    {
        //        img.DrawOrigin.X = anchor.ReferencePoint.X;
        //    }

        //    if (anchor.YRelativity == RectRelativity.Outside)
        //    {
        //        img.DrawOrigin.Y = anchor.ReferencePoint.ToInverted().Y;
        //    }
        //    else
        //    {
        //        img.DrawOrigin.Y = anchor.ReferencePoint.Y;
        //    }

        //    img.TargetRect.Anchor = anchor;
        //    img.TargetRect.IsAnchored = true;

        //}

    }
}
