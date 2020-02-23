using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.GameSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velvet.UI;
using Velvet.Input;
using C3.XNA;
using System.Xml.Linq;
using System.Xml;

namespace Velvet
{
    public class ZeldaTestScene : GameScene
    {
        public ZeldaTestScene() : base()
        {
            SceneMenu = new ZeldaHud(this);
        }


        public ZeldaHud SceneMenu { get; set; }

        Tileset Tileset { get; set; }
        TileMap DefaultMap { get; set; }
        public BasicImage Tile;
        public RectImage Background;
        public BasicImage GoldenrodCity = new BasicImage("goldenrod_city");
        public int RegionIndex { get; private set; } = 0;

        public override void LoadContent()
        {
            Tileset = TileSetManager.LoadTileset("zelda_gbc.tsx");
            DefaultMap = new TileMap("zeldaMap1.tmx");
            Tile = new BasicImage(Tileset.TextureAtlas.Regions[RegionIndex]);
            

            this.LoadImageContent(GoldenrodCity);
            GoldenrodCity.Origin = Vector2.Zero;
            GoldenrodCity.Position = Vector2.Zero;
            GoldenrodCity.Alpha = 0.25f;

            BoundingRect tempMapRect = new BoundingRect(0,0, 16 * 22, 16 * 14);

            World.TileMap = DefaultMap;
            World.LoadContent();

            Camera.SetPositionDependencyWithInterrupt(new MovablePositionDependency(Tile), false);
            Camera.WorldBounds = new BoundingRect(World.TileMap.BoundingRect.Dimensions.Center, World.TileMap.BoundingRect.Dimensions);

            Tile.Position = Camera.WorldBounds.Dimensions.Center;

            Background = new RectImage(Camera.WorldBounds);
            Background.Color = Color.Aqua;
            Background.Alpha = 0.63f;


            UIController.ChangeMenu(SceneMenu);           

        }

        GameWorld World { get; set; }  = new GameWorld();

        float playerLayerDepth = 0.3f;

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Background.Draw(spriteBatch);
            
            World.TileMap.Draw(spriteBatch);
            Tile.Draw(spriteBatch);
            spriteBatch.DrawRectangle(Background.BoundingRect.ToRectangle(), Color.HotPink);
        }

        public override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
            Camera.Update(gameTime);
            Tile.Update(gameTime);
        }

        float moveSpeed = 1;

        public override void ActivateSceneControls()
        {
            ChangeScenes();
            CheckSomething();
            MoveObjectWithKey(Tile, moveSpeed);
            ChangeRegionIndex();

            if (Input.Keyboard.KeyPressed(Keys.R))
            {
                Camera.ZoomTool.SetGradualZoom(2.5f, 200f);
            }

            if (Input.Keyboard.KeyDown(Keys.F))
            {
                Camera.ZoomTool.SetGradualZoom(2, 200f);
            }

            if (Input.Keyboard.KeyDown(Keys.V))
            {
                Camera.ZoomTool.SetGradualZoom(1, 200f);
            }

            if (Input.Keyboard.KeyDown(Keys.LeftShift))
            {
                moveSpeed = 3;
            }

            if (Input.Keyboard.KeyReleased(Keys.LeftShift))
            {
                moveSpeed = 1;
            }
        }

        private void ChangeRegionIndex()
        {
            if (Input.Keyboard.KeyPressed(Keys.I))
            {
                RegionIndex = ValueRange.Enforce(RegionIndex + 1, new ValueRange(0, Tileset.TextureAtlas.Regions.Length - 1), true);
                Tile.SetRegion(Tileset.TextureAtlas.Regions[RegionIndex]);

            }

            if (Input.Keyboard.KeyPressed(Keys.U))
            {
                RegionIndex = ValueRange.Enforce(RegionIndex - 1, new ValueRange(0, Tileset.TextureAtlas.Regions.Length - 1), true);
                Tile.SetRegion(Tileset.TextureAtlas.Regions[RegionIndex]);
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

    }
}
