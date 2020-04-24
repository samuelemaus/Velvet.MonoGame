using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Velvet.DataBinding
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    sealed class BindingTarget : Attribute
    {
        public BindingTarget([CallerMemberName]string propertyName = null)
        {

        }



    }
}
