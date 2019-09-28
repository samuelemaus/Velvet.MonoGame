using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using Velvet.UI;
using Velvet.GameSystems;
using Velvet.DataIO;
using Velvet.Input;
using Velvet.Rendering;

namespace Velvet
{
    public class TestMenu1 : Menu
    {

        #region//Test GameData
        public TestMenu1()
        {
            InitializeMenu();
        }

        public TestPerson MyPerson = new TestPerson()
        {
            Name = "Sam",
            HP = 100
        };

        public TestEquipment MyWeapon = new TestEquipment()
        {
            EquipmentName = "Durandal",
            Strength = 75
        };

        public TestEquipment MyArmor = new TestEquipment()
        {
            EquipmentName = "Tunic",
            Strength = 25
        };

        public Timer TestTimer = new Timer(0, 39, Order.Descending);





        #endregion
        #region//Test UIData
        DataManager DataManager = new DataManager();

        public TestEquipment[] Equipments;
        


        #endregion
        #region//Test Images
        

        public TextImage[] EqpTextImages;

        public BasicImage Background;

        public TextureAtlas WindowAtlas;

        public RectImage BorderAsRect;

        public WindowBackground WindowBackground;


        public RectImage[] SubdividedRects;

        TextureRegion targetRegion;

        string HandlerActivity(IInputHandler handler)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(handler.TimeInactive);

            return $"(Status: {handler.HandlerActive}, Time Inactive: {time.ToString("ss':'f")})";
        }

        Color TrueColor = Color.MediumSeaGreen, FalseColor = Color.Firebrick;
        Color BoolColor(bool value)
        {
            if (value)
            {
                return TrueColor;
            }

            else
            {
                return FalseColor;
            }
        }

        #endregion

        Rectangle reccy = new Rectangle(30,30,300,300);

        #region//Overrides
        bool UpdateImages = true;
        public override void InitializeMenu()
        {
            base.InitializeMenu();
        }

        int index = 0;
        RectImage targ => SubdividedRects[index];

        public override void ActivateMenuControls(GameTime gameTime)
        {
            ChangeToMouseDimensions();
            

            float speed = 10f;

            if (Input.Keyboard.KeyPressed(Keys.Q))
            {
                index++;
                
            }


            if (Input.Keyboard.KeyPressed(Keys.W))
            {

                index--;
            }


            if (Input.Keyboard.KeyPressed(Keys.E))
            {
                createUnion();
            }


            if (Input.Keyboard.KeyPressed(Keys.C))
            {
                targ.SetDimensionsDependency(Background, DimensionsOverrideType.HeightOverride);
                
            }

            MoveObj(targ, speed);

            if (Input.Keyboard.KeyPressed(Keys.LeftShift))
            {
                SubdividedRects[0].SetPositionDependency(Input.Mouse.GetMousePosition);
            }

            if (Input.Keyboard.KeyPressed(Keys.B))
            {
                WindowBackground.ResizeTo(new Dimensions2D(400, 300));
            }
        }

        Vector2 startPosition;
        bool resetPosition = false;
        private void ChangeToMouseDimensions()
        {
            
            Dimensions2D dimensions;

            if (drawTempRect)
            {
                if (!resetPosition)
                {
                    startPosition = Input.Mouse.GetMousePosition();

                    resetPosition = true;
                }


                Vector2 currentPosition = Input.Mouse.GetMousePosition();

                Vector2 differential = (currentPosition - startPosition);

                dimensions = new Dimensions2D(Math.Abs(differential.X), Math.Abs(differential.Y));

                var rect = new Rectangle((int)(currentPosition.X - differential.X), (int)(currentPosition.Y - differential.Y), (int)dimensions.Width, (int)dimensions.Height);

                var bRect = new BoundingRect(currentPosition - differential / 2, dimensions);

                tempRect = new RectImage(bRect)
                {
                    Alpha = 0.39f, Color = Color.Aqua
                };

                if (Input.Keyboard.KeyDown(Keys.R))
                {
                    WindowBackground.ResizeTo(dimensions);
                }

                fadeRect = tempRect;
            }

            else
            {
                tempRect = default;
                resetPosition = false;
                startPosition = Vector2.Zero;
                dimensions = Dimensions2D.Empty;

                if(fadeRect != null)
                {
                    fadeOut(fadeRect, 0.119f);
                }

            }


        }
        private void fadeOut(ITransparent transparent, float speed)
        {
            drawFadeRect = true;

            transparent.Alpha -= speed;

            if(transparent.Alpha <= 0)
            {
                drawFadeRect = false;
            }
        }

