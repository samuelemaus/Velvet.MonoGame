using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;


namespace Velvet.UI
{
    public abstract class BindableViewObject : IBindableViewObject
    {
        public abstract IDrawableObject Image { get; }
        protected abstract void InitializeImages();
        public virtual PassObjectAction UpdateImage { get; protected set; }
        public IBindingSource SourceData { get; set; }
        public string LocalPropertyName { get; set; }
        public bool BindingActive { get; set; }
        public dynamic DisplayedValue { get; set; }
        public void BindTo(IBindingSource source, object property, string propertyName)
        {
            SourceData = source;
            DisplayedValue = property;
            LocalPropertyName = propertyName;
            ActivateBinding();
        }

        public void BindTo(IBindingSource source, PropertyInfo propertyInfo)
        {
            SourceData = source;
            DisplayedValue = propertyInfo.GetValue(source);
            DisplayedValue = propertyInfo.Name;
            ActivateBinding();
        }


        public void ActivateBinding()
        {
            SourceData.PropertyChanged += this.PropertyChanged;
            BindingActive = true;
            InitializeImages();

        }

        public void DeactivateBinding()
        {
            SourceData.PropertyChanged -= this.PropertyChanged;
            BindingActive = false;
        }
        public virtual void PropertyChanged(object sender, object value, PropertyChangedEventArgs e)
        {
            if (PropertiesMatch(e.PropertyName, LocalPropertyName))
            {
                DisplayedValue = value;
                UpdateImage.Invoke(value);

            }
        }
        private bool PropertiesMatch(string senderName, string localName)
        {
            return senderName == localName;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }

    }
}
