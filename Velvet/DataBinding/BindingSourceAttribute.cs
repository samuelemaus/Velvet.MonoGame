using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Velvet
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class BindingSource : Attribute
    {
        public BindingSource([CallerMemberName]string propertyName = null)
        {
            PropertyName = propertyName;
        }
        
        public string PropertyName { get; private set; }
        public bool OverrideSetMethod { get; private set; }
        
        public void SetField(object value)
        {

        }

        public event SendValuePropertyChangedEventHandler PropertyChanged;

        public bool SetField<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            else
            {
                field = value;
                //OnPropertyChanged(value, propertyName);   
                return true;
            }

        }

    }
}
