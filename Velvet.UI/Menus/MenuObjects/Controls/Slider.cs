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

//    /// <summary>
//    /// 
//    /// </summary>
//    public class Slider
//    {

//        #region//Base
//        protected override void UpdateImage(object _value)
//        {

//        }
//        #endregion

//        #region//Content

//        #region//Images

//        public Image2D Indicator;
//        public Image2D BackgroundLine;
//        public Image2D[] IntervalImages;

//        private void InitializeImages()
//        {
//            IntervalImages = new Image2D[NumIntervals];
//        }

//        #endregion

//        public ValueRange ValueRange;
//        public Orientation Orientation { get; set; }

//        public SliderIntervalDivision ValueGradient { get; set; }

//        private int numIntervals;
//        public int NumIntervals
//        {
//            get
//            {
//                return numIntervals;
//            }

//            set
//            {
//                if (ValueGradient == SliderIntervalDivision.FullRange)
//                {
//                    numIntervals = (int)ValueRange.MaximumValue;
//                }


//            }

//        }

//        private Vector2[] intervalPositions;

//        private void Initialize()
//        {
//            intervalPositions = new Vector2[NumIntervals];

//            InitializeImages();

//        }
        



//        #endregion

//        #region//Sliding Mechanics

//        //Settings

//        /// <summary>
//        /// When using Mouse controls, the distance (in pixels) the Indicator has to be to the next IntervalNotch to snap. E.g., with a value of 4, the Indicator will snap to the nearest IntervalNotch when within 4 pixels of its center.
//        /// </summary>
//        public float SnapSensitivity { get; set; }
//        public bool MoveIndicatorWithMouse { get; set; }




//        #endregion


//    }
//}
