using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Input
{
    public interface ISelectable
    {
        PassObjectAction ObjectSelected { get; }
        void OnObjectSelected();
        PassObjectBool CanSelect { get; }

    }
}
