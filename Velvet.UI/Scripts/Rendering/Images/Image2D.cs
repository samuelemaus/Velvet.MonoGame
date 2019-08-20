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
    public abstract class Image2D : IAnchorableObject
    {
        #region //Content
        public object ParentObject { get; }
        public float Alpha;
        public string Path;

        public Texture2D Texture;
        public Color Color;

        public Vector2 Position;
        public Vector2 InitialPosition;
        public Vector2 Scale;
        public Rectangle SourceRect;
        public PositionAnchor Anchor;

        public SpriteEffects SpriteEffect;

        public ReferencePoint DrawOrigin;
        public Vector2 Origin;

        public float Rotation;

        protected ContentManager content;
        public Vector2 Dimensions;

        public ReferenceRect CurrentRect { get; set; }

        public int Speed { get; set; }

        public bool IsActive = true;

        public bool IsMoving;
        public bool IsAnchored;
        #endregion

        #region//Dimensions
        protected void InitializeToDefaults()
        {


            Scale = Vector2.One;
            Alpha = 1.0f;

            if (SourceRect == null)
            {
                SourceRect = Rectangle.Empty;
            }


            Color = Color.White;
            SpriteEffect = SpriteEffects.None;
            CurrentRect = new ReferenceRect();


        }
        protected abstract void InitializeDimensions();
        protected abstract void UpdateDimensions();
        protected virtual void InitializeOrigin()
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
            Origin = new Vector2(x, y);


        }
        protected abstract void SetOrigin();

        protected virtual void SetPositionToAnchor()
        {
            if (CurrentRect.IsAnchored && CurrentRect.Anchor != null)
            {
                if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.FullyBound)
                {
                    Position = CurrentRect.Anchor.PositionDifferential/* - Origin*/;
                }

                else if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.X_Unbound)
                {
                    Position.Y = CurrentRect.Anchor.PositionDifferential.Y - Origin.Y;
                }

                else if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.Y_Unbound)
                {
                    Position.X = CurrentRect.Anchor.PositionDifferential.X - Origin.X;
                }

                else if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.Unbound)
                {
                    CurrentRect.IsAnchored = false;
                }

            }
        }

        #endregion


        #region//XNA Methods

        protected virtual void InitializeContent()
        {
            content = UserInterface.Renderer.ContentManager;
        }

        public virtual void LoadContent()
        {
            InitializeContent();
            InitializeDimensions();
        }
        public void UnloadContent()
        {
            content.Unload();
        }
        public virtual void Update(GameTime gameTime)
        {
            UpdateDimensions();
        }
        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion

    }
}
