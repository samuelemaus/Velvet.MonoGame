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

namespace Velvet
{
    public class TestTopDownScene : GameScene
    {
        public TestTopDownScene() : base("Content/Scenes")
        {
            SceneMenu = new TestHud(this);
        }

        public TestHud SceneMenu { get; set; }

        #region//Images     

        public BasicImage Beach = new BasicImage("Beach");
        public BasicImage Ocean = new BasicImage("Ocean");
        public BasicImage Lucas = new BasicImage("Lucas");

        public RectImage TempRect;
        public RectImage FadeRect;

        public List<IDrawableObject> TemporaryImages = new List<IDrawableObject>();

        #endregion

        #region//Overrides

        #region//Controls

        public ReferencePoint LucasRefPoint = ReferencePoint.Centered;

        int refPointIndex = ReferencePoint.ReferencePoints.IndexOf(ReferencePoint.Centered);
        float zoomSpeed = 0.25f / 3;
        float rotationSpeed = .001f;
        float moveSpeed = 2;

        double deltaTime;
        public override void ActivateSceneControls()
        {
            ChangeScenes();
            CheckSomething();
            MoveObjectWithKey(moveObject, moveSpeed);

            

            if (Input.Keyboard.KeyPressed(Keys.B))
            {
                GameRenderer.ToggleFullScreen();
            }

            if (Input.Keyboard.KeyPressed(Keys.D))
            {
                ToggleLucasRefPoint();
            }

            if (Input.Keyboard.KeyPressed(Keys.R))
            {
                Camera.ZoomTool.SetGradualZoom(3, 400f);
            }

            if (Input.Keyboard.KeyDown(Keys.F))
            {
                Camera.ZoomTool.SetGradualZoom(1, 400f);
            }

            

            if (Input.Keyboard.KeyPressed(Keys.Q))
            {
                div++;
            }

            if (Input.Keyboard.KeyPressed(Keys.A))
            {
                div--;
            }

            if (Input.Keyboard.KeyDown(Keys.D6))
            {
                Camera.InstantRotation(-rotationSpeed);
            }

            if (Input.Keyboard.KeyPressed(Keys.D7))
            {
                Camera.ScrollTool.InitiateInfiniteScroll(Direction.Right, 1.75f);
            }


            //SetCameraDependency();


            if(Input.Mouse.ScrollWheelMovementVertical != 0)
            {
                Camera.ZoomTool.IncrementZoom(Input.Mouse.ScrollWheelMovementVertical * .0025f);
            }

            DrawCreatedRect();

        }

        void ToggleLucasRefPoint()
        {
            refPointIndex = ValueRange.Enforce(refPointIndex + 1, 0, ReferencePoint.ReferencePoints.Count -1, true);

            Lucas.SetOriginByReferencePoint(ReferencePoint.ReferencePoints[refPointIndex]);
        }
        
        void DrawCreatedRect()
        {
            if(drawTempRect)
            {
                TempRect = new RectImage(Input.Mouse.Pointer.CreatedRect)
                {
                    Alpha = 0.39f,
                    Color = Color.Aqua
                };

                FadeRect = TempRect;
            }

            else
            {
                TempRect = default;
                if(FadeRect != null)
                {
                    FadeOut(FadeRect, 0.119f);
                }
            }
        }

        bool drawTempRect => Input.Mouse.BtnDown(MouseButtons.Left);

        bool drawFadeRect = false;
        void FadeOut(ITransparent transparent, float speed)
        {
            drawFadeRect = true;

            transparent.Alpha -= speed;

            if (transparent.Alpha <= 0)
            {
                drawFadeRect = false;
            }
        }

        public PositionDependency LucasDependency;
        public PositionDependency MouseDependency;

        private Vector2 GetCreatedRectPosition()
        {
            return Input.Mouse.Pointer.CreatedRect.Position;
        }


        IMovable moveObject;

        public Vector2 GetTranslatedMousePosition()
        {
            Vector2 rounded = (Input.Mouse.CurrentMouseState.Position / SceneController.Renderer.TargetScale).Round();
            return Camera.Center + Vector2.Transform(rounded, Matrix.Invert(MouseTransform));
        }

        public Matrix MouseTransform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(Vector2.Zero, 0)) *
                    Matrix.CreateRotationZ(Camera.Rotation) *
                    Matrix.CreateScale(new Vector3(Camera.Zoom, Camera.Zoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(Camera.Center, 0));

            }
        }

        public int div = 2;

        #endregion
        public override void LoadContent()
        {
            
            this.LoadImageContent(Beach);
            this.LoadImageContent(Ocean);
            this.LoadImageContent(Lucas);

            moveObject = Lucas;            
            
            Beach.Origin = Vector2.Zero;
            Beach.Position = Vector2.Zero;
            
            Lucas.Position = SceneController.Renderer.TargetDimensions.Center;
            Ocean.Position = new Vector2(170, 0);

            LucasDependency = new MovablePositionDependency(Lucas);
            MouseDependency = new MethodPositionDependency(GetCreatedRectPosition);

            /*Camera = new OrthoCamera(SceneController.Renderer.Viewport);*/

            Camera.SetPositionDependencyWithInterrupt(LucasDependency, false);
            Camera.WorldBounds = new BoundingRect(Beach.BoundingRect.Dimensions.Center, Beach.BoundingRect.Dimensions);
            UIController.ChangeMenu(SceneMenu);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Beach.Draw(spriteBatch);
            Lucas.Draw(spriteBatch);
            TemporaryImages.DrawCollection(spriteBatch);

            if (drawTempRect)
            {
                TempRect.Draw(spriteBatch);
            }

            if (drawFadeRect)
            {
                FadeRect.Draw(spriteBatch);
            }

            spriteBatch.DrawRectangle(Lucas.BoundingRect.ToRectangle(), Color.Teal * 0.79f);
            spriteBatch.DrawCircle(Lucas.BoundingRect.Position, 3f, 5, Color.Black* 0.65f);
            spriteBatch.DrawCircle(Lucas.Position, 3f, 5, Color.DarkGoldenrod * 0.65f);

        }

        public override void Update(GameTime gameTime)
        {
            deltaTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update(gameTime);
            Camera.Update(gameTime);

            Beach.Update(gameTime);
            Lucas.Update(gameTime);
            TemporaryImages.UpdateCollection(gameTime);
            SetColorByZoom();
            if(TempRect!= null)
            {
                TempRect.Update(gameTime);
            }
            if (FadeRect != null)
            {
                FadeRect.Update(gameTime);
            }
            var spobles = Beach.Position;
        }

        private void SetColorByZoom()
        {
            Color blangus = RenderingExtensions.GetInterpolatedColor(Color.MistyRose, Color.DeepPink, Camera.Zoom / (Camera.ZoomRange.MaximumValue * 2.5f));

            Beach.Color = blangus;
            Lucas.Color = blangus;
        }

        


        #endregion

    }
}
