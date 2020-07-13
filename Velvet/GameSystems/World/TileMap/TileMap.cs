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
using System.Runtime.InteropServices;

namespace Velvet.GameSystems
{
    public class TileMap
    {
        public Tileset Tileset { get; protected set; }
        protected TileMapLayer[] tileMapLayers;
        public BoundingRect BoundingRect { get; protected set; }
        public string Name { get; set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public int TileCount => Width * Height;
        public int TileWidth { get; protected set; }
        public int TileHeight { get; protected set; }
        public bool Infinite { get; protected set; }
        public string FilePath { get; protected set; }

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

        //public void Update(GameTime gameTime)
        //{
        //    foreach (var layer in tileMapLayers)
        //    {
        //        layer.Update(gameTime);
        //    }
        //}

        protected XDocument GetTileMapFile(string filePath)
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

        
        protected virtual void LoadTileMapData(XDocument mapFile)
        {
            XElement mapElement = mapFile.Element(mapKey);

            Infinite = Convert.ToBoolean(int.Parse(mapElement.Attribute(infiniteKey).Value));

            Width = int.Parse(mapElement.Attribute(widthKey).Value);
            Height = int.Parse(mapElement.Attribute(heightKey).Value);

            TileWidth = int.Parse(mapElement.Attribute(tileWidthKey).Value);
            TileHeight = int.Parse(mapElement.Attribute(tileHeightKey).Value);

            string tilesetFullName = mapElement.Element(tilesetKey).Attribute(sourceKey).Value.ToString();
            string tilesetName = tilesetFullName.TrimFileExtension();
            string tilesetFileExtension = tilesetFullName.GetFileExtension();
            Tileset = GetTileset(tilesetName, tilesetFileExtension);
            LoadMapLayers(mapElement.Descendants(layerKey).ToList());
        }

        //XMLKeys

        protected static string mapKey = "map";
        protected static string infiniteKey = "infinite";
        protected static string widthKey = "width";
        protected static string heightKey = "height";
        protected static string tileWidthKey = "tilewidth";
        protected static string tileHeightKey = "tileheight";
        protected static string tilesetKey = "tileset";
        protected static string layerKey = "layer";
        protected static string sourceKey = "source";




        protected Tileset GetTileset(string name, string fileExtension)
        {
            Tileset tileset = TileSetManager.GetTileSetByName(name);

            if(tileset == null)
            {
                TileSetManager.LoadTileset(name + fileExtension);
            }

            return tileset;

        }



        protected virtual void LoadMapLayers(List<XElement> layerElements)
        {
            tileMapLayers = new TileMapLayer[layerElements.Count];

            for (int i = 0; i < tileMapLayers.Length; i++)
            {
                tileMapLayers[i] = new TileMapLayer(this, layerElements[i]);
            }
        }

    }
}
