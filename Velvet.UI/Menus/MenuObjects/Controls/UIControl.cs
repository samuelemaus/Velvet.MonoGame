using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Velvet.UI
{
    public abstract class UIControl : IControl
    {
        public abstract IControlObject MainControl { get; set; }


    }
}
