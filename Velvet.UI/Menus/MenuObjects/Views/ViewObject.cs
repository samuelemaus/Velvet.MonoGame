using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Velvet.UI
{
    public abstract class ViewObject : IViewObject
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
        protected void ActivateBinding()
        {
            SourceData.PropertyChanged += this.PropertyChanged;

            BindingActive = true;

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

        public virtual void Update(GameTime gameTime)
        {
            Image.Update(gameTime);


        }


    }
}
