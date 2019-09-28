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
        public abstract IViewObject View { get; }
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
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }

        public PositionDependency PositionDependency { get; set; }
        public virtual Dimensions2D Dimensions { get; }
        public virtual DimensionsDependency DimensionsDependency { get; set; }

        protected virtual void InitializeView(IViewObject view)
        {
            view.Image.PositionDependency = new MovablePositionDependency(this);

            
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

        public virtual void SetWidth(float value)
        {

        }
        public virtual void SetHeight(float value)
        {

        }

        public event SendValuePropertyChangedEventHandler SourceValueUpdateSent;


        #endregion
    }
}
