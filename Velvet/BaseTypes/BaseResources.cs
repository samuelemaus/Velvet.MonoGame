using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public static class BaseResources
    {
        public static void LoadContent(ContentManager _content)
        {
            content = _content;

            RectImage.DefaultRectImageTexture = content.Load<Texture2D>(RectImage.DefaultPath);


            ParticleEffect = content.Load<Effect>("Images/Effects/ParticleEffect");
            GaussianBlur = content.Load<Effect>("Images/Effects/GaussianBlur");
            Clouds = content.Load<Effect>("Images/Effects/Clouds");
            BloomExtract = content.Load<Effect>("Images/Effects/BloomExtract");
            BloomCombine = content.Load<Effect>("Images/Effects/BloomCombine");
            HighlightEffect = content.Load<Effect>("Images/Effects/HighlightEffect");






        }

        private static ContentManager content;


        public static Effect ParticleEffect;
        public static Effect GaussianBlur;
        public static Effect Clouds;
        public static Effect BloomExtract;
        public static Effect BloomCombine;
        public static Effect HighlightEffect;


    }
}
