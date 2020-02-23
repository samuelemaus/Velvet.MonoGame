using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Velvet.GameSystems;
using TWrite = Velvet.GameSystems.Tileset;

namespace GameSystems.PipelineExtensions
{
    [ContentTypeWriter]
    public class TSXTypeWriter : ContentTypeWriter<TWrite>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "GameSystems.PipelineExtensions.TSXReader, GameSystems.PipelineExtensions";
        }

        protected override void Write(ContentWriter output, TWrite value)
        {
            output.WriteObject<TWrite>(value);
        }
    }
}
