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
            DefaultFont = content.Load<SpriteFont>("Fonts/Monaco");
            SmallFont = content.Load<SpriteFont>("Fonts/SpecialMessage");
            LargeFont = content.Load<SpriteFont>("Fonts/MonacoLarge");
            

            InitializeWindowTextures();
            InitializeSmallControlTextures();


            Effect = content.Load<Effect>("Images/Effects/HighlightEffect");

        }

        private static ContentManager content;

        public static SpriteFont DefaultFont;
        public static SpriteFont SmallFont;
        public static SpriteFont LargeFont;

        #region//UI Textures

        public static TextureAtlas WindowTextures;
        private static void InitializeWindowTextures()
        {
            WindowTextures = new TextureAtlas(content.Load<Texture2D>("Images/Windows/WindowTexture1"), 3);

            WindowTextures.Regions[0].AddTag(ReferencePoint.TopLeft);
            WindowTextures.Regions[1].AddTag(ReferencePoint.TopCentered);
            WindowTextures.Regions[2].AddTag(ReferencePoint.TopRight);
            WindowTextures.Regions[3].AddTag(ReferencePoint.LeftCentered);
            WindowTextures.Regions[4].AddTag(ReferencePoint.Centered);
            WindowTextures.Regions[5].AddTag(ReferencePoint.RightCentered);
            WindowTextures.Regions[6].AddTag(ReferencePoint.BottomLeft);
            WindowTextures.Regions[7].AddTag(ReferencePoint.BottomCentered);
            WindowTextures.Regions[8].AddTag(ReferencePoint.BottomRight);
        }

        public static TextureAtlas SmallControlTextures;
        private static void InitializeSmallControlTextures()
        {
            SmallControlTextures = new TextureAtlas(content.Load<Texture2D>("Images/SmallControls1"), 4);


            //SmallButtons
            SmallControlTextures.Regions[0].AddTags(XReference.Left, ControlObjectState.Default);
            SmallControlTextures.Regions[1].AddTags(XReference.Center, ControlObjectState.Default);
            SmallControlTextures.Regions[2].AddTags(XReference.Right, ControlObjectState.Default);
            SmallControlTextures.Regions[4].AddTags(XReference.Left, ControlObjectState.Active);
            SmallControlTextures.Regions[5].AddTags(XReference.Center, ControlObjectState.Active);
            SmallControlTextures.Regions[6].AddTags(XReference.Right, ControlObjectState.Active);
            SmallControlTextures.Regions[8].AddTags(XReference.Left, ControlObjectState.Targeted);
            SmallControlTextures.Regions[9].AddTags(XReference.Center, ControlObjectState.Targeted);
            SmallControlTextures.Regions[10].AddTags(XReference.Right, ControlObjectState.Targeted);

        }

        #endregion

        #region//Effects

        public static Effect Effect;

        #endregion

    }
}
