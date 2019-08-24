using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.Rendering
{
    public abstract class Image2D  : IDrawableObject
    {
       
        #region //Content
        public object ParentObject { get; }
        public float Alpha { get; set; }
        public string FilePath { get; protected set; }

        public Texture2D Texture { get; protected set; }
        public Color Color { get; protected set; }


        public Vector2 Position { get; protected set; }
        public Vector2 InitialPosition { get; }
        public Vector2 Scale { get; protected set; }
        public Rectangle SourceRect;

        public Rectangle BoundingRect { get; protected set; }

        public PositionAnchor Anchor;

        public SpriteEffects SpriteEffect { get; protected set; }

        public ReferencePoint DrawOrigin;
        public Vector2 Origin;

        public float Rotation { get; set; }

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
            //if (CurrentRect.IsAnchored && CurrentRect.Anchor != null)
            //{
            //    if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.FullyBound)
            //    {
            //        Position = CurrentRect.Anchor.PositionDifferential/* - Origin*/;
            //    }

            //    else if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.X_Unbound)
            //    {
            //        Position.Y = CurrentRect.Anchor.PositionDifferential.Y - Origin.Y;
            //    }

            //    else if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.Y_Unbound)
            //    {
            //        Position.X = CurrentRect.Anchor.PositionDifferential.X - Origin.X;
            //    }

            //    else if (CurrentRect.Anchor.ReferencePoint.BindType == BindType.Unbound)
            //    {
            //        CurrentRect.IsAnchored = false;
            //    }

            //}
        }

        #endregion

        #region//XNA Methods

        protected virtual void InitializeContent(IRenderer2D renderer2D)
        {
            content = renderer2D.ContentManager;
        }

        public virtual void LoadContent(IRenderer2D renderer2D)
        {
            InitializeContent(renderer2D);
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

        public virtual void SetPosition(Vector2 position)
        {
            this.Position = position;
        }

        public virtual void SetColor(Color color)
        {
            this.Color = color;
        }

        public virtual void SetAlpha(float alpha)
        {
            this.Alpha = alpha;
        }

        public virtual void SetScale(Vector2 scale)
        {
            this.Scale = scale;
        }

        public void Move(Vector2 position)
        {
            throw new NotImplementedException();
        }

        #endregion



    }
}
