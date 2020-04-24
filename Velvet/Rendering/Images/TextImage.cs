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
            CasedText = this.text;
            InitializeDimensions();
            DrawMethod = DrawTextImage;
        }

        public TextImage(string text, SpriteFont font = default, Color color = default)
        {
            this.text = text;
            CasedText = this.text;

            if (font != default)
            {
                Font = font;
            }
            if(color != default)
            {
                Color = color;
            }
            else
            {
                Color = Color.White;
            }

            DrawMethod = DrawTextImage;

        }
        public TextImage(string text, SpriteFont font)
        {

            this.text = text;
            CasedText = this.text;
            Font = font;
            DrawMethod = DrawTextImage;

        }



        #endregion
        #region//IDrawableString

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
                CasedText = text.GetCasedText(TextCase);
                InitializeDimensions();
            }
        }


        protected string CasedText;

        private TextAlignment alignment = TextAlignment.Center;
        public TextAlignment Alignment
        {
            get { return alignment; }

            set
            {
                alignment = value;
                InitializeOrigin();
            }
        }
        private TextCase textCase;
        public TextCase TextCase
        {
            get
            {
                return textCase;
            }

            set
            {
                textCase = value;
                CasedText = Text.GetCasedText(value);
            }
        }

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
                boundingRect.Dimensions = (Font.MeasureString(CasedText) * Scale);
            }

            else
            {
                boundingRect.Dimensions = (Dimensions2D.Empty);
            }

            
            InitializeOrigin();
            

        }
        protected override void InitializeOrigin()
        {
            float x = 0;
            float y = BoundingRect.Dimensions.VerticalCenter;
            switch (Alignment)
            {
                case TextAlignment.Center:
                    x = BoundingRect.Dimensions.HorizontalCenter;

                    break;

                case TextAlignment.Left:

                    x = 0;

                    break;

                case TextAlignment.Right:

                    x = BoundingRect.Dimensions.Width;

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
            spriteBatch.DrawString(Font, CasedText, Position + Vector2.One, Color, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
            spriteBatch.DrawString(Font, CasedText, Position, Color, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }


        public override string ToString()
        {
            return $"({Text}, {Position}";
        }

        

    }
}
