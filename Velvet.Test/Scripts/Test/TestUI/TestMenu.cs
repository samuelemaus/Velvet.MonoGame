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
        public ToggleButton GlassesToggle;

        public MenuText TimerText;

        public MenuText MyPersonHP;

        public ProgressBar TimerProgressBar;

        #endregion

        #region//Test Images

        RectImage RectImageTest;

        #endregion

        public override void InitializeMenu()
        {
            base.InitializeMenu();

            GlassesToggle = new ToggleButton(MyPerson, MyPerson.WearsGlasses, nameof(MyPerson.WearsGlasses));

            TimerText = new MenuText(TestTimer, TestTimer.TimeSpan, nameof(TestTimer.TimeSpan), new Vector2(300, 300), 2, Color.HotPink, @"mm\:ss");

            TimerProgressBar = new ProgressBar(TestTimer, TestTimer.CurrentValue, nameof(TestTimer.CurrentValue), new ReferenceRect(new Rectangle(960, 960, 200, 40)), new List<Color> { Color.Red, Color.Yellow, Color.Green, Color.CadetBlue }, TestTimer.TimeRange, ProgressBarFillType.Default, ProgressBarFillColorBehavior.Dynamic);

            MyPersonHP = new MenuText(MyPerson, MyPerson.HP, nameof(MyPerson.HP), new Vector2(300, 330), 2, Color.YellowGreen);

        }

        public override void ActivateMenuControls()
        {
            base.ActivateMenuControls();

            if (InputHandler.KeyPressed(Keys.Enter))
            {
                GlassesToggle.OnObjectSelected();
            }

            if (InputHandler.KeyPressed(Keys.Space))
            {
                TestTimer.StartTimer();

            }

            if (InputHandler.KeyPressed(Keys.X))
            {
                TestTimer.ResetToDefault();
            }

            MoveThing(TimerText.Image);

        }

        private void MoveThing(Image2D img)
        {
            float speed = 19f;

            if (InputHandler.KeyDown(Keys.Up))
            {
                img.Position.Y-= speed;
            }

            if (InputHandler.KeyDown(Keys.Down))
            {
                img.Position.Y += speed;
            }

            if (InputHandler.KeyDown(Keys.Left))
            {
                img.Position.X -= speed;
            }

            if (InputHandler.KeyDown(Keys.Right))
            {
                img.Position.X += speed;
            }
        }

        public override void LoadContent()
        {
            TimerText.LoadContent();

            RectImageTest = new RectImage(new ReferenceRect(new Rectangle(220, 780, 210, 400)))
            {
                Color = Color.Teal,
                Alpha = 0.37f
            };

            RectImageTest.LoadContent();

            RectImageTest.AnchorTo(TimerText.Image, RectRelativity.Inside, ReferencePoint.Centered, 30);

            TimerProgressBar.LoadContent();

            MyPersonHP.LoadContent();

            TestTimer.PropertyChanged += this.SetMyPersonHP;

            
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            TestTimer.Update(gameTime);
            TimerText.Update(gameTime);
            TimerProgressBar.Update(gameTime);
            RectImageTest.Update(gameTime);
            MyPersonHP.Update(gameTime);

            ActivateMenuControls();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(UserInterface.Renderer.DefaultFont, nameof(TestTimer.CurrentValue) + ": " + TestTimer.CurrentValue.ToString(), new Vector2(900, 425), Color.Red);
            spriteBatch.DrawString(UserInterface.Renderer.DefaultFont, nameof(TimerProgressBar.CurrentValue) + ": " + TimerProgressBar.CurrentValue.ToString(), new Vector2(900, 450), Color.Red);

            //spriteBatch.DrawString(UserInterface.Renderer.DefaultFont, TestTimer.TimeSpan.ToString(@"mm\:ss"), new Vector2(900, 400), Color.White);

            spriteBatch.DrawString(UserInterface.Renderer.DefaultFont, nameof(TimerText.Image)+ " (Pos: " + TimerText.Image.Position.ToString() + " Ogn: " + TimerText.Image.Origin.ToString() + " )", new Vector2(50, 1000), Color.White);

            spriteBatch.DrawString(UserInterface.Renderer.DefaultFont, nameof(RectImageTest)+ " (Pos: " + RectImageTest.Position.ToString() + " Ogn: " + RectImageTest.Origin.ToString()+ " )", new Vector2(50, 1030), Color.White);


            RectImageTest.Draw(spriteBatch);

            TimerText.Draw(spriteBatch);

            TimerProgressBar.Draw(spriteBatch);

            MyPersonHP.Draw(spriteBatch);

        }



    }
}
