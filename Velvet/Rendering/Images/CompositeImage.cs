using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Velvet
{

    /// <summary>
    /// Drawable Object which contains multiple <see cref="IDrawableObject"/> types. 
    /// </summary>
    public class CompositeImage : Image, IDrawableComposite
    {
        #region//Constructors

        public CompositeImage()
        {
            InitializeDimensions();
            DrawMethod = DrawImages;
        }

        public CompositeImage(IEnumerable<IDrawableObject> images, Vector2 position = default, Color color = default, Vector2 scale = default, float alpha = 0, float rotation = 0, float layerDepth = 0, SpriteEffects spriteEffect = default)
        {

            Images = images.ToList();

            Position = position;

            Color = color;

            Scale = scale;

            Alpha = alpha;

            Rotation = rotation;

            LayerDepth = layerDepth;

            SpriteEffect = spriteEffect;

            InitializeDimensions();
            DrawMethod = DrawImages;

        }

        #endregion

        #region//Fields & Properties
        public virtual IEnumerable<IDrawableObject> Images { get; set; }
        protected override DrawDelegate DrawMethod { get; set; }

        protected override void InitializeDimensions()
        {
            //InitializeDependencies();

            Dimensions = BoundingRect.Dimensions;

            InitializeOrigin();
        }

        protected virtual void InitializeDependencies()
        {
            foreach(var img in Images)
            {
                img.AnchorToCurrentDifferential(this);
            }
        }

        protected override void InitializeOrigin()
        {
            Origin = new Vector2(Dimensions.HorizontalCenter, Dimensions.VerticalCenter);
        }

        #endregion

        #region//Image Overrides

        

        public override BoundingRect BoundingRect
        {
            get
            {
                if(Images != null)
                {
                    return BoundingRect.Union(Images);
                }

                else
                {
                    return default;
                }
                
            }
        }

        #endregion

        #region//Methods







        public override void Update(GameTime gameTime)
        {
            foreach(var img in Images)
            {
                img.Update(gameTime);
            }
        }

        protected virtual void DrawImages(SpriteBatch spriteBatch)
        {
            

            foreach(var img in Images)
            {
                img.Draw(spriteBatch);
            }
        }

        #endregion

        #region//Common Positioning & Dimension Methods



        #endregion

    }
}
