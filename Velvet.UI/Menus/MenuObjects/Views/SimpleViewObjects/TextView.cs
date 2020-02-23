using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public class TextView : BindableViewObject
    {
        public TextView()
        {
            UpdateImage = UpdateTextImage;
        }

        public TextView(TextAlignment textAlignment)
        {

        }

        public override IDrawableObject Image => TextImage;

        private TextImage TextImage;

        private void UpdateTextImage(object value)
        {
            TextImage.Text = value.ToString();
        }
        

        protected override void InitializeImages()
        {
            TextImage = new TextImage(DisplayedValue.ToString(), UIController.Renderer.DefaultFont);
        }
    }
}
