using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.GameSystems;
using Microsoft.Xna.Framework.Input;
using C3.XNA;
using System.IO;

namespace Velvet.FETest.FE.Systems.Scenes
{
    public class MapScene : GameScene
    {

        #region//Constants
        public static string defaultContentLocation = "Content/Maps";
        #endregion

        #region//Constructors
        public MapScene()
        {
            InitializeContent(defaultContentLocation);
        }

        public MapScene(string mapName)
        {
            Map = new ChapterMap(mapName);
            InitializeCamera();

        }
        #endregion

        #region//Content
        public ChapterMap Map { get; private set; }
        public RectImage CursorRectImage { get; set; }

        public RectImage GoodGuyRectImage { get; set; }
        public RectImage EnemyRectImage { get; set; }

        public uint guid;

        public IMovable Movable { get; set; }

        public Vector2 GetTranslatedMousePosition()
        {
            Vector2 rounded = (Input.Mouse.CurrentMouseState.Position / SceneController.Instance.Renderer.TargetScale).Round();
            return Camera.InitialCenterPoint + Vector2.Transform(rounded, Matrix.Invert(MouseTransform));
        }

        public Matrix MouseTransform
        {
            get
            {
                return  Matrix.CreateTranslation(new Vector3(Vector2.Zero, 0)) *
                        Matrix.CreateRotationZ(Camera.Rotation) *
                        Matrix.CreateScale(new Vector3(Camera.Zoom, Camera.Zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(Camera.InitialCenterPoint, 0));

            }
        }

        public Vector2 ScreenToWorld
        {
            get
            {
                return Camera.ScreenToWorld(Input.Mouse.CurrentMouseState.Position);
            }
        }

        public Vector2 WorldToScreen
        {
            get
            {
                return Camera.ScreenToWorld(Input.Mouse.CurrentMouseState.Position);
            }
        }

        #endregion

        #region//Overrides
        protected override void InitializeCamera()
        {
            base.InitializeCamera();
            
        }

        public override void ActivateSceneControls(GameTime gameTime)
        {
            base.ActivateSceneControls(gameTime);

            MoveObjectWithKey(Movable, 230, gameTime);
            ZoomCamera();
            SetMoveObject();
            if (Input.Keyboard.KeyPressed(Keys.P))
            {
                TakeAllScreenshots();
            }

            if (Input.Keyboard.KeyPressed(Keys.Z))
            {
                GameRenderer.Instance.ToggleFullScreen();
            }

            if (Input.Keyboard.KeyPressed(Keys.Q))
            {
                GameRenderer.Instance.ConstrainToAspectRatio = !GameRenderer.Instance.ConstrainToAspectRatio;
            }
        }

        private void ZoomCamera()
        {
            if (Input.Keyboard.KeyPressed(Keys.R))
            {
                Camera.ZoomTool.SetGradualZoom(3, 120f);
            }

            if (Input.Keyboard.KeyDown(Keys.F))
            {
                Camera.ZoomTool.SetGradualZoom(2, 120f);
            }

            if (Input.Keyboard.KeyDown(Keys.V))
            {
                Camera.ZoomTool.SetGradualZoom(1, 120f);
            }
        }
        
        private void SetMoveObject()
        {
            if (Input.Keyboard.KeyPressed(Keys.G))
            {
                Movable = GoodGuyRectImage;
                SnapCamera();
            }

            if (Input.Keyboard.KeyPressed(Keys.T))
            {
                Movable = EnemyRectImage;
                SnapCamera();
            }

            if (Input.Keyboard.KeyPressed(Keys.B))
            {
                Movable = CursorRectImage;
                SnapCamera();
            }
        }

        private void SnapCamera()
        {
            if(Movable == this.CursorRectImage)
            {
                Camera.SetPositionDependencyWithInterrupt(new MovablePositionDependency(Movable), false);
            }

            else
            {
                Camera.SetPositionDependencyWithInterrupt(new MovablePositionDependency(Movable));
            }
        }


        public override void LoadContent()
        {
            Map.LoadContent();
            BoundingRect bounds = new BoundingRect(Map.TileMap.BoundingRect.Dimensions.Center, Map.TileMap.BoundingRect.Dimensions);
            Camera.WorldBounds = bounds;
            CursorRectImage = new RectImage(new BoundingRect(110, 110, 16, 16))
            {
                Color = Color.LightYellow,
                Alpha = .85f
            };

            GoodGuyRectImage = new RectImage(new BoundingRect(304, 144, 16, 16))
            {
                Color = Color.Aqua,
                Alpha = .85f
            };

            EnemyRectImage = new RectImage(new BoundingRect(16, 16, 16, 16))
            {
                Color = Color.OrangeRed, Alpha = .85f
            };

            Movable = CursorRectImage;

            Camera.SetPositionDependencyWithInterrupt(new MovablePositionDependency(CursorRectImage), false);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Map.TileMap.Draw(spriteBatch);
            CursorRectImage.Draw(spriteBatch);
            GoodGuyRectImage.Draw(spriteBatch);
            EnemyRectImage.Draw(spriteBatch);
            DrawCross(Camera.Position, 16, Color.HotPink, spriteBatch);
            DrawCross(GetTranslatedMousePosition(), 16, Color.Turquoise, spriteBatch);
            DrawCross(Camera.ScreenToWorld(Input.Mouse.CurrentMouseState.Position) / SceneController.Instance.Renderer.TargetScale, 16, Color.Gold, spriteBatch);
            DrawCross(Camera.ScreenToWorld(Input.Mouse.CurrentMouseState.Position) / SceneController.Instance.Renderer.TargetScale, 16, Color.Black, spriteBatch);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Camera.Update(gameTime);

        }
        #endregion

        #region//Debug

        private void TakeAllScreenshots()
        {

            string basePath = "C:\\Users\\Sam\\Documents\\";
            string ext = ".png";

            foreach (var renderer in GameRenderer.Instance.Renderers)
            {
                
                string name = renderer.GetType().Name;

                string fullPath = $"{basePath}\\{name}{ext}";

                FileStream stream = new FileStream(fullPath, FileMode.Create);

                renderer.RenderTarget.SaveAsPng(stream, renderer.RenderTarget.Width, renderer.RenderTarget.Height);

                stream.Close();
            }

            string gameRendererPath = $"{basePath}\\{nameof(GameRenderer)}{ext}";

            GameRenderer.Instance.BaseRenderTarget.SaveAsPng(new FileStream(gameRendererPath, FileMode.Create), GameRenderer.Instance.BaseRenderTarget.Width, GameRenderer.Instance.BaseRenderTarget.Height);

        }

        private void DrawCross(Vector2 position, int size, Color color, SpriteBatch spriteBatch)
        {


            Vector2 horLineLeft = new Vector2(position.X - (size / 2), position.Y);
            Vector2 horLineRight = new Vector2(position.X + (size / 2), position.Y);

            Vector2 verLineTop = new Vector2(position.X, position.Y - (size / 2));
            Vector2 verLineBottom = new Vector2(position.X, position.Y + (size / 2));

           

            spriteBatch.DrawLine(horLineLeft, horLineRight, color);
            spriteBatch.DrawLine(verLineTop, verLineBottom, color);

            //spriteBatch.PutPixel(position, color);
        }

        #endregion



    }
}
