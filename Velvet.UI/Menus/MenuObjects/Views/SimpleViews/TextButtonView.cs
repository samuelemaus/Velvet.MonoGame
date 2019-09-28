using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Velvet;

namespace Velvet.UI
{
    public class TextButtonView : ButtonView
    {

        public TextButtonView()
        {

        }

        public TextButtonView(string buttonText)
        {

            

            
        }

        

        public override IDrawableObject Image => ImageComposite;

        protected virtual CompositeImage ImageComposite { get; set; }

        public IDrawableString ButtonText { get; protected set; }

        public virtual void UpdateTextButtonImage(object value)
        {
            ButtonText.Text = ((string)value); 
        }
        protected override void InitializeImages()
        {
            UpdateImage = UpdateTextButtonImage;
            ButtonText = new TextImage((string)DisplayedValue);

        }


    }
}
