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


    public class BasicImage2D : Image2D
    {
        #region //Constructors
        public BasicImage2D()
        {
            FilePath = string.Empty;

            DrawOrigin = ReferencePoint.Centered;

            InitializeToDefaults();

        }

        public BasicImage2D(List<BasicImage2D> list)
        {
            FilePath = string.Empty;
            DrawOrigin = ReferencePoint.Centered;
            InitializeToDefaults();
            list.Add(this);



        }





        #endregion


        #region//Dimensions



        protected override void SetOrigin()
        {
            float x = 0;
            float y = 0;

            switch (DrawOrigin.X)
            {
                case XReference.Center:

                    x = SourceRect.Width / 2;

                    break;

                case XReference.Left:

                    x = 0;

                    break;

                case XReference.Right:

                    x = SourceRect.Width;

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

        protected override void InitializeDimensions()
        {
            //Dimensions.X = Texture.Width;
            //Dimensions.Y = Texture.Height;

            //if (SourceRect == Rectangle.Empty)
            //{
            //    SourceRect = new Rectangle(0, 0, (int)Dimensions.X, (int)Dimensions.Y);
            //}

            ////Origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);

            //InitializeOrigin();

            //CurrentRect = new ReferenceRect(SourceRect);


        }



        protected override void UpdateDimensions()
        {

            //Dimensions.X = System.Math.Abs(Texture.Width * Scale.X);
            //Dimensions.Y = System.Math.Abs(Texture.Height * Scale.Y);

            //SetOrigin();

            //CurrentRect = new Rectangle((int)(Position.X - (origin.X * Scale.X)), (int)(Position.Y - (origin.Y * Scale.Y)), (int)Dimensions.X, (int)Dimensions.Y);

            

            //CurrentRect.Content.X = (int)(Position.X - (Origin.X * Scale.X));
            //CurrentRect.Content.Y = (int)(Position.Y - (Origin.Y * Scale.Y));
            //CurrentRect.Content.Width = (int)Dimensions.X;
            //CurrentRect.Content.Height = (int)Dimensions.Y;

            SetPositionToAnchor();



        }

        protected override void InitializeContent(IRenderer2D renderer2D)
        {
            base.InitializeContent(renderer2D);

            if (FilePath != string.Empty)
            {
                Texture = renderer2D.ContentManager.Load<Texture2D>(FilePath);
            }

            else
            {
                Texture = renderer2D.ContentManager.Load<Texture2D>("Images/Effects/BorderEffect");
                Scale = new Vector2(60, 60);
            }
        }
        #endregion

        #region //XNA Methods

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, Position, SourceRect, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, 0.0f);

        }

        #endregion


        public override string ToString()
        {
            if (ParentObject != null)
            {
                return ParentObject.ToString();
            }

            else
            {
                return FilePath;
            }

        }
    }

}
