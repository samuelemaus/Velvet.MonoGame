using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Velvet.UI
{
    public class ToggleButton : MenuObject
    {

        #region//Base
        protected override void UpdateImage(object _value)
        {

            if (ButtonOn())
            {
                Image = ButtonOnImage;
            }

            else
            {
                Image = ButtonOffImage;
            }
        }
        public override Image2D Image { get; set; } = new BasicImage();

        private BasicImage ButtonOnImage = new BasicImage();
        private BasicImage ButtonOffImage = new BasicImage();


        #endregion

        #region//Content

        public ToggleButtonType ToggleType { get; set; }

        private TextToggleButtonBehavior _TextBehavior;
        private TextToggleButtonBehavior TextBehavior {
            get
            {
                return _TextBehavior;
            }
            set
            {
                if(ToggleType != ToggleButtonType.Text)
                {
                    _TextBehavior = TextToggleButtonBehavior.Default;
                }

                else
                {
                    _TextBehavior = value;
                }
            }
        }
        private bool ButtonOn()
        {
            bool? _val = DisplayedValue as bool?;

            if (_val != null)
            {
                return (bool)_val;
            }

            else
            {
                return false;
            }

        }

        private void Toggle()
        {
            bool sendValue = !ButtonOn();

            //SendValueUpdateToDataSource(sendValue);

        }



        #endregion

        #region//Constructors

        public ToggleButton()
        {

        }

        public ToggleButton(GameData data, object property, string propertyName)
        {
            this.BindTo(data, property, propertyName);

            ObjectSelected = Toggle;
            

        }

        #endregion

    }
}
