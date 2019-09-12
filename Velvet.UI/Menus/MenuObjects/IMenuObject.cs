using System;
using System.Collections.Generic;
using System.Text;
using Velvet.Input;

namespace Velvet.UI
{
    public interface IMenuObject
    {
        IViewObject View { get; }

        bool ObjectActive { get; }

        bool ObjectTargeted { get; }

        ITargetingSource TargetingSource { get; }

    }
}
