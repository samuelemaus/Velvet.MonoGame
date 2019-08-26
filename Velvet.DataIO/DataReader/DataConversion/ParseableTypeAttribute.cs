using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Microsoft.Xna.Framework;



namespace Velvet.DataIO
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class ParseableTypeAttribute : Attribute
    {
        public ParseableTypeAttribute(string positionalString)
        {
            
        }

    }

}
