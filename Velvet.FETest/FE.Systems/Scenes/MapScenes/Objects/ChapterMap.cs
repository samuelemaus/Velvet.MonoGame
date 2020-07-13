using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.GameSystems;

namespace Velvet.FETest
{
    public class ChapterMap
    {

        public ChapterMap()
        {

        }

        public ChapterMap(string filePath)
        {
            FilePath = filePath;
            InitializeTileMap();
        }

        public string FilePath { get; private set; }

        public FETileMap TileMap { get; set; }
        private void InitializeTileMap()
        {
            TileMap = new FETileMap(FilePath);

        }

        public void LoadContent()
        {
            TileMap.LoadContent();
        }



    }
}
