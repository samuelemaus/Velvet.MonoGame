using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public class TestPerson : GameData
    {

        #region//Notifying Properties
        private string name;
        public string Name { get => name; set => SetField(ref name, value); }

        private int hp;
        public int HP { get => hp; set
            {
                int newValue = (int)Math.EnforceValueRange(value, HPRange);

                SetField(ref hp, newValue);
            }
        }

        public ValueRange HPRange = new ValueRange(0, 100);

        private bool isDead;
        public bool IsDead
        {
            get
            {
                return isDead;
            }

            set
            {
                if(HP <= 0)
                {
                    isDead = true;
                }

                else
                {
                    isDead = false;
                }

                SetField(ref isDead, isDead);
            }
        }


        private bool wearsGlasses;
        public bool WearsGlasses { get => wearsGlasses; set => SetField(ref wearsGlasses, value); }

        

        public TestEquipment Weapon;
        public TestEquipment Armor;


        #endregion


    }
}
