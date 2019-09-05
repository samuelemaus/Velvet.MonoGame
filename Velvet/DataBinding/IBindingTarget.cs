using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Velvet
{
    public interface IBindingTarget
    {
        IBindingSource SourceData { get;  }
        string LocalPropertyName { get; }
        bool BindingActive { get; }
        void PropertyChanged(object sender, object value, PropertyChangedEventArgs e);
        //void ActivateBinding();
        void BindTo(IBindingSource source, object property, string propertyName);
        dynamic DisplayedValue { get; }




    }
}
