using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velvet.Rendering;

namespace Velvet
{
    public delegate void DrawDelegate(SpriteBatch spriteBatch);
    public delegate void UpdateDrawObject(IDrawableObject obj, object args);
    public static class RenderingExtensions
    {

        #region//Content

        public static void LoadImageContent(this IContentManager contentManager, IDrawableTexture drawableTexture)
        {
            bool hasFilePath = drawableTexture.FilePath != "";

            if (hasFilePath)
            {
                drawableTexture.Texture = contentManager.Content.Load<Texture2D>(drawableTexture.FilePath);
            }
        }

        public static void LoadImageContentFromPath(this IContentManager contentManager, IDrawableTexture drawableTexture, string filePath)
        {
            drawableTexture.Texture = contentManager.Content.Load<Texture2D>(filePath);
        }

        public static void LoadCompositeImageContent(this IRenderer2D renderer, IDrawableComposite drawableComposite)
        {
            
        }

        #endregion

        #region//Textures

        public static void SetRegion(this IDrawableTexture drawable, TextureRegion region)
        {
            drawable.Texture = region.SourceTexture;
            drawable.SourceRect = region.SourceRect;
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

        #region//IDrawableObject

        

        #endregion

        #region //Vector2




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


                }
            }

            return new Vector2(x, y);
        }




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
        public static Color Invert(this Color color)
        {
            int r = color.R;
            int g = color.G;
            int b = color.B;

            return new Color(g, b, r);

        }

        public static Color GetRandomColor(int min = 0, int max = 255)
        {
            Random rand = new Random();

            int r = rand.Next(min, max);
            int g = rand.Next(min, max);
            int b = rand.Next(min, max);

            return new Color(r, g, b);
        }

        #endregion

        public static void DrawCollection(this IEnumerable<IDrawableObject> drawables, SpriteBatch spriteBatch)
        {
            if(drawables != null)
            {
                foreach(var d in drawables)
                {
                    d.Draw(spriteBatch);
                }
            }
        }

        public static void UpdateCollection(this IEnumerable<IUpdate> updates, GameTime gameTime)
        {
            if(updates != null)
            {
                foreach(var u in updates)
                {
                    u.Update(gameTime);
                }
            }
        }

        public static string GetString(this IRenderer2D renderer)
        {
            return $"Pos: {renderer.RenderPosition}, Bounds{renderer.RenderTarget.Bounds}";
        }

        #region//RectImages


        public static void DrawRectImage(this IBoundingRect rect, ref List<IDrawableObject> targetList, float alpha = 0.35f, Color color = default)
        {
            Color rectColor = default;

            if(color == default)
            {
                rectColor = GetRandomColor();
            }

            else
            {
                rectColor = color;
            }

            RectImage rectImage = new RectImage(rect)
            {
                Color = rectColor,
                Alpha = alpha
            };

            targetList.Add(rectImage);

        }

        

        #endregion

    }
}
