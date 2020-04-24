using System;
using Microsoft.Xna.Framework;
using Velvet.Input;


namespace Velvet.UI
{
    public interface IControlObject : ISelectable, IControlValueSender, IBoundingRect
    {
        bool IsVisible { get; }
        bool IsActive { get; }
        IBindableViewObject View { get; }
        ControlObjectState CurrentBaseState { get; }
        ControlObjectState GetCurrentState();

    }
}