using System;
using System.Collections.Generic;
using System.Text;
using Velvet;


namespace Velvet.UI
{
    public interface IViewObject : IBindingTarget
    {
        IDrawableObject Image { get; }
        PassObjectAction UpdateImage { get; }

    }
}
