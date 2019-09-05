using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using Velvet.Input;
using System.ComponentModel;

namespace Velvet.UI
{
    public abstract class ControlObject : IControlObject
    {
        #region//IControlObject
        public abstract IViewObject View { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual object TargetObject { get; set; }
        public virtual PassObjectAction ObjectSelected { get; set; }
        public virtual PassObjectBool CanSelect { get; set; }
        public virtual void OnObjectSelected()
        {
            ObjectSelected.Invoke(TargetObject);   
        }
        public ControlObjectState CurrentBaseState { get; }
        public abstract ControlObjectState GetCurrentState();

        #endregion

        #region//IBoundingRect
        public BoundingRect BoundingRect { get; protected set; }
        public Vector2 Position { get; protected set; }

        public virtual void Move(Vector2 position)
        {
            View.Image.Move(position);
            Position += position;
            
        }
        public virtual void SetPosition(Vector2 position)
        {
            View.Image.SetPosition(position);
            Position = position;
        }


        protected virtual void InitializeView(IViewObject view)
        {
            view.Image.SetPosition(this.Position);
        }

        #endregion

        #region//IControlValueSender
        public void SendValueUpdateToDataSource(object _value)
        {
            SourceValueUpdateSent?.Invoke(this, _value, new PropertyChangedEventArgs(View.LocalPropertyName));
        }

        public void UpdateBoundingRect()
        {
            throw new NotImplementedException();
        }

        public event SendValuePropertyChangedEventHandler SourceValueUpdateSent;


        #endregion
    }
}
