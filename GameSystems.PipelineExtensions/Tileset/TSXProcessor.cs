using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml.Linq;
using System.IO;
using TInput = System.Xml.Linq.XDocument;
using TOutput = Velvet.GameSystems.Tileset;
using Velvet.GameSystems;

namespace GameSystems.PipelineExtensions
{
    [ContentProcessor(DisplayName = "GameSystems.PipelineExtensions.TSXProcessor")]
    public class TSXProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            Tileset tileset = default;
            XDocument tilesetFile = input as XDocument;
            XElement tilesetElement = tilesetFile.Element("tileset");
            tileset = new Tileset(tilesetElement);
            return tileset;
        }
    }
}