using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velvet.Rendering;

namespace Velvet.GameSystems
{
    public enum TileLayerFormat {CSV, Base64Uncompressed, Base64ZlibCompressed, Base64ZStandardCompressed}

    public class TileMapLayer : CompositeImage
    {
        private TileMap parentMap;
        private Texture2D sourceTexture;
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        //TODO: Other TileLayerFormats
        public TileLayerFormat TileLayerFormat { get; private set; } = TileLayerFormat.CSV;

        public int TileCount => Width * Height;


        #region//IDrawableComposite

        private BasicImage[] images;
        public override IEnumerable<IDrawableObject> Images => images;

        #endregion  

        public TileMapLayer()
        {

        }

        TileImage[] tiles;

        public TileMapLayer(TileMap parent, XElement layerData)
        {
            parentMap = parent;
            Width = int.Parse(layerData.Attribute("width").Value);
            Height = int.Parse(layerData.Attribute("height").Value);
            Name = layerData.Attribute("name").Value;
            ID = int.Parse(layerData.Attribute("id").Value);
            
            string[] tileIndexStrings = layerData.Element("data").Value.Split(',');
            int[] tileIndexes = new int[tileIndexStrings.Length];
            for (int i = 0; i < tileIndexes.Length; i++) {tileIndexes[i] = int.Parse(tileIndexStrings[i]);}
            Position = Vector2.Zero;
            Origin = Vector2.Zero;

            boundingRect = new BoundingRect(0, 0, Width * parentMap.TileWidth, Height * parentMap.TileHeight);

            //images = new BasicImage[TileCount];
            //InitializeImages(tileIndexes);
            DrawMethod = DrawTiles;
            InitializeTiles(tileIndexes);
        }

        //private BoundingRect boundingRect;
        //public override BoundingRect BoundingRect => boundingRect;

        private void InitializeImages(int[] tileIndexes)
        {
            TextureAtlas textureAtlas = parentMap.Tileset.TextureAtlas;

            for (int i = 0; i < images.Length; i++)
            {
                images[i] = new BasicImage(textureAtlas[tileIndexes[i] - 1]);
                images[i].Origin = Vector2.Zero;
            }

            InitializeDependencies();
        }


        protected override void InitializeDependencies()
        {
            int rowIndex = 0;
            int columnIndex = 1;

            for (int i = 1; i < images.Length; i++)
            {
                
                images[i].Position = GetPositionFromIndex(rowIndex, columnIndex);
                

                if(columnIndex < (Width - 1))
                {
                    columnIndex++;
                }

                else
                {
                    columnIndex = 0;
                    rowIndex++;
                }



            }
        }

        private void InitializeTiles(int[] tileIndexes)
        {
            tiles = new TileImage[tileIndexes.Length];
            TextureAtlas textureAtlas = parentMap.Tileset.TextureAtlas;
            sourceTexture = textureAtlas.SourceTexture;

            int rowIndex = 0;
            int columnIndex = 0;

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new TileImage(tileIndexes[i], rowIndex, columnIndex, GetPositionFromIndex(rowIndex, columnIndex), textureAtlas[tileIndexes[i] - 1].SourceRect);
                

                if (columnIndex < (Width - 1))
                {
                    columnIndex++;
                }

                else
                {
                    columnIndex = 0;
                    rowIndex++;
                }
            }
        }


        private Vector2 GetPositionFromIndex(int row, int column)
        {
            float x = column * parentMap.TileWidth;
            float y = row * parentMap.TileHeight;

            return new Vector2(x, y);
        }

        public BasicImage this[int x, int y]
        {
            get
            {
                return null;
            }
        }

        public void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                spriteBatch.Draw(sourceTexture, tiles[i].Position.Round(), tiles[i].SourceRect, Color * Alpha, Rotation, Vector2.Zero, Scale, SpriteEffect, LayerDepth);
            }
        }

        public void LoadContent()
        {

        }


        public void UnloadContent()
        {

        }

    }
}
