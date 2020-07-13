using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.UI;
using Velvet;
using Velvet.FETest.FE.Systems.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace Velvet.FETest
{
    public class TestMapSceneUI : Menu
    {
        public TestMapSceneUI(MapScene mapScene)
        {
            SceneData = mapScene;
        }

        int resolutionMultiplier;

        int GetInitialMultiplier(Dimensions2D resolution)
        {
            return (int)resolution.Width / 16;
        }

        MapScene SceneData { get; set; }

        SpriteFont general;
        SpriteFont dialogue;
        SpriteFont monaco;

        SpriteFont drawFont;
        Color FontColor = Color.White;

        public RectImage Background;


        #region//Controls

        public override void ActivateMenuControls(GameTime gameTime)
        {
            base.ActivateMenuControls(gameTime);
            ToggleFontColor();
            IncrementResolutionMultiplierControls();
            if (Input.Keyboard.KeyPressed(Keys.L))
            {
                SceneData.CursorRectImage.SetOriginByReferencePoint(Alignment.BottomRight);
            }
        }

        private void IncrementResolutionMultiplierControls()
        {
            if (Input.Keyboard.KeyPressed(Keys.OemCloseBrackets))
            {
                IncrementResolutionMultiplier(1);
            }

            if (Input.Keyboard.KeyPressed(Keys.OemOpenBrackets))
            {
                IncrementResolutionMultiplier(-1);
            }
        }

        private void IncrementResolutionMultiplier(int amt)
        {
            resolutionMultiplier = ValueRange.Enforce(resolutionMultiplier + amt, new ValueRange(1, GetMaxMultiplier()));

            GameRenderer.Instance.InternalResolution = GetNewResolution(resolutionMultiplier);
        }

        private int GetMaxMultiplier()
        {
            Dimensions2D maxRes = GameRenderer.Instance.DisplayResolution;

            return (int)maxRes.Width / 16;
        }

        private Dimensions2D GetNewResolution(int multiplier)
        {
            return new Dimensions2D(16 * multiplier, 9 * multiplier);
        }

        private void ToggleFont()
        {
            if (Input.Keyboard.KeyPressed(Keys.M))
            {
                if(drawFont == general)
                {
                    drawFont = dialogue;
                }

                else
                {
                    drawFont = general;
                }
            }
        }

        private void ToggleFontColor()
        {
            if (Input.Keyboard.KeyPressed(Keys.LeftShift))
            {
                int currentIndex = fontColors.IndexOf(FontColor);

                int next = ValueRange.Enforce(currentIndex + 1, new ValueRange(0, fontColors.Count - 1), true);

                FontColor = fontColors[next];
            }

            if (Input.Keyboard.KeyPressed(Keys.LeftControl))
            {
                int currentIndex = fontColors.IndexOf(FontColor);

                int next = ValueRange.Enforce(currentIndex - 1, new ValueRange(0, fontColors.Count - 1), true);

                FontColor = fontColors[next];
            }

        }

        private List<Color> fontColors = new List<Color>()
        {
            Color.White, Color.Black, Color.Blue, Color.Aqua, Color.Red, Color.Green, Color.Yellow
        };

        

        #endregion

        //Draw Debug

        private string GetMouseInfo()
        {
            return $"transform: {SceneData.Input.Mouse.GetMousePosition()} | raw: {SceneData.Input.Mouse.CurrentMouseState.Position}";
        }

        private string GetMouseInfo2()
        {
            return $"stw{SceneData.ScreenToWorld} | wts {SceneData.WorldToScreen}";
        }
        private string GetTileMapInfo()
        {
            return $"{SceneData.Map.FilePath} | {SceneData.Map.TileMap.TileCount} ({SceneData.Map.TileMap.TileWidth} x {SceneData.Map.TileMap.TileHeight})";
        }
        private string GetCrossInfo()
        {
            return $"L:{horLineLeftTxt}, R:{horLineRightTxt}, T:{verLineTopTxt}, B:{verLineBottomTxt}";
        }
        private string GetRectInfo()
        {
            return SceneData.Movable.ToString();
        }

        private string GetCameraInfo()
        {
            return $"{SceneData.Camera} | {SceneData.Camera.DisplayBounds} | {SceneData.Camera.DisplayRanges}";
        }

        private string GetResolutionInfo()
        {
            return $"Internal Res:{GameRenderer.Instance.InternalResolution} | Mult: {resolutionMultiplier}";
        }

        private string GetTilesViewable()
        {
            return $"h{GetHorizontalTilesViewable()}, v: {GetVerticalTilesViewable()}";
        }

        private double GetHorizontalTilesViewable()
        {
            return SceneData.Camera.ViewableArea.Dimensions.Width / 16;
            //return (int)Math.Ceiling(SceneData.Camera.ViewableArea.Dimensions.Width / 16);
        }

        private double GetVerticalTilesViewable()
        {
            return SceneData.Camera.ViewableArea.Dimensions.Height / 16;
            //return (int)Math.Ceiling(SceneData.Camera.ViewableArea.Dimensions.Height / 16);
        }
        private List<String> GetDefaultList()
        {
            return new List<string>() {GetMouseInfo(), GetMouseInfo2(), GetCameraInfo(), GetTilesViewable(), GetResolutionInfo() };
        }

        private List<String> activeList;
        private void DrawDebugInfo(List<String> strings, SpriteBatch spriteBatch)
        {
            if(strings != null && strings.Count > 0)
            {
                for (int i = 0; i < strings.Count; i++)
                {
                    int xPos = 5;
                    int yPos = (i + 1) * 15;

                    Vector2 position = new Vector2(xPos, yPos);

                    spriteBatch.DrawString(drawFont, strings[i], position, FontColor);
                }
            }
        }

        public override void LoadContent()
        {
            general = UIController.Instance.Content.Load<SpriteFont>("Fonts/gba-fe-general");
            dialogue = UIController.Instance.Content.Load<SpriteFont>("Fonts/gba-fe-dialogue");
            monaco = UIController.Instance.Content.Load<SpriteFont>("Fonts/Monaco");
            InitializeBackground();
            GameRenderer.Instance.InternalResolutionChanged += this.OnInternalResolutionChanged;

            Background.SetOriginByReferencePoint(Alignment.TopCentered);

            activeList = new List<string>();
            drawFont = monaco;
            resolutionMultiplier = GetInitialMultiplier(GameRenderer.Instance.InternalResolution);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ActivateMenuControls(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            DrawDebugInfo(GetDefaultList(), spriteBatch);
        }

        private void InitializeBackground()
        {
            Background = new RectImage(UIController.Instance.Renderer.Bounds)
            {
                Color = Color.DarkBlue,
                Alpha = 0.47f
            };
        }

        public void OnInternalResolutionChanged(object sender, EventArgs e)
        {
            InitializeBackground();
        }

        private void DrawCross(Vector2 position, int size, Color color, SpriteBatch spriteBatch)
        {


            Vector2 horLineLeft = new Vector2(position.X - (size / 2), position.Y);
            Vector2 horLineRight = new Vector2(position.X + (size / 2), position.Y);

            Vector2 verLineTop = new Vector2(position.X, position.Y - (size / 2));
            Vector2 verLineBottom = new Vector2(position.X, position.Y + (size / 2));

            horLineLeftTxt = horLineLeft.ToString();
            horLineRightTxt = horLineRight.ToString();
            verLineTopTxt = verLineTop.ToString();
            verLineBottomTxt = verLineBottom.ToString();

            spriteBatch.DrawLine(horLineLeft, horLineRight, color);
            spriteBatch.DrawLine(verLineTop, verLineBottom, color);

            spriteBatch.PutPixel(position, color);
        }

        string horLineLeftTxt;
        string horLineRightTxt;
        string verLineTopTxt;
        string verLineBottomTxt;

    }
}
