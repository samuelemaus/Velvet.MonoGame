using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Velvet.Rendering;

namespace Velvet.GameSystems
{
    public class Tileset
    {
        public Tileset(XElement tileset)
        {
            LoadTilesetData(tileset);

        }

        public Tileset LoadTileset(string filePath)
        {
            return default;
        }

        public string Name { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public Dimensions2D TileDimensions => new Dimensions2D(TileWidth, TileHeight);
        public int TileCount { get; private set; }
        public int Columns { get; private set; }

        //TODO
        public int Offset { get; private set; } 
        public string SourceImagePath { get; private set; }
        public int SourceImageWidth { get; private set; }
        public int SourceImageHeight { get; private set; }

        
        public TextureAtlas TextureAtlas { get; private set; }



        private void PopulateTiles(XElement tileset)
        {

            


            var tileNodes = tileset.Nodes();



        }

        private void LoadTilesetData(XElement tileset)
        {
            Name = tileset.Attribute("name").Value;
            TileWidth = int.Parse(tileset.Attribute("tilewidth").Value);
            TileHeight = int.Parse(tileset.Attribute("tileheight").Value);
            TileCount = int.Parse(tileset.Attribute("tilecount").Value);
            Columns = int.Parse(tileset.Attribute("columns").Value);
            SourceImagePath = tileset.Element("image").Attribute("source").Value.ToString().TrimStart(new char[3] { '.', '.', '/' }).TrimFileExtension();
            SourceImageWidth = int.Parse(tileset.Element("image").Attribute("width").Value);
            SourceImageHeight = int.Parse(tileset.Element("image").Attribute("height").Value);
        }

        public void LoadContent()
        {
            Texture2D sourceTexture = GetSourceTexture();
            TextureAtlas = new TextureAtlas(sourceTexture, TileDimensions);
        }

        public void UnloadContent()
        {

        }

        private Texture2D GetSourceTexture()
        {
            return SceneController.Content.Load<Texture2D>(SourceImagePath);
        }

      

    }
}
