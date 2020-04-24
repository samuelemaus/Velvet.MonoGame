using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Velvet
{
    public delegate void SendValuePropertyChangedEventHandler(object sender, object value, PropertyChangedEventArgs e);

    public interface IBindingSource
    {
        PropertyInfo[] GetProperties();
        PropertyInfo IdentifyProperty(string propertyName);

        event SendValuePropertyChangedEventHandler PropertyChanged;
        void OnPropertySetByControl(object sender, object value, PropertyChangedEventArgs e);

    }
}
