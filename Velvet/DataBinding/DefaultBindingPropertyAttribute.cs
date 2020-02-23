using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class DefaultBindingPropertyAttribute : Attribute
    {
        

        // This is a positional argument
        public DefaultBindingPropertyAttribute()
        {
            

        }

    }
}
