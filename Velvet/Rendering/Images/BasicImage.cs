using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Velvet.Rendering;

namespace Velvet
{
    public class BasicImage : Image, IDrawableTexture
    {

        public BasicImage()
        {
            DrawMethod = DrawTexture;
            Alpha = 1;
            Color = Color.White;
            
        }

        public BasicImage(TextureRegion textureRegion)
        {
            Texture = textureRegion.SourceTexture;
            SourceRect = textureRegion.SourceRect;
            InitializeDimensions();
            DrawMethod = DrawTextureFromRegion;
            Alpha = 1f;
            Color = Color.White;
        }

        public BasicImage(string filePath)
        {
            FilePath = filePath;
            DrawMethod = DrawTexture;
            Alpha = 1f;
            Color = Color.White;
        }

        private Texture2D texture;
        public Texture2D Texture { get { return texture; }
            set
            {
                texture = value;

                InitializeDimensions();

            }
        }

        
        public string FilePath { get; set; }

        protected override DrawDelegate DrawMethod { get; set; }

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
                DrawMethod = DrawTextureFromRegion;
            }
        }

        protected override void InitializeDimensions()
        {
            if(!SourceRect.IsEmpty)
            {
                boundingRect.Dimensions = new Dimensions2D(SourceRect.Width, SourceRect.Height);
            }

            else
            {
                boundingRect.Dimensions = new Dimensions2D(Texture.Width, Texture.Height);
            }

            
        }
        
        protected void DrawTexture(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, /*Draw*/Position, null, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }

        protected void DrawTextureFromRegion(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, /*Draw*/Position, SourceRect, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }

        public override string ToString()
        {
            return $"{FilePath}, Bounds: {BoundingRect}";
        }
    }
}
