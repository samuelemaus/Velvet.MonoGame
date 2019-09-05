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
    public class ScrollBar : ControlObject
    {
        public override IViewObject View { get; set; }

        public override ControlObjectState GetCurrentState()
        {
            throw new NotImplementedException();
        }


    }
}
