using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;

namespace Velvet
{
    public abstract class MenuObject : UIData, IAnchorableObject
    {

        #region//Content
        public virtual Image2D Image { get; set; }

        public virtual ReferenceRect CurrentRect { get; }
        #endregion

        #region//PropertyChange Handling
        protected override void PropertyChanged(object sender, object _value, PropertyChangedEventArgs e)
        {
            if (LocalPropertyName == e.PropertyName)
            {
                DisplayedValue = _value;

                UpdateImage(_value);

                
            }
        }

        protected abstract void UpdateImage(object _value);
        #endregion

        #region//Object Interaction / Handling

        public bool ObjectReadOnly { get; private set; } = false;
        public bool CanBeSelected { get => _CanSelect(); }

        protected virtual bool _CanSelect()
        {
            bool value = true;

            if (ObjectReadOnly)
            {
                value = false;
            }

            return value;
        }

        public virtual Action ObjectSelected { get; set; }
        public virtual void OnObjectSelected()
        {
            ObjectSelected.Invoke();
        }

        public delegate bool CanBeHovered();
        public CanBeHovered CanHover;

        public virtual Action ObjectHovered { get; set; }
        public virtual void OnObjectHOvered()
        {
            ObjectHovered.Invoke();
        }

        #endregion

        public virtual void LoadContent()
        {
            Image.LoadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this.Image.Draw(spriteBatch);
        }




    }
}
