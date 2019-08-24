//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Input;
//using Velvet.Rendering;

//namespace Velvet.UI
//{
//    public class ProgressBar : MenuObject
//    {
//        protected override void UpdateImage(object _value)
//        {
//            //FillBar.BoundingRect.Width = fillBarLength();
//            //UpdateFillColor();
//            //FillBar.Color = FillColor;

//        }

//        public override void LoadContent()
//        {
//            //if(EmptyBar != null)
//            //{
//            //    EmptyBar.LoadContent();
//            //}

//            //FillBar.LoadContent();
//        }

//        public override void Update(GameTime gameTime)
//        {
//            //if(EmptyBar != null)
//            //{
//            //    EmptyBar.Update(gameTime);
//            //}

//            //FillBar.Update(gameTime);

//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            if(EmptyBar != null)
//            {
//                EmptyBar.Draw(spriteBatch);
//            }

//            FillBar.Draw(spriteBatch);
//        }

//        #region//Constructors
//        public ProgressBar()
//        {

//        }

//        public ProgressBar(ReferenceRect target, ProgressBarFillType fillType, ProgressBarFillColorBehavior colorBehavior, Color[] colorRange, ValueRange valueRange)
//        {
//            TargetRect = target;

//            FillType = fillType;

//            FillColorBehavior = colorBehavior;

//            ColorRange = colorRange;

//            ValueRange = valueRange;

//            Initialize();

//        }

//        public ProgressBar(ReferenceRect target, ProgressBarFillType fillType, ProgressBarFillColorBehavior colorBehavior, List<Color> colorRange, ValueRange valueRange)
//        {
//            TargetRect = target;

//            FillType = fillType;

//            FillColorBehavior = colorBehavior;

//            ColorRange = colorRange.ToArray();

//            ValueRange = valueRange;

//            Initialize();

//        }

//        public ProgressBar(IBindingSource source, object property, string propertyName, ReferenceRect target, List<Color> colorRange, ValueRange valueRange, ProgressBarFillType fillType = ProgressBarFillType.Default, ProgressBarFillColorBehavior colorBehavior = ProgressBarFillColorBehavior.Static)
//        {
//            SourceData = source;

//            DisplayedValue = property;

//            LocalPropertyName = propertyName;

//            this.BindTo(SourceData, DisplayedValue, LocalPropertyName);

//            TargetRect = target;

//            FillType = fillType;

//            FillColorBehavior = colorBehavior;

//            ColorRange = colorRange.ToArray();

//            ValueRange = valueRange;

//            Initialize();

//        }


//        private void Initialize()
//        {


//            initializeImageDimensions();

//            defineColorSetPoints();

//            //FillBar.TargetRect.Content.Location = TargetRect.Content.Location;

//            UpdateFillColor();

//            //FillBar.Color = FillColor;

//        }

//        #endregion

//        #region//Content

//        public float CurrentValue
//        {
//            get => DisplayedValue;
//        }

//        protected ValueRange ValueRange;

//        private float PercentageFilled()
//        {

//            float huh = CurrentValue / ValueRange.MaximumValue;
//            return huh;
//        }

//        public Orientation BarOrientation()
//        {
//            if (TargetRect.Content.Width > TargetRect.Content.Height)
//            {
//                return Orientation.Horizontal;
//            }

//            else if (TargetRect.Content.Width < TargetRect.Content.Height)
//            {
//                return Orientation.Vertical;
//            }

//            else
//            {
//                return Orientation.Horizontal;
//            }

//        }
//        public ReferenceRect TargetRect;

//        public IDrawableTexture FillBar;
//        public IDrawableTexture EmptyBar;


//        int fillBarMaxLength()
//        {
//            if(BarOrientation() == Orientation.Horizontal)
//            {
//                return TargetRect.Content.Width;
//            }

//            else
//            {
//                return TargetRect.Content.Height;
//            }
//        }
//        int fillBarLength()
//        {
//            int bleh = (int)(PercentageFilled() * fillBarMaxLength());

//            return bleh;
//        }

//        int fillBarWidth()
//        {
//            if(BarOrientation() == Orientation.Horizontal)
//            {
//                return TargetRect.Content.Height;
//            }

