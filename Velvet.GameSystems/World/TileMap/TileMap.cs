using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using Velvet;
using System.IO;
using System.Linq;

namespace Velvet.GameSystems
{
    public class TileMap
    {
        public Tileset Tileset { get; private set; }

        private TileMapLayer[] tileMapLayers;

        public BoundingRect BoundingRect { get; private set; }

        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TileCount => Width * Height;
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public bool Infinite { get; private set; }
        public string FilePath { get; private set; }

        public TileMap()
        {

        }

        public TileMap(string filePath)
        {
            FilePath = filePath;
            Name = FilePath.TrimFileExtension();            
            
            
        }

        public void LoadContent()
        {
            //foreach(var layer in tileMapLayers)
            //{
            //    layer.LoadContent();
            //}
            LoadTileMapData(GetTileMapFile(FilePath));
            BoundingRect = new BoundingRect(0, 0, Width * TileWidth, Height * TileHeight);

        }

        public void UnloadContent()
        {
            foreach (var layer in tileMapLayers)
            {
                layer.UnloadContent();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tileMapLayers.Length; i++)
            {
                tileMapLayers[i].Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var layer in tileMapLayers)
            {
                layer.Update(gameTime);
            }
        }

        private XDocument GetTileMapFile(string filePath)
        {
            XDocument mapFile = default;
            string fullPath = Path.GetFullPath(@"..\..\..\..\" + TileSetManager.TileSetDirectory + @"\" + filePath);

            try
            {
                mapFile = XDocument.Load(fullPath);
            }

            catch
            {
                throw new Exception("XDocument load failure");
            }

            return mapFile;

        }

        private void LoadTileMapData(XDocument mapFile)
        {
            XElement mapElement = mapFile.Element("map");

            Infinite = Convert.ToBoolean(int.Parse(mapElement.Attribute("infinite").Value));

            Width = int.Parse(mapElement.Attribute("width").Value);
            Height = int.Parse(mapElement.Attribute("height").Value);

            TileWidth = int.Parse(mapElement.Attribute("tilewidth").Value);
            TileHeight = int.Parse(mapElement.Attribute("tileheight").Value);

            string tilesetFullName = mapElement.Element("tileset").Attribute("source").Value.ToString();
            string tilesetName = tilesetFullName.TrimFileExtension();
            string tilesetFileExtension = tilesetFullName.GetFileExtension();
            Tileset = GetTileset(tilesetName, tilesetFileExtension);
            LoadMapLayers(mapElement.Descendants("layer").ToList());

            

        }

        private Tileset GetTileset(string name, string fileExtension)
        {
            Tileset tileset = TileSetManager.GetTileSetByName(name);

            if(tileset == null)
            {
                TileSetManager.LoadTileset(name + fileExtension);
            }

            return tileset;

        }



        private void LoadMapLayers(List<XElement> layerElements)
        {
            tileMapLayers = new TileMapLayer[layerElements.Count];

            for (int i = 0; i < tileMapLayers.Length; i++)
            {
                tileMapLayers[i] = new TileMapLayer(this, layerElements[i]);
            }
        }

    }
}
