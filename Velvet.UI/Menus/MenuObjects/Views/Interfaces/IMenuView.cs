using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public interface IMenuView : IMenuObject
    {
        bool IsVisible { get; }
        void UpdateVisualState();

    }
}
