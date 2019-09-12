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
        }

        #endregion

        #region//Content

        protected BoundingRect InitialTargetRect;
        public ReferencePoint OriginReferencePoint { get; protected set; } = ReferencePoint.Centered;
        #endregion

        #region//IDrawableTexture

        public static Texture2D DefaultRectImageTexture;
        public Texture2D Texture { get; set; } = DefaultRectImageTexture;

        public static string DefaultPath = "Images/EmptyRect";
        public string FilePath { get; set; } = DefaultPath;
        #endregion

        protected override DrawDelegate DrawMethod { get; set; }

        protected override void InitializeDimensions()
        {
            Dimensions = InitialTargetRect.Dimensions;
            Position = InitialTargetRect.Position;
            SetOrigin();

        }

        protected override void SetOrigin()
        {
            Origin = InitialTargetRect.GetOrigin(OriginReferencePoint);
        }

        #region//XNA Methods

        protected void DrawRect(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, BoundingRect.ToRectangle(), Color * Alpha, Rotation, Origin, Scale, SpriteEffects.None, 0);
        }



        #endregion
    }
}
