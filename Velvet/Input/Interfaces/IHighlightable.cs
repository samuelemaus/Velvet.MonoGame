using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Input
{
    public interface IHighlightable
    {
        Action Highlighted { get; }
        void OnHighlighted();
        PassObjectBool CanHighlight { get; }

    }
}
