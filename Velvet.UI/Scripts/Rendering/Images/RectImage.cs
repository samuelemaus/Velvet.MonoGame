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
    /// <summary>
    /// An Image that fills the space of a desginated Target Rectangle.  Can be filled with  Texture, but defaults to a single pixel that can be colored.
    /// </summary>
    public class RectImage : Image2D
    {
        public RectImage()
        {
            TargetRect = new ReferenceRect();
            DrawOrigin = ReferencePoint.TopLeft;
        }

        public RectImage(ReferenceRect rect)
        {
            InitializeToDefaults();
            DrawOrigin = ReferencePoint.TopLeft;

            TargetRect = rect;

        }


        #region//Dimensions
        public ReferenceRect TargetRect;

        protected override void InitializeDimensions()
        {
            Dimensions.X = Texture.Width;
            Dimensions.Y = Texture.Height;

            if (SourceRect == Rectangle.Empty)
            {
                SourceRect = new Rectangle(0, 0, (int)Dimensions.X, (int)Dimensions.Y);
            }

            Position = TargetRect.Content.Location.ToVector2();

            InitializeOrigin();

            CurrentRect = TargetRect;


        }
        protected override void InitializeContent()
        {
            base.InitializeContent();

            if (Path != null)
            {
                Texture = content.Load<Texture2D>(Path);
            }

            else
            {
                Texture = content.Load<Texture2D>("Images/EmptyRect");
            }
        }
        protected override void InitializeOrigin()
        {
            float x = 0;
            float y = 0;

            switch (DrawOrigin.X)
            {
                case XReference.Center:

                    x = TargetRect.Content.Width / 2;

                    break;

                case XReference.Left:

                    x = 0;

                    break;

                case XReference.Right:

                    x = TargetRect.Content.Width;

                    break;
            }

            switch (DrawOrigin.Y)
            {
                case YReference.Center:

                    y = TargetRect.Content.Height / 2;

                    break;

                case YReference.Top:

                    y = 0;

                    break;

                case YReference.Bottom:

                    y = TargetRect.Content.Height;

                    break;
            }

            Origin = new Vector2(x, y);

        }
        protected override void SetOrigin()
        {
            float x = 0;
            float y = 0;

            switch (DrawOrigin.X)
            {
                case XReference.Center:

                    x = TargetRect.Content.Width / 2;

                    break;

                case XReference.Left:

                    x = 0;

                    break;

                case XReference.Right:

                    x = TargetRect.Content.Width;

                    break;
            }

            switch (DrawOrigin.Y)
            {
                case YReference.Center:

                    y = TargetRect.Content.Height / 2;

                    break;

                case YReference.Top:

                    y = 0;

                    break;

                case YReference.Bottom:

                    y = TargetRect.Content.Height;

                    break;
            }

            Origin.X = x;
            Origin.Y = y;
        }
        protected override void UpdateDimensions()
        {
            Dimensions.X = System.Math.Abs(Texture.Width * Scale.X);
            Dimensions.Y = System.Math.Abs(Texture.Width * Scale.Y);


            //SetOrigin();

            SetPositionToAnchor();
        }
        protected override void SetPositionToAnchor()
        {
            if (TargetRect.IsAnchored && TargetRect.Anchor != null)
            {
                if (TargetRect.Anchor.ReferencePoint.BindType == BindType.FullyBound)
                {
                    Position = TargetRect.Anchor.PositionDifferential;
                }

                else if (TargetRect.Anchor.ReferencePoint.BindType == BindType.X_Unbound)
                {
                    Position.Y = TargetRect.Anchor.PositionDifferential.Y;
                }

                else if (TargetRect.Anchor.ReferencePoint.BindType == BindType.Y_Unbound)
                {
                    Position.X = TargetRect.Anchor.PositionDifferential.X;
                }

                else if (TargetRect.Anchor.ReferencePoint.BindType == BindType.Unbound)
                {
                    TargetRect.IsAnchored = false;
                }

            }

            else
            {
                Position = TargetRect.Content.Location.ToVector2();
            }
        }
        #endregion

        #region//XNA Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, TargetRect.Content, Color * Alpha, Rotation, Origin, Scale, SpriteEffects.None, 0);
        }

        #endregion
    }
}
