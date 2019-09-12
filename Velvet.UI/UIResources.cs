using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Velvet.UI
{
    public static class UIResources
    {
        public static void LoadContent(ContentManager content)
        {
            _content = content;
            DefaultFont = _content.Load<SpriteFont>("Fonts/Monaco");
            RectImage.DefaultRectImageTexture = _content.Load<Texture2D>(RectImage.DefaultPath);
        }

        private static ContentManager _content;

        public static SpriteFont DefaultFont;

    }
}
