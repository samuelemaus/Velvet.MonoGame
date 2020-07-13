using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.UI;
using Velvet.GameSystems;

namespace Velvet
{
    public class TestEquipment : GameData
    {
        private string eqpname;
        public string EquipmentName { get => eqpname; set => SetFieldAndNotify(ref eqpname, value); }

        private int cost;
        public int Cost { get => cost; set => SetFieldAndNotify(ref cost, value); }

        private int str;
        public int Strength { get => str; set => SetFieldAndNotify(ref str, value); }


    }
}
