using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public abstract class SimpleControl : IControl
    {
        public IControlObject MainControl { get; set; }
    }
}
