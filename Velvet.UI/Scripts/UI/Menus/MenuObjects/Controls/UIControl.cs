using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Velvet.UI.UI
{
    public abstract class UIControl : ITwoWayBindingTarget
    {





        public void SendValueUpdateToDataSource(object _value)
        {
            SourceValueUpdateSent?.Invoke(this, _value, new PropertyChangedEventArgs(LocalPropertyName));
        }

        public event SendValuePropertyChangedEventHandler SourceValueUpdateSent;


        protected virtual bool _CanSelect()
        {
            bool value = true;

            //if (ObjectReadOnly)
            //{
            //    value = false;
            //}

            return value;
        }

        public virtual Action ObjectSelected { get; set; }
        public virtual void OnObjectSelected()
        {
            ObjectSelected.Invoke();
        }

        public delegate bool CanBeHovered();
        public CanBeHovered CanHover;

        public virtual Action ObjectHovered { get; set; }

        public IBindingSource SourceData { get; protected set; }

        public bool BindingActive { get; protected set; }

        public dynamic DisplayedValue { get; protected set; }

        public string LocalPropertyName { get; protected set; }

        public virtual void OnObjectHOvered()
        {
            ObjectHovered.Invoke();
        }

        public void PropertyChanged(object sender, object _value, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void BindTo(IBindingSource source, object property, string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
