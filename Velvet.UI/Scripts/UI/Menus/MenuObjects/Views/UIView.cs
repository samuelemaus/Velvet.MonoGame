using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Velvet.UI
{
    public abstract class UIView : IBindingTarget
    {

        public IBindingSource SourceData { get; protected set; }
        public virtual dynamic DisplayedValue { get; protected set; }
        protected virtual Type PropertyType { get;  set; }
        public string LocalPropertyName { get; protected set; }
        public bool BindingActive { get; protected set; }

        public abstract void PropertyChanged(object sender, object _value, PropertyChangedEventArgs e);

        public void BindTo(IBindingSource source, object property, string propertyName)
        {
            SourceData = source;
            DisplayedValue = property;
            LocalPropertyName = propertyName;

            ActivateBinding();

        }

        protected void ActivateBinding()
        {
            SourceData.PropertyChanged += this.PropertyChanged;

            //this.SourceValueUpdateSent += SourceData.OnPropertyUpdatedExternally;

            BindingActive = true;

        }

       




    }
}
