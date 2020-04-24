using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velvet.Rendering;

namespace Velvet.GameSystems
{
    public class CharacterImage : Image, IDrawableTexture
    {
        public CharacterImage()
        {

        }

        public CharacterImage(CharacterObject parent, string filePath)
        {
            ParentObject = parent;
            FilePath = filePath;
            DrawMethod = DrawTextureFromRegion;
            
        }

        public CharacterObject ParentObject { get; private set; }

        private TextureAtlas textureAtlas;
        public TextureAtlas TextureAtlas
        {
            get => textureAtlas;
            set
            {
                this.SetRegion(value.Regions[0]);
                InitializeDimensions();
                textureAtlas = value;
            }
        }

        private Texture2D texture;
        public Texture2D Texture
        {
            get => texture;
            set
            {
                InitializeDimensions();
                texture = value;
            }
        }

        //TODO
        public bool BufferTextureAtlas { get; set; } = false;
        public string FilePath { get; set; }

        
        

        public override Vector2 Position => ParentObject.Position;





        private Rectangle sourceRect;
        public Rectangle SourceRect
        {
            get => sourceRect;
            set
            {
                sourceRect = value;
                InitializeDimensions();
            }
        }
        public Dimensions2D TextureCellDimensions { get; set; } = new Dimensions2D(16, 16);
        protected override DrawDelegate DrawMethod { get; set; }

        protected void DrawTextureFromRegion(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, /*Draw*/Position, SourceRect, Color * Alpha, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }
        protected override void InitializeDimensions()
        {
            boundingRect.Dimensions = new Dimensions2D(SourceRect.Width, SourceRect.Height);
            InitializeOrigin();
        }

        public void InitializeTextureAtlas()
        {
            TextureAtlas = new TextureAtlas(texture, TextureCellDimensions);
        }

    }
}
