using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Velvet.UI
{
    public interface IBindingSource
    {
        PropertyInfo[] GetProperties();
        PropertyInfo IdentifyProperty(string propertyName);

        event SendValuePropertyChangedEventHandler PropertyChanged;
        void OnPropertyUpdatedExternally(object sender, object _value, PropertyChangedEventArgs e);



    }
}
