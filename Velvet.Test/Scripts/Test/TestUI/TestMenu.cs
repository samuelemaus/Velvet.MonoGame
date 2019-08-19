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

            

        }

        public override void ActivateMenuControls()
        {
            
        }

        

        public override void LoadContent()
        {

            
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            

        }



    }
}