        private bool drawFadeRect;

        private bool drawTempRect => Input.Mouse.BtnDown(MouseButtons.Left);
        private RectImage tempRect;
        private RectImage fadeRect;

        private void toggleAnchor()
        {
            if (targ.PositionDependency != null)
            {
                targ.PositionDependency = null;
            }
            else
            {
                targ.AnchorTo(Background, ReferencePoint.BottomRight, RectRelativity.Inside, 10);
            }
         
        }

        private void MoveObj(IMovable mov, float spd)
        {
            if (Input.Keyboard.KeyDown(Keys.L))
            {
                mov.MoveX(spd);
            }

            if (Input.Keyboard.KeyDown(Keys.J))
            {
                mov.MoveX(-spd);
            }

            if (Input.Keyboard.KeyDown(Keys.I))
            {
                mov.MoveY(-spd);
            }

            if (Input.Keyboard.KeyDown(Keys.K))
            {
                mov.MoveY(spd);
            }
        }

        public override void LoadContent()
        {
            Background = new BasicImage("Images/Windows/Backgrounds/EarthboundWallpaper");
            BorderAsRect = new RectImage(new BoundingRect(140, 140, 100, 150));
            Equipments = DataManager.LoadObjects<TestEquipment>("Equipment.csv");

            EqpTextImages = new TextImage[Equipments.Length];

            for (int i = 0; i < Equipments.Length; i++)
            {
                EqpTextImages[i] = new TextImage(Equipments[i].EquipmentName, UIController.Renderer.DefaultFont);
                //EqpTextImages[i].Scale = new Vector2(2f);
                EqpTextImages[i].Position = new Vector2(20, (i * 20));
                EqpTextImages[i].Color = Color.Aqua;
            }

            UIController.Renderer.LoadImageContent(Background);
            Background.Position = new Vector2(960, 540) / 2;
            Background.Color = Color.DimGray;
            Background.Scale = new Vector2(0.5f);

            UIController.Renderer.LoadImageContentFromPath(BorderAsRect, "Images/Windows/WindowTexture1");
            BorderAsRect.Color = Color.HotPink;

            Rectangle[] targetRects = reccy.SubdivideToGrid(2,7);

            SubdividedRects = new RectImage[targetRects.Length];


            Color color = Color.DarkSeaGreen;

            for (int i = 0; i < targetRects.Length; i++)
            {


                SubdividedRects[i] = new RectImage(targetRects[i]);

                float pct = ((float)i / (float)targetRects.Length);

                int r = (int)(90 * pct);
                int g = (int)(205 * pct);
                int b = (int)(255 * pct);

                SubdividedRects[i].Color = new Color(r, g, b);


            }

            WindowAtlas = new TextureAtlas(UI.UIController.Renderer.Content.Load<Texture2D>("Images/Windows/WindowTexture1"), 3);

            targetRegion = WindowAtlas.Regions[0];

           


        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            Input.Update(gameTime);
            ActivateMenuControls(gameTime);

            if (UpdateImages)
            {
                foreach (var img in EqpTextImages)
                {
                    img.Update(gameTime);
                }
            }

            Background.Update(gameTime);
            WindowBackground.Update(gameTime);

            foreach (var r in SubdividedRects)
            {
                r.Update(gameTime);
            }

            
            
            if(intersectImages.Count != 0)
            {
                foreach (var img in intersectImages)
                {
                    img.Update(gameTime);
                }
            }

            

            if (drawUnion)
            {
                unionRect.Update(gameTime);
            }

            WindowBackground.Update(gameTime);

            if (drawFadeRect)
            {
                fadeRect.Update(gameTime);
            }

            if (drawTempRect)
            {
                tempRect.Update(gameTime);
            }


    }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);



            //foreach(var img in EqpTextImages)
            //{
            //    img.Draw(spriteBatch);
            //}



            BorderAsRect.Draw(spriteBatch);

            foreach(var img in SubdividedRects)
            {
                img.Draw(spriteBatch);
            }

            LabelGrid(spriteBatch);

            if(tempRect != null)
            {
                spriteBatch.DrawString(UIController.Renderer.DefaultFont, tempRect.Position.ToString() + ", " + tempRect.Dimensions.ToString(), new Vector2(10, 500), Color.Black);
            }
            
            spriteBatch.DrawString(UIController.Renderer.DefaultFont, startPosition.ToString()+ ", " + Input.Mouse.GetMousePosition().ToString(), new Vector2(10, 520), Color.Pink);



            spriteBatch.Draw(UIResources.WindowTextures.SourceTexture, new Vector2(110, 160), targetRegion.SourceRect, Color.HotPink);



            if (drawUnion)
            {
                unionRect.Draw(spriteBatch);
            }

            WindowBackground.Draw(spriteBatch);

            if (drawFadeRect)
            {
                fadeRect.Draw(spriteBatch);
            }

            if (drawTempRect)
            {
                tempRect.Draw(spriteBatch);
            }

            DrawIntersects(spriteBatch);

        }
        #endregion

        private void LabelGrid(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < SubdividedRects.Length; i++)
            {

                spriteBatch.DrawString(UIController.Renderer.DefaultFont, i.ToString(), SubdividedRects[i].Position + Vector2.One, Color.Black);
                spriteBatch.DrawString(UIController.Renderer.DefaultFont, i.ToString(), SubdividedRects[i].Position, Color.White);
            }
        }


        private List<BoundingRect> intersects
        {
            get
            {
                var returnList = new List<BoundingRect>();

                for (int i = 0; i < SubdividedRects.Length; i++)
                {
                    if (tempRect != null && tempRect.BoundingRect.Intersects(SubdividedRects[i].BoundingRect))
                    {
                        returnList.Add(BoundingRect.Intersect(tempRect.BoundingRect, SubdividedRects[i].BoundingRect));
                    }
                }

                return returnList;
            }
        }

        private List<RectImage> intersectImages
        {
            get
            {
                var returnList = new List<RectImage>();

                for (int i = 0; i<intersects.Count; i++)
                {
                    Color color = new Color((i + 1) * 45, (i + 1) * 10, (i + 1) * 22);

        returnList.Add(new RectImage(intersects[i])
        {
            Color = color,
                        Alpha = 0.65f
                    });
                }

                return returnList;

            }
        }


        private void DrawIntersects(SpriteBatch spriteBatch)
        {
            if(tempRect != null)
            {
                spriteBatch.DrawString(UIController.Renderer.DefaultFont, $"tempRect: {tempRect.ToString()}, {tempRect.BoundingRect.ToString()}", new Vector2(500, 0), Color.HotPink);
            }
            

            spriteBatch.DrawString(UIController.Renderer.DefaultFont, $"Images: {intersectImages.Count}", new Vector2(850, 0), Color.HotPink);


            for (int i = 0; i < intersectImages.Count; i++)
            {
                spriteBatch.DrawString(UIController.Renderer.DefaultFont, $"Target intersects SubdividedRects[{i.ToString()}], Intersect Created: {intersects[i].ToString()}", new Vector2(400, 18 * (i + 1)), intersectImages[i].Color);
                intersectImages[i].Draw(spriteBatch);

            }

        }

        bool drawUnion = false;

        void createUnion()
        {


            //unionRect = new RectImage(BoundingRect.Union(targ.BoundingRect, SubdividedRects[0].BoundingRect)) { Color = Color.LawnGreen, Alpha = 0.55f };

            

            unionRect = new RectImage(BoundingRect.Union(SubdividedRects)) { Color = Color.LemonChiffon, Alpha = 0.65f };
            unionRect.SetRegion(UIResources.WindowTextures[ReferencePoint.BottomLeft]);
            drawUnion = true;

        }

        RectImage unionRect;

    }
}
