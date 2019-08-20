using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Velvet.UI
{
    public class LabeledValue : UIElement
    {

        public MenuObject Value { get; set; }
        public MenuText Label { get; set; }
        public BasicImage Icon { get; set; }
        

        private string getLabel()
        {
            return Value.LocalPropertyName;
        }

    }
}
