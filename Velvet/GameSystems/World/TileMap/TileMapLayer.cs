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
        protected TileMap parentMap;
        protected Texture2D sourceTexture;
        public RenderTarget2D renderedStaticTiles { get; protected set; }
        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public int WidthInTiles { get; protected set; }
        public int HeightInTiles { get; protected set; }

        public int LayerWidth { get; protected set; }
        public int LayerHeight { get; protected set; }

        //TODO: Other TileLayerFormats
        public TileLayerFormat TileLayerFormat { get; private set; } = TileLayerFormat.CSV;

        public int TileCount => WidthInTiles * HeightInTiles;

        #region//IDrawableComposite

        protected BasicImage[] images;
        public override IEnumerable<IDrawableObject> Images => images;

        #endregion  

        public TileMapLayer()
        {

        }

        TileImageRect[] tiles;

        public TileMapLayer(TileMap parent, XElement layerData)
        {
            parentMap = parent;
            WidthInTiles = int.Parse(layerData.Attribute(widthKey).Value);
            HeightInTiles = int.Parse(layerData.Attribute(heightKey).Value);

            LayerWidth = WidthInTiles * parentMap.TileWidth;
            LayerHeight = HeightInTiles * parentMap.TileHeight;
            
            Name = layerData.Attribute(nameKey).Value;
            ID = int.Parse(layerData.Attribute(idKey).Value);
            
            string[] tileIndexStrings = layerData.Element(dataKey).Value.Split(commaDelimiter);
            int[] tileIndexes = new int[tileIndexStrings.Length];
            for (int i = 0; i < tileIndexes.Length; i++) {tileIndexes[i] = int.Parse(tileIndexStrings[i]);}
            Position = Vector2.Zero;
            Origin = Vector2.Zero;

            boundingRect = new BoundingRect(0, 0, WidthInTiles * parentMap.TileWidth, HeightInTiles * parentMap.TileHeight);

            //images = new BasicImage[TileCount];
            //InitializeImages(tileIndexes);
            DrawMethod = DrawTiles;
            InitializeTiles(tileIndexes);
        }

        //XML Keys
        protected static string widthKey = "width";
        protected static string heightKey = "height";
        protected static string nameKey = "name";
        protected static string idKey = "id";
        protected static string dataKey = "data";
        protected static char commaDelimiter = ',';

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
                if(columnIndex < (WidthInTiles - 1))
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

        protected void InitializeTiles(int[] tileIndexes)
        {
            tiles = new TileImageRect[tileIndexes.Length];
            TextureAtlas textureAtlas = parentMap.Tileset.TextureAtlas;
            sourceTexture = textureAtlas.SourceTexture;

            int rowIndex = 0;
            int columnIndex = 0;

            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new TileImageRect(tileIndexes[i], rowIndex, columnIndex, GetDestinationRectFromIndex(rowIndex, columnIndex), textureAtlas[tileIndexes[i] - 1].SourceRect);
                

                if (columnIndex < (WidthInTiles - 1))
                {
                    columnIndex++;
                }

                else
                {
                    columnIndex = 0;
                    rowIndex++;
                }
            }

            renderedStaticTiles = DrawStaticTilesToRenderTarget();
        }


        protected Vector2 GetPositionFromIndex(int row, int column)
        {
            float x = column * parentMap.TileWidth;
            float y = row * parentMap.TileHeight;

            return new Vector2(x, y);
        }

        protected Rectangle GetDestinationRectFromIndex(int row, int column)
        {
            int x = column * parentMap.TileWidth;
            int y = row * parentMap.TileHeight;

            return new Rectangle(x, y, parentMap.TileWidth, parentMap.TileHeight);

        }

        public void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                //spriteBatch.Draw(sourceTexture, tiles[i].Position/*.Round()*/, tiles[i].SourceRect, Color * Alpha, Rotation, Vector2.Zero, Scale, SpriteEffect, LayerDepth);
                spriteBatch.Draw(sourceTexture, tiles[i].DestinationRect, tiles[i].SourceRect, Color * Alpha, Rotation, Vector2.Zero, SpriteEffect, LayerDepth);
            }
        }

        public void DrawRenderTarget(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderedStaticTiles, renderedStaticTiles.Bounds, null, Color, Rotation, Vector2.Zero, SpriteEffect, LayerDepth);
        }

        private RenderTarget2D DrawStaticTilesToRenderTarget()
        {
            SpriteBatch spriteBatch = new SpriteBatch(GameRenderer.Instance.GraphicsDevice);

            RenderTarget2D renderTarget = new RenderTarget2D(GameRenderer.Instance.GraphicsDevice, LayerWidth, LayerHeight);

            GameRenderer.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, null);
            DrawTiles(spriteBatch);
            spriteBatch.End();

            return renderTarget;
        }

        public void LoadContent()
        {

        }


        public void UnloadContent()
        {

        }

    }
}
