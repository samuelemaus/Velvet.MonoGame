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
    public class BasicImage : Image, IDrawableTexture
    {

        public BasicImage()
        {
            DrawMethod = DrawTexture;
            
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
            Dimensions = new Dimensions2D(Texture.Width, Texture.Height);

            
        }
        
        protected void DrawTexture(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawPosition, null, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }

        protected void DrawTextureFromRegion(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawPosition, SourceRect, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }
    }
}
