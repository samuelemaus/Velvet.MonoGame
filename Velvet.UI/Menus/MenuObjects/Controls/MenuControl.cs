using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Velvet.UI
{
    public abstract class MenuControl : MenuView, IControl
    {
        public abstract IControlObject MainControl { get; set; }


    }
}
