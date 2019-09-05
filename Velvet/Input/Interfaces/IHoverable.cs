using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Input
{
    public interface IHoverable
    {
        Action HoveredOver { get; }
        void OnHoveredOver();
        PassObjectBool CanHover { get; }

    }
}
