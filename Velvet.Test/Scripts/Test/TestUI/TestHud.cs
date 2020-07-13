//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Velvet.UI;
//using Velvet.DataIO;
//using Velvet.Rendering;
//using Velvet.Input;
//using Velvet.GameSystems;
//using Microsoft.Xna.Framework.Input;
//using System.Threading;
//using C3.XNA;

//namespace Velvet
//{
//    public class TestHud : Menu
//    {
//        public TestHud(TestTopDownScene scene)
//        {
            
//            SceneData = scene;

//            drawInfos.Add(DrawCameraInfo);
//            drawInfos.Add(DrawRectInfo);
//            drawInfos.Add(DrawSceneInfo);
//            drawInfos.Add(DrawResolutions);
//            drawInfos.Add(DrawTextView);

//        }

//        SpriteFont font;
//        SpriteFont smallFont;
//        SpriteFont largeFont;

//        TestTopDownScene SceneData { get; set; }

//        public TextImage[] PeopleTextImages;
//        public DataManager Manager = new DataManager();
//        public TestPerson[] People;

//        public TextImage[] RendererTextImages;
//        public List<IDrawableObject> TemporaryImages;
        
//        public TestTopDownScene TopDownScene { get; set; }

//        public OrthoCamera Camera;

//        public RectImage Background;

//        public TextView Person1TextView { get; set; }

//        Color[] randomColors = new Color[10];

//        string targetRectString
//        {
//            get
//            {

//                if (Input.Mouse.Pointer.TargetedObject != null && Input.Mouse.Pointer.TargetedObject is IBoundingRect r)
//                {
//                    return r.BoundingRect.ToString();
//                }

//                else
//                {
//                    return "...";
//                }
//            }
//        }
//        string drawnRectString
//        {
//            get
//            {
//                if (drawnRect != null)
//                {
//                    return drawnRect.BoundingRect.ToString();
//                }

//                else
//                {
//                    return "...";
//                }

//            }
//        }

//        RectImage drawnRect;
//        void GetDrawnRect()
//        {
//            foreach (var img in TemporaryImages)
//            {
//                if (img.BoundingRect.Contains(Input.Mouse.Pointer.CenterPosition))
//                {
//                    drawnRect = img as RectImage;
//                }
//            }
//        }

//        List<RectImage> debugOverlays
//        {
//            get
//            {
//                var returnList = new List<RectImage>();

//                foreach(var p in PeopleTextImages)
//                {
//                    RectImage next = new RectImage(p.BoundingRect)
//                    {
//                        Color = randomColors[8],
//                        Alpha = 0.244f
//                    };

//                    returnList.Add(next);
//                }

//                return returnList;
//            }
//        }

//        #region//Overrides

//        public override void ActivateMenuControls(GameTime gameTime)
//        {
//            if (Input.Keyboard.KeyPressed(Keys.U))
//            {
//                People[0].HP -= 10;
//            }

//            if (Input.Keyboard.KeyPressed(Keys.I))
//            {
//                People[0].HP += 10;
//            }

//            if (Input.Keyboard.KeyPressed(Keys.O))
//            {
                
//            }

//            if (Input.Keyboard.KeyPressed(Keys.W))
//            {
                
//            }

            

//            if (Input.Keyboard.KeyPressed(Keys.K))
//            {
//                foreach (var p in PeopleTextImages)
//                {
//                    p.Alignment = TextAlignment.Left;
//                }
//            }

//            if (Input.Keyboard.KeyPressed(Keys.L))
//            {
//                foreach (var p in PeopleTextImages)
//                {
//                    p.Alignment = TextAlignment.Center;
//                }
//            }

//            if (Input.Keyboard.KeyPressed(Keys.OemSemicolon))
//            {
//                foreach (var p in PeopleTextImages)
//                {
//                    p.Alignment = TextAlignment.Right;
//                }
//            }

//            if (Input.Mouse.BtnClicked(MouseButtons.Right))
//            {
//                infoIndex = ValueRange.Enforce(infoIndex + 1, 0, drawInfos.Count -1, true);
//            }

//            if (Input.Keyboard.KeyPressed(Keys.Z))
//            {
//                resToggle = !resToggle;

