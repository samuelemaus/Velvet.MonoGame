using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Velvet.GameSystems
{
    public static class TileSetManager
    {

        public static ContentManager Content { get; set; }
        public static string TileSetDirectory { get; private set; }

        public static List<Tileset> LoadedTileSets { get; private set; } = new List<Tileset>();



        public static void Initialize(ContentManager content, string directory)
        {
            Content = content;
            TileSetDirectory = directory;
        }

        public static Tileset LoadTileset(string path)
        {
            Tileset tileset = default;
            string fullPath = Path.GetFullPath(@"..\..\..\..\" + TileSetDirectory + @"\" + path);

            try
            {
                XDocument tilesetFile = XDocument.Load(fullPath);
                XElement tilesetElement = tilesetFile.Element("tileset");
                tileset = new Tileset(tilesetElement);
                LoadedTileSets.Add(tileset);
                tileset.LoadContent();

            }

            catch 
            {


                
            }

            return tileset;
        }

        public static Tileset GetTileSetByName(string name)
        {
            Tileset tileset = null;

            bool matchFound = false;

            foreach (var t in LoadedTileSets)
            {
                if (t.Name.Equals(name))
                {
                    tileset = t;
                    matchFound = true;
                }
            }

            if (!matchFound)
            {
                throw new NullReferenceException($"No tileset with the name{name} was found.");
            }

            return tileset;
        }

    }
}
