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


        protected override void InitializeDimensions()
        {
            Dimensions = new Dimensions2D(Texture.Width, Texture.Height);

            SetOrigin();
        }

        protected override void SetOrigin()
        {
            Origin = new Vector2(Dimensions.HorizontalCenter, Dimensions.VerticalCenter);
        }
        
        protected void DrawTexture(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }
    }
}
