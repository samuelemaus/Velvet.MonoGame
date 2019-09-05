//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Input;
//

//namespace Velvet.UI
//{
//    public class MenuText : MenuObject
//    {
//        protected override void UpdateImage(object _value)
//        {
//            TextImage textImage = Image as TextImage;

//            //string text = "";

//            //if(_value is float v)
//            //{
//            //    text = v.ToString(Format);
//            //}

//            //else
//            //{
//            //    text = _value.ToString();
//            //}

           
//            textImage.Text = FormattedValue(_value,Format);
//        }

//        public override Image2D Image { get; set; } = new TextImage();

//        public override ReferenceRect CurrentRect {
//            get
//            {
//                TextImage textImage = Image as TextImage;

//                return textImage.CurrentRect;
//            }
//        }

//        public string Format { get; protected set; }

//        public string FormattedValue(object _value, string format)
//        {
//            string finalText = _value.ToString();

//            if(_value is float f)
//            {
//                finalText = f.ToString(format);
//            }

//            else if(_value is TimeSpan ts)
//            {
//                finalText = ts.ToString(format);
//            }


//            return finalText;
//        }
        

//        #region//Constructors
//        public MenuText()
//        {

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="property"></param>
//        /// <param name="propertyName"></param>
//        /// <param name="position"></param>
//        /// <param name="fontScale"></param>
//        /// <param name="color"></param>
//        /// <param name="format"></param>
//        /// <param name="iFormattable"></param>
//        public MenuText(IBindingSource source, object property, string propertyName, Vector2 position, float fontScale, Color color, string format = null, IFormattable iFormattable = null)
//        {
//            this.BindTo(source, property, propertyName);

//            PropertyType = property.GetType();

//            Format = format;


//            Image = new TextImage(FormattedValue(property,Format))
//            {
//                Position = position,
//                Font = UserInterface.Renderer.DefaultFont,
//                Scale = new Vector2(fontScale, fontScale),
//                Color = color
//            };
            
            
//        }

//        #endregion

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            base.Draw(spriteBatch);
//        }
//    }



//    #region//Generic

//    public class MenuText<T> : MenuText
//    {

//        public new Action<T> ObjectSelected { get; set; }




//    }

//    #endregion

//}
