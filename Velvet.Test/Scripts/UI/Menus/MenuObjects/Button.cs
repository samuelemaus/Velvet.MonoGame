using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Velvet
{
    public class Button : MenuObject
    {
        protected override void UpdateImage(object _value)
        {

        }

        public override Image2D Image { get; set; } = new BasicImage();

    }
}
