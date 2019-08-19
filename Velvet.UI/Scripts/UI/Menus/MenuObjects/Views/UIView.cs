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

namespace Velvet
{
    public abstract class UIViewObject
    {

        public GameData Data { get; protected set; }
        public virtual dynamic DisplayedValue { get; protected set; }
        protected virtual Type propertyType { get;  set; }
        public string LocalPropertyName { get; protected set; }
        public bool BindingActive { get; protected set; }

        protected abstract void PropertyChanged(object sender, object _value, PropertyChangedEventArgs e);

        protected void BindTo(GameData data, object property, string propertyName)
        {
            Data = data;
            DisplayedValue = property;
            LocalPropertyName = propertyName;

            ActivateBinding();

        }

        protected void ActivateBinding()
        {
            Data.PropertyChanged += this.PropertyChanged;

            this.SourceValueUpdateSent += Data.OnPropertyUpdatedExternally;

            BindingActive = true;

        }

        protected void SendValueUpdateToDataSource(object _value)
        {
            SourceValueUpdateSent?.Invoke(this, _value, new PropertyChangedEventArgs(LocalPropertyName));
        }



        public event SendValuePropertyChangedEventHandler SourceValueUpdateSent;




    }
}
