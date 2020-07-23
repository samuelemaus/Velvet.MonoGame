using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Velvet.GameSystems;

namespace Velvet.FETest
{
    public class FETileMapLayer : TileMapLayer
    {
        protected static string tileKey = "tile";
        protected static string gidKey = "gid";
        

        public FETileMapLayer()
        {

        }

        public FETileMapLayer(FETileMap parent, XElement layerData)
        {
            parentMap = parent;
            WidthInTiles = int.Parse(layerData.Attribute(widthKey).Value);
            HeightInTiles = int.Parse(layerData.Attribute(heightKey).Value);
            LayerWidth = WidthInTiles * parentMap.TileWidth;
            LayerHeight = HeightInTiles * parentMap.TileHeight;
            Name = layerData.Attribute(nameKey).Value;

            XElement dataElement = layerData.Element(dataKey);

            var tileGids = dataElement.Descendants(tileKey).ToArray();

            int[] tileIndexes = new int[tileGids.Length];
            for (int i = 0; i < tileIndexes.Length; i++)
            {
                string gid = tileGids[i].Attribute(gidKey).Value;
                tileIndexes[i] = int.Parse(gid);
            }
            Position = Vector2.Zero;
            Origin = Vector2.Zero;

            boundingRect = new BoundingRect(0, 0, WidthInTiles * parentMap.TileWidth, HeightInTiles * parentMap.TileHeight);
            DrawMethod = DrawRenderTarget;
            InitializeTiles(tileIndexes);

        }

    }
}
