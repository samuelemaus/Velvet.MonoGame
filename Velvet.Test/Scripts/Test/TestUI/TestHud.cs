using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velvet.UI;
using Velvet.DataIO;
using Velvet.Rendering;
using Velvet.Input;
using Velvet.GameSystems;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Velvet
{
    public class TestHud : Menu
    {
        public TestHud(VelvetTestGame game, TestTopDownScene scene = null)
        {
            GameData = game;
            
            SceneData = scene;

            drawInfos.Add(DrawCameraInfo);
            drawInfos.Add(DrawRectInfo);
            drawInfos.Add(DrawSceneInfo);
            drawInfos.Add(DrawResolutions);


            
        }

        SpriteFont font;
        SpriteFont smallFont;
        SpriteFont largeFont;

        TestTopDownScene SceneData { get; set; }

        public TextImage[] PeopleTextImages;
        public DataManager Manager = new DataManager();
        public TestPerson[] People;

        public TextImage[] RendererTextImages;
        public List<IDrawableObject> TemporaryImages;
        public VelvetTestGame GameData { get; private set; }

        public TestTopDownScene TopDownScene { get; set; }

        public OrthoCamera Camera;

        public RectImage Background;

        public ButtonView.ButtonCompositeImage ButtonCompositeImage;

        Color[] randomColors = new Color[10];

        string targetRectString
        {
            get
            {

                if (Input.Mouse.Pointer.TargetedObject != null && Input.Mouse.Pointer.TargetedObject is IBoundingRect r)
                {
                    return r.BoundingRect.ToString();
                }

                else
                {
                    return "...";
                }
            }
        }
        string drawnRectString
        {
            get
            {
                if (drawnRect != null)
                {
                    return drawnRect.BoundingRect.ToString();
                }

                else
                {
                    return "...";
                }

            }
        }

        RectImage drawnRect;
        void GetDrawnRect()
        {
            foreach (var img in TemporaryImages)
            {
                if (img.BoundingRect.Contains(Input.Mouse.Pointer.Position))
                {
                    drawnRect = img as RectImage;
                }
            }
        }

        List<RectImage> debugOverlays
        {
            get
            {
                var returnList = new List<RectImage>();

                foreach(var p in PeopleTextImages)
                {
                    RectImage next = new RectImage(p.BoundingRect)
                    {
                        Color = randomColors[8],
                        Alpha = 0.244f
                    };

                    returnList.Add(next);
                }

                return returnList;
            }
        }

        #region//Overrides

        public override void ActivateMenuControls(GameTime gameTime)
        {
            if (Input.Keyboard.KeyPressed(Keys.E))
            {
                SceneController.Renderer.AddSceneEffect(BaseResources.GaussianBlur);
            }

            if (Input.Keyboard.KeyPressed(Keys.I))
            {
                SceneController.Renderer.AddSceneEffect(BaseResources.BloomCombine);
            }

            if (Input.Keyboard.KeyPressed(Keys.O))
            {
                SceneController.Renderer.AddSceneEffect(BaseResources.BloomExtract);
            }

            if (Input.Keyboard.KeyPressed(Keys.W))
            {
                SceneController.Renderer.ClearEffects();
            }

            

            if (Input.Keyboard.KeyPressed(Keys.K))
            {
                foreach (var p in PeopleTextImages)
                {
                    p.Alignment = TextAlignment.Left;
                }
            }

            if (Input.Keyboard.KeyPressed(Keys.L))
            {
                foreach (var p in PeopleTextImages)
                {
                    p.Alignment = TextAlignment.Center;
                }
            }

            if (Input.Keyboard.KeyPressed(Keys.OemSemicolon))
            {
                foreach (var p in PeopleTextImages)
                {
                    p.Alignment = TextAlignment.Right;
                }
            }

            if (Input.Mouse.BtnClicked(MouseButtons.Right))
            {
                infoIndex = ValueRange.Enforce(infoIndex + 1, 0, drawInfos.Count -1, true);
            }
        }



        public override void LoadContent()
        {
            for (int i = 0; i < randomColors.Length; i++)
            {
                randomColors[i] = RenderingExtensions.GetRandomColor(110);
                Thread.Sleep(10);
            }

            Camera = SceneController.Renderer.Camera;

            TemporaryImages = new List<IDrawableObject>();

            People = Manager.LoadObjects<TestPerson>("Persons.csv");

            PeopleTextImages = new TextImage[People.Length];

            font = UIController.Renderer.DefaultFont;
            smallFont = UIResources.SmallFont;
            largeFont = UIResources.LargeFont;

            Background = new RectImage(UIController.Renderer.Bounds)
            {
                Color = Color.DarkBlue,
                Alpha = 0.5f
            };

            ButtonCompositeImage = new ButtonView.ButtonCompositeImage(new BoundingRect(100, 20, 32, 8));


            TextAlignment alignment = TextAlignment.Right;

            PeopleTextImages[0] = new TextImage(People[0].ToString())
            {
                Color = Color.Red,
                Font = font,
                Alignment = alignment
            };

            PeopleTextImages[0].AnchorTo(UIController.Renderer.Bounds, ReferencePoint.TopRight, RectRelativity.Inside, 5);
            

            for (int i = 1; i < People.Length; i++)
            {
                PeopleTextImages[i] = new TextImage(People[i].ToString())
                {
                    Color = Color.Red,
                    Font = font,
                    Alignment = alignment
                };

                PeopleTextImages[i].AnchorTo(PeopleTextImages[i - 1], ReferencePoint.BottomCentered, RectRelativity.Outside, 5);
                
            }

            Input.SetPointerTargets(PeopleTextImages);

            foreach(var p in PeopleTextImages)
            {
                p.DrawRectImage(ref TemporaryImages);
            }


            var CameraInfo = Camera.DebugMemberInfoList();

        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Background.Update(gameTime);

            ActivateMenuControls(gameTime);

            PeopleTextImages.UpdateCollection(gameTime);
            

            ButtonCompositeImage.Update(gameTime);

            GetDrawnRect();

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);

            PeopleTextImages.DrawCollection(spriteBatch);

            //DrawCameraInfo(spriteBatch);

            TemporaryImages.DrawCollection(spriteBatch);



            DrawInfo.Invoke(spriteBatch);

            
        }



        #endregion

        public delegate void DrawMethod(SpriteBatch spriteBatch);
        public DrawMethod DrawInfo => drawInfos[infoIndex];

        private void DrawCameraInfo(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"Pos {Camera.Position}, Targ Pos: {Camera.ScrollTool.targetPosition}", new Vector2(0, 5), randomColors[0]);
            spriteBatch.DrawString(font, $"TargReached: {Camera.ScrollTool.TargetReached}, Scrolling: {Camera.ScrollTool.Scrolling}", new Vector2(0, 20), randomColors[0]);
            spriteBatch.DrawString(font, $"PosDep: {Camera.PositionDependency}, Active: {Camera.PositionDependency.DependencyActive}", new Vector2(0, 35), randomColors[0]);
            spriteBatch.DrawString(font, $"Viewport: {Camera.Viewport}, ViewableArea: {Camera.ViewableArea}, WorldBounds: {Camera.WorldBounds}", new Vector2(0, 50), randomColors[5]);
            spriteBatch.DrawString(font, $"GraphicsDevice: {GameRenderer.GraphicsDevice.Adapter.Description}, GD Viewport: {GameRenderer.GraphicsDevice.Viewport}", new Vector2(0, 65), randomColors[5]);

        }

        private void DrawRectInfo(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"Ptr Pos: {Input.Mouse.Pointer.Position}, Translated: {SceneData.GetTranslatedMousePosition()}", new Vector2(5, 5), randomColors[0]);
            spriteBatch.DrawString(font, $"Div: {SceneData.div}, Matrix: {SceneData.MouseTransform}", new Vector2(5, 15), randomColors[0]);
            spriteBatch.DrawString(font, $"[0]: BR:{PeopleTextImages[0].BoundingRect}, DP{PeopleTextImages[0].DrawPosition}", new Vector2(5, 30), randomColors[0]);
            spriteBatch.DrawString(font, $"Ptr Obj Bounds: {targetRectString}, Ptr DR Bounds: {drawnRectString}", new Vector2(5, 45), randomColors[3]);


        }

        private void DrawSceneInfo(SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(font, $"Beach Pos: {SceneData.Beach.Position}, Origin: {SceneData.Beach.Origin}, OD: {SceneData.Beach.OriginDifferential}, DP: {SceneData.Beach.DrawPosition}", new Vector2(5, 5), randomColors[0]);
            spriteBatch.DrawString(font, $"Lucas Pos: {SceneData.Lucas.Position}, Origin: {SceneData.Lucas.Origin}, OD: {SceneData.Lucas.OriginDifferential}", new Vector2(5, 15), randomColors[0]);
            spriteBatch.DrawString(font, $"Camera Pos: {Camera.Position}", new Vector2(5, 30), randomColors[2]);

        }

        private void DrawResolutions(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameRenderer.AvailableScreenResolutions.Count; i++)
            {
                spriteBatch.DrawString(font, $"{GameRenderer.AvailableScreenResolutions[i].ToString()}", new Vector2(5, (i+1)*15), randomColors[3]);
            }
        }

        private List<DrawMethod> drawInfos = new List<DrawMethod>();
        private int infoIndex = 0;

        Vector2 Ldiff => SceneData.Lucas.Position - SceneData.Lucas.OriginDifferential;
        Vector2 Bdiff => SceneData.Beach.Position - SceneData.Beach.OriginDifferential;

    }
}
