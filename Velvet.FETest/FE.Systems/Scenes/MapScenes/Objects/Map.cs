using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.GameSystems;

namespace Velvet.FETest.FE.Systems.Scenes.MapScenes.Objects
{
    public class Map
    {

        public Map()
        {

        }

        public Map(string _tilemapName)
        {
            tilemapName = _tilemapName;
        }

        string tilemapName;

        public TileMap TileMap { get; set; }
        private void InitializeTileMap()
        {
            TileMap = new TileMap(tilemapName);
        }

        public void LoadContent()
        {

        }

    }
}
