using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public interface IViewObject
    {
        IDrawableObject Image { get; }
        PassObjectAction UpdateImage { get; }
    }
}
