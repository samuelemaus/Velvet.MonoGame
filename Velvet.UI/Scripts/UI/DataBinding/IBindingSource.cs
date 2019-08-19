using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Velvet
{
    public interface IBindingSource
    {
        PropertyInfo[] GetProperties();

        event SendValuePropertyChangedEventHandler PropertyChanged;

        


    }
}
