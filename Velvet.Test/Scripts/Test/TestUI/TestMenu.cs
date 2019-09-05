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

        public TextImage TextImage1 = new TextImage("Blue sticky gloves",UserInterface.Renderer.DefaultFont);

        public TextImage[] EqpTextImages;

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

        public override void ActivateMenuControls()
        {
            float speed = 35f;

            if (InputHandler.KeyPressed(Keys.Q))
            {
                SetNewAlignment(TextAlignment.Left);
            }


            if (InputHandler.KeyPressed(Keys.W))
            {
                SetNewAlignment(TextAlignment.Center);
            }


            if (InputHandler.KeyPressed(Keys.E))
            {
                SetNewAlignment(TextAlignment.Right);
            }


            if (InputHandler.KeyDown(Keys.C))
            {
                TextImage1.Move(new Vector2(4,10));
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

            if (InputHandler.KeyPressed(Keys.B))
            {
                UpdateImages = !UpdateImages;
            }
        }

        

        public override void LoadContent()
        {
            TextImage1.SetPosition(new Vector2(500, 500));
            TextImage1.SetColor(Color.HotPink);

            Equipments = DataManager.LoadObjects<TestEquipment>("Equipment.csv");

            EqpTextImages = new TextImage[Equipments.Length];

            for (int i = 0; i < Equipments.Length; i++)
            {
                EqpTextImages[i] = new TextImage(Equipments[i].EquipmentName, UserInterface.Renderer.DefaultFont);
                EqpTextImages[i].SetPosition(new Vector2(20, (i * 20)));
                EqpTextImages[i].SetColor(Color.Aqua);
            }


        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.Update(gameTime);
            ActivateMenuControls();

            if (UpdateImages)
            {
                foreach (var img in EqpTextImages)
                {
                    img.Update(gameTime);
                }
            }

        }

        bool UpdateImages = true;

        public override void Draw(SpriteBatch spriteBatch)
        {
            TextImage1.Draw(spriteBatch);

            foreach(var img in EqpTextImages)
            {
                img.Draw(spriteBatch);
            }

            spriteBatch.DrawString(UserInterface.Renderer.DefaultFont, EqpTextImages[0].Alignment.ToString(), new Vector2(5, 700), Color.White);
            

        }



    }
}