//                GameRenderer.Instance.SetResolution(toggledRes);
//            }

            
//        }


//        bool resToggle = false;
//        Dimensions2D otherRes = new Dimensions2D(1280, 720);
//        Dimensions2D toggledRes
//        {
//            get
//            {
//                if (resToggle)
//                {
//                    return otherRes;
//                }

//                else
//                {
//                    return GameRenderer.Instance.DisplayResolution;
//                }
//            }
//        }

//        public override void LoadContent()
//        {
//            for (int i = 0; i < randomColors.Length; i++)
//            {
//                randomColors[i] = RenderingExtensions.GetRandomColor(110);
//                Thread.Sleep(10);
//            }

//            Camera = SceneController.Instance.Renderer.Camera;

//            TemporaryImages = new List<IDrawableObject>();

//            People = Manager.LoadObjects<TestPerson>("Persons.csv");

//            PeopleTextImages = new TextImage[People.Length];

//            font = UIController.Instance.Renderer.DefaultFont;
//            smallFont = UIResources.SmallFont;
//            largeFont = UIResources.LargeFont;

//            Background = new RectImage(UIController.Instance.Renderer.Bounds)
//            {
//                Color = Color.DarkBlue,
//                Alpha = 0.5f
//            };

//            TextAlignment alignment = TextAlignment.Left;

//            PeopleTextImages[0] = new TextImage(People[0].ToString())
//            {
//                Color = Color.Red,
//                Font = font,
//                Alignment = alignment
//            };

//            PeopleTextImages[0].AnchorTo(UIController.Instance.Renderer.Bounds, Alignment.TopRight, RectRelativity.Inside, 5);
            

//            for (int i = 1; i < People.Length; i++)
//            {
//                PeopleTextImages[i] = new TextImage(People[i].ToString())
//                {
//                    Color = Color.Red,
//                    Font = font,
//                    Alignment = alignment
//                };

//                PeopleTextImages[i].AnchorTo(PeopleTextImages[i - 1], Alignment.BottomCentered, RectRelativity.Outside, 5);
                
//            }

//            Input.SetPointerTargets(PeopleTextImages);

//            foreach(var p in PeopleTextImages)
//            {
//                p.DrawRectImage(ref TemporaryImages);
//            }


//            Person1TextView = new TextView();

//            Person1TextView.BindTo(People[0], People[0].HP, nameof(TestPerson.HP));
//            Person1TextView.Image.CenterPosition = UIController.Instance.Renderer.TargetDimensions.Center;

//            TextImage t = Person1TextView.Image as TextImage;

//            t.Alignment = TextAlignment.Center;

//            Person1TextView.Image.Color = Color.HotPink;


            
//        }
//        public override void UnloadContent()
//        {
//            base.UnloadContent();
//        }
//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);

//            Background.Update(gameTime);

//            ActivateMenuControls(gameTime);

//            PeopleTextImages.UpdateCollection(gameTime);

//            GetDrawnRect();

            
//        }
//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            Background.Draw(spriteBatch);

//            PeopleTextImages.DrawCollection(spriteBatch);

//            //DrawCameraInfo(spriteBatch);

//            TemporaryImages.DrawCollection(spriteBatch);

//            DrawInfo.Invoke(spriteBatch);

//            Person1TextView.Draw(spriteBatch);

//            DrawDebugRects(spriteBatch);
            
//        }
//        #endregion

//        public delegate void DrawMethod(SpriteBatch spriteBatch);
//        public DrawMethod DrawInfo => drawInfos[infoIndex];

//        private void DrawDebugRects(SpriteBatch spriteBatch)
//        {
//            foreach(var p in PeopleTextImages)
//            {
//                spriteBatch.DrawRectangle(p.BoundingRect.ToRectangle(), Color.Honeydew * 0.45f);
//            }
//        }

