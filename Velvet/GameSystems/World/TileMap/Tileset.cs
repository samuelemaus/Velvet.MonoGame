using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using Velvet.Rendering;

namespace Velvet.GameSystems
{
    public enum TilesetLoadType { FromTSX, FromImage}

    public class Tileset
    {

        

        public Tileset(XElement tileset, TilesetLoadType loadType = TilesetLoadType.FromTSX)
        {
            LoadTilesetDataFromTSX(tileset);
        }

        private Tileset()
        {

        }

        public static Tileset LoadTilesetFromImage(string filePath, int squareTileSize)
        {

            Tileset tileset = new Tileset();

            tileset.SourceImagePath = filePath;
            tileset.SetTileDimensions(squareTileSize);
            tileset.LoadContent();
            



            return tileset;
        }

        public string Name { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        private void SetTileDimensions(Dimensions2D dimensions)
        {
            this.TileWidth = (int)dimensions.Width;
            this.TileHeight = (int)dimensions.Height;
        }
        public Dimensions2D TileDimensions => new Dimensions2D(TileWidth, TileHeight);
        public int TileCount { get; private set; }
        public int Columns { get; private set; }

        //TODO
        public int Offset { get; private set; } 
        public string SourceImagePath { get; private set; }
        public int SourceImageWidth { get; private set; }
        public int SourceImageHeight { get; private set; }
        public Dimensions2D SourceImageDimensions => new Dimensions2D(SourceImageWidth, SourceImageHeight);
        private void SetSourceImageDimensions(Dimensions2D dimensions)
        {
            SourceImageWidth = (int)dimensions.Width;
            SourceImageHeight = (int)dimensions.Height;
        }

        private Dimensions2D GetSourceImageDimensionsFromImage(Texture2D texture)
        {
            Dimensions2D dimensions = 0;
            
            if(texture != null)
            {
                dimensions = new Dimensions2D(texture.Width, texture.Height);
            }

            return dimensions;
        }
        
        public TextureAtlas TextureAtlas { get; private set; }



        private void PopulateTiles(XElement tileset)
        {
            var tileNodes = tileset.Nodes();
        }

        private void LoadTilesetDataFromTSX(XElement tileset)
        {
            Name = tileset.Attribute(nameKey).Value;
            TileWidth = int.Parse(tileset.Attribute(tileWidthKey).Value);
            TileHeight = int.Parse(tileset.Attribute(tileHeightKey).Value);
            TileCount = int.Parse(tileset.Attribute(tilecountKey).Value);
            Columns = int.Parse(tileset.Attribute(columnsKey).Value);
            SourceImagePath = GetSourceImagePath(tileset);
            SourceImageWidth = int.Parse(tileset.Element(imageKey).Attribute(widthKey).Value);
            SourceImageHeight = int.Parse(tileset.Element(imageKey).Attribute(heightKey).Value);
        }

        //XML Keys
        private static string nameKey = "name";
        private static string tileWidthKey = "tilewidth";
        private static string tileHeightKey = "tileheight";
        private static string tilecountKey = "tilecount";
        private static string columnsKey = "columns";
        private static string imageKey = "image";
        private static string sourceKey = "source";
        private static string widthKey = "width";
        private static string heightKey = "height";

        private string GetSourceImagePath(XElement tileset)
        {
            return tileset.Element(imageKey).Attribute(sourceKey).Value.ToString().TrimStart(new char[3] { '.', '.', '/' }).TrimFileExtension();
        }

        public void LoadContent()
        {
            Texture2D sourceTexture = GetSourceTexture();
            TextureAtlas = new TextureAtlas(sourceTexture, TileDimensions);

            if(SourceImageDimensions == 0)
            {
                SetSourceImageDimensions(GetSourceImageDimensionsFromImage(sourceTexture));
            }

        }

        public void UnloadContent()
        {

        }

        private Texture2D GetSourceTexture()
        {
            return TileSetManager.Content.Load<Texture2D>(SourceImagePath);
        }

      

    }
}
