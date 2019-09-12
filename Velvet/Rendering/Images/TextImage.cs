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
            SetText("text null");
            InitializeDimensions();
            DrawMethod = DrawTextImage;
        }

        public TextImage(string text)
        {
            SetText(text);

            DrawMethod = DrawTextImage;

        }
        public TextImage(string text, SpriteFont font)
        {
            SetText(text);
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

        public string Text { get; protected set; } = "";
        protected string CasedText => Text.GetCasedText(TextCase);

        private TextAlignment alignment;
        public TextAlignment Alignment
        {
            get { return alignment; }

            set
            {
                alignment = value;
                SetOrigin();
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
        public void SetText(string text)
        {
            this.Text = text;
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

            SetOrigin();

        }
        protected override void SetOrigin()
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
            spriteBatch.DrawString(Font, CasedText, Position, Color, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }
        protected void DrawTextImageWithShadow(SpriteBatch spriteBatch)
        {

        }





    }
}