//        private void DrawCameraInfo(SpriteBatch spriteBatch)
//        {
//            if(Camera.PositionDependency != null)
//            {
//                spriteBatch.DrawString(font, $"Pos {Camera.CenterPosition}, Targ Pos: {Camera.ScrollTool.targetPosition}", new Vector2(0, 5), randomColors[0]);
//                spriteBatch.DrawString(font, $"TargReached: {Camera.ScrollTool.TargetReached}, Scrolling: {Camera.ScrollTool.Scrolling}", new Vector2(0, 20), randomColors[0]);
//                spriteBatch.DrawString(font, $"PosDep: {Camera.PositionDependency}, Active: {Camera.PositionDependency.DependencyActive}", new Vector2(0, 35), randomColors[0]);
//                spriteBatch.DrawString(font, $"ViewableArea: {Camera.ViewableArea}, WorldBounds: {Camera.WorldBounds}", new Vector2(0, 50), randomColors[5]);
//                spriteBatch.DrawString(font, $"GraphicsDevice: {GameRenderer.Instance.GraphicsDevice.Adapter.Description}, GD Viewport: {GameRenderer.Instance.GraphicsDevice.Viewport}", new Vector2(0, 65), randomColors[5]);
//            }
//        }

//        private void DrawRectInfo(SpriteBatch spriteBatch)
//        {
//            spriteBatch.DrawString(font, $"Ptr Pos: {Input.Mouse.Pointer.CenterPosition}, Translated: {SceneData.GetTranslatedMousePosition()}", new Vector2(5, 5), randomColors[0]);
//            spriteBatch.DrawString(font, $"Div: {SceneData.div}, Matrix: {SceneData.MouseTransform}", new Vector2(5, 15), randomColors[0]);
//            spriteBatch.DrawString(font, $"[0]: BR:{PeopleTextImages[0].BoundingRect}, Pos: {PeopleTextImages[0].CenterPosition}, Algn: {PeopleTextImages[0].Alignment}, Org: {PeopleTextImages[0].Origin}", new Vector2(5, 30), randomColors[0]);
//            spriteBatch.DrawString(font, $"Ptr Obj Bounds: {targetRectString}, Ptr DR Bounds: {drawnRectString}", new Vector2(5, 45), randomColors[3]);


//        }
//        private void DrawSceneInfo(SpriteBatch spriteBatch)
//        {
            
//            spriteBatch.DrawString(font, $"Beach Pos: {SceneData.Beach.CenterPosition}, Origin: {SceneData.Beach.Origin}, OD: {SceneData.Beach.OriginDifferential}, BR: {SceneData.Beach.BoundingRect}", new Vector2(5, 5), randomColors[0]);
//            spriteBatch.DrawString(font, $"Lucas Pos: {SceneData.Lucas.CenterPosition}, , Origin: {SceneData.Lucas.Origin}, OD: {SceneData.Lucas.OriginDifferential}, BR: {SceneData.Lucas.BoundingRect}", new Vector2(5, 15), randomColors[0]);
//            spriteBatch.DrawString(font, $"Camera Pos: {Camera.CenterPosition}", new Vector2(5, 30), randomColors[2]);

//        }
//        private void DrawResolutions(SpriteBatch spriteBatch)
//        {
//            spriteBatch.DrawString(font, $"{GameRenderer.Instance.ScreenResolution}", new Vector2(5, 5), randomColors[3]);

//            for (int i = 0; i < GameRenderer.Instance.AvailableScreenResolutions.Count; i++)
//            {
//                spriteBatch.DrawString(font, $"{GameRenderer.Instance.AvailableScreenResolutions[i].ToString()}", new Vector2(5, (i+2)*15), randomColors[3]);
//            }
//        }
//        private void DrawTextView(SpriteBatch spriteBatch)
//        {
//            spriteBatch.DrawString(font, $"Pos: {Person1TextView.Image.CenterPosition}, DimCent{Person1TextView.Image.Dimensions.HorizontalCenter}", new Vector2(5, 5), Color.White);
//        }

//        private List<DrawMethod> drawInfos = new List<DrawMethod>();
//        private int infoIndex = 0;

//        Vector2 Ldiff => SceneData.Lucas.CenterPosition - SceneData.Lucas.OriginDifferential;
//        Vector2 Bdiff => SceneData.Beach.CenterPosition - SceneData.Beach.OriginDifferential;

//    }
//}
