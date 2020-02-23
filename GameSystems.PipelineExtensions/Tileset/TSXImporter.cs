using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = System.Xml.Linq.XDocument;
using TOutput = Velvet.GameSystems.Tileset;

namespace GameSystems.PipelineExtensions
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".tsx", DisplayName = "TSX Importer", DefaultProcessor = "ContentProcessor1")]
    public class TSXImporter : ContentImporter<TInput>
    {

        
        
        public override TInput Import(string filename, ContentImporterContext context)
        {
            
            return XDocument.Load(filename);
        }

    }

}