//            else
//            {
//                return TargetRect.Content.Width;
//            }
//        }

//        private void initializeImageDimensions()
//        {
//            EmptyColor = Color.DarkGray;

//            EmptyColor *= 0.6f;

//            //if (EmptyColor != null)
//            //{
//            //    EmptyBar = new RectImage(TargetRect)
//            //    {
//            //        Color = EmptyColor
//            //    };
//            //}

//            //FillBar = new RectImage(new ReferenceRect(new Rectangle(TargetRect.Content.Location,TargetRect.Content.Size)));
            
//            //if(BarOrientation() == Orientation.Horizontal)
//            //{
//            //    FillBar.TargetRect.Content.Width = fillBarLength();
//            //    FillBar.TargetRect.Content.Height = fillBarWidth();
//            //}

//            //else
//            //{
//            //    FillBar.TargetRect.Content.Height = fillBarLength();
//            //    FillBar.TargetRect.Content.Width = fillBarWidth();
//            //}



//        }

//        #endregion

//        #region//Colors & Fill Logic
//        public ProgressBarFillType FillType;
//        public ProgressBarFillColorBehavior FillColorBehavior;
//        public ProgressBarColorType ColorType;

//        private Color[] ColorRange;
//        public float[] ColorSetPoints;

//        private void defineColorSetPoints()
//        {
//            ColorSetPoints = new float[ColorRange.Length];

//            float Ratio = (1f / (ColorSetPoints.Length - 1));

//            for (int i = 0; i < ColorSetPoints.Length; i++)
//            {
//                ColorSetPoints[i] = Ratio * (i);
//            }

//        }

//        public int CurrentColorIndex()
//        {
//            int value = 0;

//            for (int i = 0; i < ColorRange.Length; i++)
//            {
//                if (i == 0)
//                {
//                    if (PercentageFilled() < ColorSetPoints[i])
//                    {
//                        value = i;
//                    }
//                }

//                else if (i == ColorRange.Length - 1)
//                {
//                    if (PercentageFilled() >= ColorSetPoints[i])
//                    {
//                        value = i;
//                    }
//                }

//                else
//                {
//                    if (PercentageFilled() >= ColorSetPoints[i] && PercentageFilled() < ColorSetPoints[i + 1])
//                    {
//                        value = i;
//                    }
//                }
//            }

//            return value;
//        }
//        private float GetPercentageDifference(float low, float high, float current)
//        {

//            float totalRange = high - low;

//            float adjustedCurrent = current - low;

//            float value = adjustedCurrent / totalRange;

//            return value;
//        }

//        public Color EmptyColor;
//        public Color FillColor;
//        public void UpdateFillColor()
//        {

//            if (FillColorBehavior == ProgressBarFillColorBehavior.Static)
//            {
//                FillColor = ColorRange[CurrentColorIndex()];

//            }

//            else if (FillColorBehavior == ProgressBarFillColorBehavior.Dynamic)
//            {
//                int currentIndex = CurrentColorIndex();

//                //Color color1 = ColorRange[currentIndex];


//                if (currentIndex >= ColorRange.Length - 1)
//                {
//                    FillColor = ColorRange[ColorRange.Length - 1];
//                }

//                else
//                {
//                    FillColor = RenderingExtensions.GetInterpolatedColor(ColorRange[currentIndex], ColorRange[currentIndex + 1], 1f - GetPercentageDifference(ColorSetPoints[currentIndex + 1], ColorSetPoints[currentIndex], PercentageFilled()));
//                }

//                //if (currentIndex == ColorSetPoints.Length - 1)
//                //{
//                //    FillColor = Extensions.GetInterpolatedColor(ColorRange[currentIndex - 1], ColorRange[currentIndex], 1f - GetPercentageDifference(ColorSetPoints[currentIndex -1], ColorSetPoints[currentIndex], PercentageFilled));

//                //}

//                //else
//                //{
//                //    FillColor = Extensions.GetInterpolatedColor(ColorRange[currentIndex], ColorRange[currentIndex + 1], 1f - GetPercentageDifference(ColorSetPoints[currentIndex + 1], ColorSetPoints[currentIndex], PercentageFilled));
//                //}





//            }

//        }



//        #endregion


//    }
//}
