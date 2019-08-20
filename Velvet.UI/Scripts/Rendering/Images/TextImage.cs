using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.UI
{
    public class TextImage : Image2D
    {
        public string Text;

        private string CasedText()
        {
            string value;

            if (Text != null)
            {
                value = Text;
            }
            else
            {
                value = "text null";
            }


            //if(TextCase == TextCase.Default)
            //{
            //    return value;
            //}

            if (TextCase == TextCase.AllLower)
            {
                value = value.ToLower();
            }

            else if (TextCase == TextCase.AllUpper)
            {
                value = value.ToUpper();
            }

            return value;

        }

        public SpriteFont Font = UserInterface.Renderer.DefaultFont;

        public TextCase TextCase;

        public float WriteSpeed;

        //Shadow
        public bool HasShadow = true;
        public Color ShadowColor = Color.Black;
        public float ShadowAlpha = 0.85f;
        public Vector2 ShadowOffset = new Vector2(1, 2);


        public TextImage()
        {
            Text = string.Empty;

            DrawOrigin = ReferencePoint.TopLeft;

            //Font = MenuManager.Instance.MenuTheme.ItemFont;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            //Dimensions *= Scale;
            Color = Color.White;
        }

        public TextImage(string text)
        {
            Text = text;
            DrawOrigin = ReferencePoint.TopLeft;
            //Font = MenuManager.Instance.MenuTheme.ItemFont;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            //Dimensions *= Scale;
            Color = Color.White;

        }

        protected override void InitializeDimensions()
        {
            if (Text != null)
            {
                Dimensions.X = Font.MeasureString(Text).X * Scale.X;
                Dimensions.Y = Font.MeasureString(Text).Y * Scale.Y;
            }

            else
            {
                Dimensions = new Vector2(50, 50);
            }


            SourceRect = new Rectangle(0, 0, (int)Dimensions.X / 2, (int)Dimensions.Y / 2);

            InitializeOrigin();

            CurrentRect = new ReferenceRect();

            CurrentRect.Content.X = (int)(Position.X - (Origin.X * Scale.X));
            CurrentRect.Content.Y = (int)(Position.Y - (Origin.Y * Scale.Y));
            CurrentRect.Content.Width = (int)Dimensions.X;
            CurrentRect.Content.Height = (int)Dimensions.Y;

        }

        protected override void UpdateDimensions()
        {
            Dimensions.X = System.Math.Abs(Font.MeasureString(Text).X * Scale.X);
            Dimensions.Y = System.Math.Abs(Font.MeasureString(Text).Y * Scale.Y);

            SetOrigin();

            CurrentRect.Content.X = (int)(Position.X - (Origin.X * Scale.X));
            CurrentRect.Content.Y = (int)(Position.Y - (Origin.Y * Scale.Y));
            CurrentRect.Content.Width = (int)Dimensions.X;
            CurrentRect.Content.Height = (int)Dimensions.Y;

            if (CurrentRect.IsAnchored && CurrentRect.Anchor != null)
            {
                Position = CurrentRect.Anchor.PositionDifferential;
            }


        }

        protected override void SetOrigin()
        {
            float x = 0;
            float y = 0;

            switch (DrawOrigin.X)
            {
                case XReference.Center:

                    x = (Dimensions.X / Scale.X) / 2;

                    break;

                case XReference.Left:

                    x = 0;

                    break;

                case XReference.Right:

                    x = (Dimensions.X);

                    break;
            }
            switch (DrawOrigin.Y)
            {
                case YReference.Center:

                    y = SourceRect.Height / 2;

                    break;

                case YReference.Top:

                    y = 0;

                    break;

                case YReference.Bottom:

                    y = SourceRect.Height;

                    break;
            }

            Origin.X = x;

            Origin.Y = y;
        }

        public override void LoadContent()
        {
            //content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            content = UserInterface.Renderer.ContentManager;


            this.InitializeDimensions();
        }

        public override void Update(GameTime gameTime)
        {

            this.UpdateDimensions();



        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);

            if (HasShadow)
            {
                spriteBatch.DrawString(Font, CasedText(), Position + ShadowOffset, ShadowColor * ShadowAlpha, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
            }

            spriteBatch.DrawString(Font, CasedText(), Position, Color * Alpha, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);



        }





    }
}
