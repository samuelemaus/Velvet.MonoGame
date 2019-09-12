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

namespace Velvet
{
    public class TestMenu : Menu
    {

        #region//Test GameData
        public TestMenu()
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

        private void SetMyPersonHP(object sender, object _value, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TestTimer.CurrentValue))
            {
                float pct = (float)_value / TestTimer.TimeRange.MaximumValue;

                int result = (int)(MyPerson.HPRange.MaximumValue * pct);

                MyPerson.HP = result;

            }
        }



        #endregion
        #region//Test UIData
        DataManager DataManager = new DataManager();

        public TestEquipment[] Equipments;
        


        #endregion
        #region//Test Images
        

        public TextImage[] EqpTextImages;

        public BasicImage Background;

        public RectImage Rect;

        public BasicImage Border;

        public RectImage BorderAsRect;


        string ScaledMouseState => InputHandler.GetMousePosition().ToString();
        string ExtMouseState => InputHandler.CurrentMouseState.Position.ToString();
        bool LeftBtnDown => InputHandler.BtnDown(MouseButtons.Left);

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

        public override void InitializeMenu()
        {
            base.InitializeMenu();
        }

        void SetNewAlignment(TextAlignment alignment)
        {
            foreach(var img in EqpTextImages)
            {
                img.Alignment = alignment;
            }
        }

        public override void ActivateMenuControls(GameTime gameTime)
        {
            Rect.MoveY(InputHandler.ScrollWheelMovementVertical * .24f);

            float speed = 10f;

            if (InputHandler.KeyPressed(Keys.Q))
            {
                if (Rect.PositionDependency != null)
                {
                    Rect.PositionDependency.CoordinateOverride = PositionCoordinateOverride.XOverride;
                }
            }


            if (InputHandler.KeyPressed(Keys.W))
            {
                if (Rect.PositionDependency != null)
                {
                    Rect.PositionDependency.CoordinateOverride = PositionCoordinateOverride.YOverride;
                }
            }


            if (InputHandler.KeyPressed(Keys.E))
            {
                Rect.AnchorTo(EqpTextImages[0], ReferencePoint.Centered, RectRelativity.Outside);
            }


            if (InputHandler.KeyPressed(Keys.C))
            {
                EqpTextImages[1].AnchorToCurrentDifferential(InputHandler.GetMousePosition);
            }

            if (InputHandler.KeyDown(Keys.L))
            {
                foreach(var img in EqpTextImages)
                {
                    img.Move(new Vector2(speed, 0));

                }
            }

            if (InputHandler.KeyDown(Keys.J))
            {
                foreach (var img in EqpTextImages)
                {
                    img.Move(new Vector2(-speed, 0));

                }
            }

            if (InputHandler.KeyDown(Keys.I))
            {
                foreach (var img in EqpTextImages)
                {
                    img.Move(new Vector2(0, -speed));

                }
            }

            if (InputHandler.KeyDown(Keys.K))
            {
                foreach (var img in EqpTextImages)
                {
                    img.Move(new Vector2(0, speed));

                }
            }

            if (InputHandler.KeyPressed(Keys.LeftShift))
            {
                EqpTextImages[0].SetDependency(InputHandler.GetMousePosition);
            }
        }

        public override void LoadContent()
        {
            Background = new BasicImage("Images/Windows/Backgrounds/EarthboundWallpaper");

            Rect = new RectImage(new BoundingRect(300, 300, 200, 400));

            Border = new BasicImage("Images/Windows/Borders/Border1");

            BorderAsRect = new RectImage(new BoundingRect(140, 140, 100, 150));

            Rect.Color = Color.DodgerBlue;

            Equipments = DataManager.LoadObjects<TestEquipment>("Equipment.csv");

            EqpTextImages = new TextImage[Equipments.Length];

            for (int i = 0; i < Equipments.Length; i++)
            {
                EqpTextImages[i] = new TextImage(Equipments[i].EquipmentName, UIController.Renderer.DefaultFont);
                //EqpTextImages[i].Scale = new Vector2(2f);
                EqpTextImages[i].Position = new Vector2(20, (i * 20));
                EqpTextImages[i].Color = Color.Aqua;
            }

            UIController.Renderer.LoadImageContent(Border);
            Border.Position = new Vector2(700, 900);
            Border.Color = Color.White;


            UIController.Renderer.LoadImageContent(Background);
            Background.Position = new Vector2(960, 540) / 2;
            Background.Color = Color.DimGray;
            Background.Scale = new Vector2(0.5f);

            UIController.Renderer.LoadImageContentFromPath(BorderAsRect, "Images/Windows/Borders/Border1");
            BorderAsRect.Color = Color.HotPink;
        }

        public override void UnloadContent()
        {

        }

        

        public override void Update(GameTime gameTime)
        {
            InputHandler.Update(gameTime);
            ActivateMenuControls(gameTime);

            if (UpdateImages)
            {
                foreach (var img in EqpTextImages)
                {
                    img.Update(gameTime);
                }
            }



        }

        bool RectHeld => RectContainsMouse && InputHandler.BtnDown(MouseButtons.Left);

        bool UpdateImages = true;

        public string MouseInfo => Mouse.GetState().Position.ToVector2().ToString();

        public bool RectContainsMouse => Rect.BoundingRect.Contains(Mouse.GetState().Position);

        void HoldImage(IMovable movable)
        {
            if (InputHandler.BtnDown(MouseButtons.Left))
            {
                movable.Position = (Mouse.GetState().Position.ToVector2());
            }
        }

        bool Hold;


        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            
            Rect.Draw(spriteBatch);

            foreach(var img in EqpTextImages)
            {
                img.Draw(spriteBatch);
            }

            Border.Draw(spriteBatch);
            BorderAsRect.Draw(spriteBatch);

            spriteBatch.DrawString(UIController.Renderer.DefaultFont, ScaledMouseState, new Vector2(10, 500), Color.Black);
            spriteBatch.DrawString(UIController.Renderer.DefaultFont, ExtMouseState, new Vector2(10, 520), Color.Pink);

        }



    }
}
