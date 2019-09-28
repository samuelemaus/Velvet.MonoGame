using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public class TextImage : Image, IDrawableString
    {
        #region//Constructors
        public TextImage()
        {
            this.text = "text null";
            InitializeDimensions();
            DrawMethod = DrawTextImage;
        }

        public TextImage(string text)
        {
            this.text = text;

            DrawMethod = DrawTextImage;

        }
        public TextImage(string text, SpriteFont font)
        {
            this.text = text;
            Font = font;
            //InitializeDimensions();
            DrawMethod = DrawTextImage;

        }



        #endregion


        #region//IDrawableString

        public IDrawableString Instantiate(string text)
        {
            return new TextImage(text);
        }

        private string text = "";
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                InitializeDimensions();
            }
        }
        protected string CasedText => Text.GetCasedText(TextCase);

        private TextAlignment alignment;
        public TextAlignment Alignment
        {
            get { return alignment; }

            set
            {
                alignment = value;
                InitializeOrigin();
            }
        }
        public TextCase TextCase { get; set; }

        private SpriteFont font;
        public SpriteFont Font
        {
            get { return font; }
            set
            {
                font = value;
                InitializeDimensions();
            }
        }
        
        

        #endregion

        protected override void InitializeDimensions()
        {
            if (Text != null)
            {
                Dimensions = (Font.MeasureString(CasedText) * Scale);
            }

            else
            {
                Dimensions = (Dimensions2D.Empty * Scale);
            }

            
            InitializeOrigin();

        }
        protected override void InitializeOrigin()
        {
            float x = 0;
            float y = Dimensions.VerticalCenter;
            switch (Alignment)
            {
                case TextAlignment.Center:
                    x = Dimensions.HorizontalCenter;

                    break;

                case TextAlignment.Left:

                    x = 0;

                    break;

                case TextAlignment.Right:

                    x = Dimensions.Width;

                    break;
            }

            Origin = new Vector2(x, y);
        }

        

        protected override DrawDelegate DrawMethod { get; set; }

        protected void DrawTextImage(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, CasedText, Position, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }
        protected void DrawTextImageWithShadow(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, CasedText, DrawPosition + Vector2.One, Color, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
            spriteBatch.DrawString(Font, CasedText, DrawPosition, Color, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }


        public override string ToString()
        {
            return $"({Text}, {Position}";
        }


    }
}
