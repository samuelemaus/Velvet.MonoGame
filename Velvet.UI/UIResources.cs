using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Velvet.Rendering;

namespace Velvet.UI
{
    public static class UIResources
    {
        public static void LoadContent(ContentManager _content)
        {
            content = _content;
            //DefaultFont = content.Load<SpriteFont>("Fonts/Monaco");
            //SmallFont = content.Load<SpriteFont>("Fonts/SpecialMessage");
            //LargeFont = content.Load<SpriteFont>("Fonts/MonacoLarge");
            

            //InitializeWindowTextures();
            //InitializeSmallControlTextures();


            //Effect = content.Load<Effect>("Images/Effects/HighlightEffect");

        }

        private static ContentManager content;

        public static SpriteFont DefaultFont;
        public static SpriteFont SmallFont;
        public static SpriteFont LargeFont;
        public static SpriteFont DialogueFont;

        #region//UI Textures

        public static TextureAtlas WindowTextures;
        private static void InitializeWindowTextures()
        {
            WindowTextures = new TextureAtlas(content.Load<Texture2D>("Images/Windows/WindowTexture1"), 3);

            WindowTextures.Regions[0].AddTag(Alignment.TopLeft);
            WindowTextures.Regions[1].AddTag(Alignment.TopCentered);
            WindowTextures.Regions[2].AddTag(Alignment.TopRight);
            WindowTextures.Regions[3].AddTag(Alignment.LeftCentered);
            WindowTextures.Regions[4].AddTag(Alignment.Centered);
            WindowTextures.Regions[5].AddTag(Alignment.RightCentered);
            WindowTextures.Regions[6].AddTag(Alignment.BottomLeft);
            WindowTextures.Regions[7].AddTag(Alignment.BottomCentered);
            WindowTextures.Regions[8].AddTag(Alignment.BottomRight);
        }

        public static TextureAtlas SmallControlTextures;
        private static void InitializeSmallControlTextures()
        {
            SmallControlTextures = new TextureAtlas(content.Load<Texture2D>("Images/SmallControls1"), 4);


            //SmallButtons
            SmallControlTextures.Regions[0].AddTags(HorizontalAlignment.Left, ControlObjectState.Default);
            SmallControlTextures.Regions[1].AddTags(HorizontalAlignment.Center, ControlObjectState.Default);
            SmallControlTextures.Regions[2].AddTags(HorizontalAlignment.Right, ControlObjectState.Default);
            SmallControlTextures.Regions[4].AddTags(HorizontalAlignment.Left, ControlObjectState.Active);
            SmallControlTextures.Regions[5].AddTags(HorizontalAlignment.Center, ControlObjectState.Active);
            SmallControlTextures.Regions[6].AddTags(HorizontalAlignment.Right, ControlObjectState.Active);
            SmallControlTextures.Regions[8].AddTags(HorizontalAlignment.Left, ControlObjectState.Targeted);
            SmallControlTextures.Regions[9].AddTags(HorizontalAlignment.Center, ControlObjectState.Targeted);
            SmallControlTextures.Regions[10].AddTags(HorizontalAlignment.Right, ControlObjectState.Targeted);

        }

        #endregion

        #region//Effects

        public static Effect Effect;

        #endregion

    }
}
