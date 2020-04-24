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
    public class RectImage : Image, IDrawableTexture
    {
        #region//Constructors
        public RectImage()
        {
            
        }

        public RectImage(BoundingRect target)
        {
            
            InitialTargetRect = target;
            InitializeDimensions();
            DrawMethod = DrawRect;
        }

        public RectImage(Rectangle target)
        {
            
            InitialTargetRect = new BoundingRect(target);
            InitializeDimensions();
            DrawMethod = DrawRect;
        }

        public RectImage(IBoundingRect target)
        {
            
            InitialTargetRect = target.BoundingRect;
            InitializeDimensions();
            this.SetPositionDependency(target);
            DrawMethod = DrawRect;
        }

        #endregion

        #region//Content

        public override BoundingRect BoundingRect => boundingRect;


        private Rectangle _destinationRect;

        private Rectangle destinationRect
        {
            get
            {
                _destinationRect.Location = BoundingRect.TopLeft.ToPoint();
                _destinationRect.Width = (int)BoundingRect.Dimensions.Width;
                _destinationRect.Height = (int)BoundingRect.Dimensions.Height;

                return _destinationRect;
            }
        }

        private bool stretchToFill;
        public bool StretchToFill { get { return stretchToFill; }
            set
            {
                stretchToFill = value;

                if(value == true)
                {
                    DrawMethod = DrawRectStretched;
                }

            }

        }

        private Vector2 scale = Vector2.One;
        public override Vector2 Scale
        {
            get
            {
                if (StretchToFill)
                {
                    return new Vector2((BoundingRect.Dimensions.Width / Texture.Width), (BoundingRect.Dimensions.Height / Texture.Height));
                }

                else
                {
                    return scale;
                }
                
            }

            set
            {
                scale = value;
            }
        }

        private BoundingRect initialTargetRect;
        public BoundingRect InitialTargetRect
        {
            get
            {
                return initialTargetRect;
            }

            set
            {
                initialTargetRect = value;
                InitializeDimensions();
            }
        }
        #endregion

        #region//IDrawableTexture

        public static Texture2D DefaultRectImageTexture;

        private Texture2D texture = DefaultRectImageTexture;
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                UpdateDimensions();

            }
        }



        public static string DefaultPath = "Images/EmptyRect";
        public string FilePath { get; set; } = DefaultPath;

        private Rectangle sourceRect;
        public Rectangle SourceRect
        {
            get
            {
                return sourceRect;
            }

            set
            {
                sourceRect = value;
                if(DrawMethod != DrawRectFromRegion)
                {
                    DrawMethod = DrawRectFromRegion;
                }
            }
        }

        #endregion

        protected override DrawDelegate DrawMethod { get; set; }

        protected override void InitializeDimensions()
        {

            boundingRect.Dimensions = InitialTargetRect.Dimensions;
            Position = InitialTargetRect.CenterPosition;
            InitializeOrigin();
            _destinationRect = BoundingRect.ToRectangle();
            
        }

        private void UpdateDimensions()
        {
            boundingRect.Dimensions = InitialTargetRect.Dimensions;
            InitializeOrigin();
            _destinationRect = BoundingRect.ToRectangle();
        }


        #region//XNA Methods

        public override void Update(GameTime gameTime)
        {
            
        }


        protected void DrawRect(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, destinationRect, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }

        protected void DrawRectFromRegion(SpriteBatch spriteBatch)
        {
            //var dest = BoundingRect.ToRectangle();

            spriteBatch.Draw(Texture, destinationRect, SourceRect, Color * Alpha, Rotation, Vector2.Zero, SpriteEffect, LayerDepth);
        }

        private Vector2 drawScale => new Vector2((BoundingRect.Dimensions.Width / Texture.Width), (BoundingRect.Dimensions.Height / Texture.Height));
        protected void DrawRectStretched(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color * Alpha, Rotation, Origin / drawScale, drawScale, SpriteEffect, LayerDepth);
        }

        #endregion

        public override string ToString()
        {
            return $"{Color}, {BoundingRect}";
        }
    }
}
