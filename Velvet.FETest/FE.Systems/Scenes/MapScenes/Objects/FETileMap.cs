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
using Velvet.GameSystems;
using System.Reflection;
using System.IO.IsolatedStorage;

namespace Velvet.FETest
{
    public class FETileMap : TileMap
    {

        #region//Content

        protected List<XElement> mapChanges = new List<XElement>();

        #endregion

        #region//Constants
        protected static int tileSize = 16;
        protected static string rootMapsFolder = "Maps";
        protected static string imageKey = "image";
        protected static string mainLayerKey = "Main";
        protected static string mapChangeKey = "Map Change";
        protected static string nameKey = "name";
        protected static string propertiesKey = "properties";
        protected static string propertyKey = "property";
        protected static string tilesetImageFolder = "img";
        #endregion
        public FETileMap(string filePath)
        {
            FilePath = filePath;
            Name = FilePath.TrimFileExtension();
        }

        protected override void LoadTileMapData(XDocument mapFile)
        {
            XElement mapElement = mapFile.Element(mapKey);

            Width = int.Parse(mapElement.Attribute(widthKey).Value);
            Height = int.Parse(mapElement.Attribute(heightKey).Value);

            TileWidth = int.Parse(mapElement.Attribute(tileWidthKey).Value);
            TileHeight = int.Parse(mapElement.Attribute(tileHeightKey).Value);

            string tilesetSourceName = mapElement.Element(tilesetKey).Element(imageKey).Attribute(sourceKey).Value;

            Tileset = Tileset.LoadTilesetFromImage(GetSourceImagePath(tilesetSourceName), tileSize);
            LoadMapLayers(mapElement.Descendants(layerKey).ToList());
        }

        protected override void LoadMapLayers(List<XElement> layerElements)
        {
            List<XElement> drawLayers = new List<XElement>();

            for (int i = 0; i < layerElements.Count; i++)
            {
                string name = layerElements[i].Attribute(nameKey).Value;

                if (IsMainLayer(layerElements[i]) || !name.Contains(mapChangeKey))
                {
                    drawLayers.Add(layerElements[i]);
                }

                else
                {
                    mapChanges.Add(layerElements[i]);
                }
            }

            tileMapLayers = new TileMapLayer[drawLayers.Count];

            for (int i = 0; i < drawLayers.Count; i++)
            {
                tileMapLayers[i] = new FETileMapLayer(this, drawLayers[i]);
            }

            

        }

        protected bool IsMainLayer(XElement layer)
        {

            bool value = false;
            List<XElement> properties = layer.Element(propertiesKey).Descendants().ToList();

            foreach(var prop in properties)
            {
                if (prop.Attribute(nameKey).Value.Equals(mainLayerKey))
                {
                    value = true;
                }
            }

            return value;

        }

        protected string GetRootFolder()
        {
            return rootMapsFolder + "/" + FilePath.Substring(0, FilePath.LastIndexOf('/'));
        }
        protected string GetSourceImagePath(string sourceName)
        {
            return GetRootFolder() + "/" + tilesetImageFolder + "/" + sourceName;
        }
    }
}
