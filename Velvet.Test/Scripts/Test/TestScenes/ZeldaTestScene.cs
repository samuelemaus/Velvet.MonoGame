//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Velvet.GameSystems;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Velvet.UI;
//using Velvet.Input;
//using C3.XNA;
//using System.Xml.Linq;
//using System.Xml;
//using Velvet.Rendering;

//namespace Velvet
//{
//    public class ZeldaTestScene : GameWorldScene
//    {
//        public ZeldaTestScene() : base()
//        {
//            //SceneMenu = new ZeldaHud(this);
//            World = new GameWorld("zeldaMap1.tmx");
//        }

//        //public ZeldaHud SceneMenu { get; set; }
//        public GameSettingsMenu GameSettingsMenu { get; set; }

//        Tileset Tileset { get; set; }
//        TileMap DefaultMap { get; set; }
//        public CharacterObject Link { get; set; } = new CharacterObject("linkAtlas");

//        public BoundingTriangle RightAngle { get; set; } = new BoundingTriangle(new Vector2(25, 25), new Vector2(25, 75), new Vector2(75, 25));

//        public int RegionIndex { get; private set; } = 0;

//        public override void LoadContent()
//        {
//            Tileset = TileSetManager.LoadTileset("zelda_gbc.tsx");
//            DefaultMap = new TileMap("zeldaMap1.tmx");


//            World.GameObjects.Add(Link);
//            World.Camera.SetPositionDependencyWithInterrupt(new MovablePositionDependency(Link), false);
//            World.LoadContent();

//            SetCharacterTags(Link.Image.TextureAtlas);
//            Link.Position = World.WorldBounds.Dimensions.Center;
//            //UIController.Instance.ChangeMenu(SceneMenu);
//        }

//        float playerLayerDepth = 0.3f;
//        public int sides = 24;

//        public override void Draw(SpriteBatch spriteBatch)
//        {

//            World.Draw(spriteBatch);
//            //Primitives2D.DrawLine(spriteBatch, RightAngle.VertexA, RightAngle.VertexB, Color.HotPink);
//            //Primitives2D.DrawLine(spriteBatch, RightAngle.VertexB, RightAngle.VertexC, Color.Teal);
//            //Primitives2D.DrawLine(spriteBatch, RightAngle.VertexC, RightAngle.VertexA, Color.Yellow);
//            //Primitives2D.DrawCircle(spriteBatch, Link.Position, Link.Image.BoundingRect.Dimensions.Width / 3, sides, Color.Teal);
//            //Primitives2D.PutPixel(spriteBatch, CirclePosition, Color.HotPink);
//            //Primitives2D.PutPixel(spriteBatch, Link.Image.Position, Color.HotPink);

//        }

//        public Vector2 CirclePosition => new Vector2(Link.Position.X, Link.Image.BoundingRect.Bottom/* - (Link.Image.BoundingRect.Dimensions.VerticalCenter / 2)*/);
//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);
//            World.Update(gameTime);
//        }

//        public float baseSpeed = 1.0f;
//        public float fastSpeed = 2.0f;
//        public float moveSpeed = 1.0f;

//        public override void ActivateSceneControls()
//        {
//            ChangeScenes();
//            CheckSomething();
//            //MoveCharacterWithKey(Link, moveSpeed);
//            //ChangeRegionIndex();

//            if (Input.Keyboard.KeyPressed(Keys.R))
//            {
//                World.Camera.ZoomTool.SetGradualZoom(3, 200f);
//            }

//            if (Input.Keyboard.KeyDown(Keys.F))
//            {
//                World.Camera.ZoomTool.SetGradualZoom(2, 200f);
//            }

//            if (Input.Keyboard.KeyDown(Keys.V))
//            {
//                World.Camera.ZoomTool.SetGradualZoom(1, 150f);
//            }

//            if (Input.Keyboard.KeyDown(Keys.LeftShift))
//            {
//                moveSpeed = fastSpeed;
//            }

//            if (Input.Keyboard.KeyReleased(Keys.LeftShift))
//            {
//                moveSpeed = baseSpeed;
//            }

//            if (Input.Mouse.BtnClicked(MouseButtons.Left))
//            {
//                UIController.Instance.ChangeMenu(GameSettingsMenu);
//            }

//            //if (Input.Mouse.BtnClicked(MouseButtons.Right))
//            //{
//            //    UIController.Instance.ChangeMenu(SceneMenu);
//            //}


//            ToggleRendererModes();




//        }

//        private void ToggleRendererModes()
//        {
//            if (Input.Keyboard.KeyPressed(Keys.Q))
//            {
//                SceneController.Instance.Renderer.ToggleRasterizerState();

//            }

//            if (Input.Keyboard.KeyPressed(Keys.A))
//            {
//                SceneController.Instance.Renderer.ToggleRasterizerState(true);
//            }


//            if (Input.Keyboard.KeyPressed(Keys.W))
//            {
//                SceneController.Instance.Renderer.ToggleSamplerState();
//            }

//            if (Input.Keyboard.KeyPressed(Keys.S))
//            {
//                SceneController.Instance.Renderer.ToggleSamplerState(true);
//            }


//            if (Input.Keyboard.KeyPressed(Keys.E))
//            {
//                SceneController.Instance.Renderer.ToggleBlendState();
//            }

//            if (Input.Keyboard.KeyPressed(Keys.D))
//            {
//                SceneController.Instance.Renderer.ToggleBlendState(true);
//            }

//            if (Input.Keyboard.KeyPressed(Keys.T))
//            {
//                SceneController.Instance.Renderer.ToggleSpriteSortMode();
//            }

//            if (Input.Keyboard.KeyPressed(Keys.G))
//            {
//                SceneController.Instance.Renderer.ToggleSpriteSortMode(true);
//            }


//        }


//        public override void UnloadContent()
//        {
//            base.UnloadContent();
//        }

//        private void SetCharacterTags(TextureAtlas atlas)
//        {
//            atlas.Regions[0].AddTag(Direction.Down);
//            atlas.Regions[6].AddTag(Direction.Up);
//            atlas.Regions[12].AddTag(Direction.Right);
//            atlas.Regions[18].AddTag(Direction.Left);
//        }
//    }
//}
