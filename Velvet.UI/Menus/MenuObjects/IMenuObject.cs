using System;
using System.Collections.Generic;
using System.Text;
using Velvet.Input;

namespace Velvet.UI
{
    public interface IMenuObject : IBoundingRect
    {

        bool IsActive { get; }
        bool IsTargeted { get; }

        

    }
}
