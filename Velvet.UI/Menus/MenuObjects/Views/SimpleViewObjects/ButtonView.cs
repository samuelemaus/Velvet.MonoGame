using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Velvet.Rendering;

namespace Velvet.UI
{
    public  class ButtonView : BindableViewObject
    {
        public override IDrawableObject Image => ButtonTexture;
        protected ButtonCompositeImage ButtonTexture { get; set; }
        public ControlObjectState ButtonState { get; set; }
        protected virtual TextureAtlas ButtonTextures { get; }

        public BoundingRect TargetRect { get; set; }

        protected override void InitializeImages()
        {
            ButtonTexture = new ButtonCompositeImage(TargetRect);


        }

        public class ButtonCompositeImage : CompositeImage
        {
            public ButtonCompositeImage()
            {
                buttonAtlas = UIResources.SmallControlTextures;
                InitializeImages();
            }

            public ButtonCompositeImage(BoundingRect targetRect, TextureAtlas textureAtlas = null)
            {
                TargetRect = targetRect;
                if(textureAtlas != null)
                {
                    buttonAtlas = textureAtlas;
                }

                else
                {
                    buttonAtlas = UIResources.SmallControlTextures;
                }


                InitializeImages();
            }

            public BoundingRect TargetRect { get; set; }
            private Dimensions2D cellDimensions;

            public override BoundingRect BoundingRect => TargetRect;

            private TextureAtlas buttonAtlas;

            RectImage Left;
            RectImage Center;
            RectImage Right;

            private RectImage[] images = new RectImage[3];

            public override IEnumerable<IDrawableObject> Images => images;

            private void InitializeImages()
            {
                cellDimensions = buttonAtlas.CellDimensions;

                BoundingRect defaultSideRect = new BoundingRect(Vector2.Zero, new Dimensions2D(cellDimensions.Width, TargetRect.Dimensions.Height));
                BoundingRect defaultCenterRect = new BoundingRect(Vector2.Zero, GetCenterDimensions());

                Left = new RectImage(defaultSideRect)
                {
                    Alpha = 0.95f,
                    Color = Color.LightSeaGreen
                };
                Right = new RectImage(defaultSideRect)
                {
                    Alpha = 0.95f,
                    Color = Color.LightSeaGreen
                };
                Center = new RectImage(defaultCenterRect)
                {
                    Alpha = 0.95f,
                    Color = Color.LightSeaGreen
                };


                Left.SetRegion(buttonAtlas[HorizontalAlignment.Left, ControlObjectState.Default]);
                Right.SetRegion(buttonAtlas[HorizontalAlignment.Right, ControlObjectState.Default]);
                Center.SetRegion(buttonAtlas[HorizontalAlignment.Center, ControlObjectState.Default]);

                images[0] = Center;
                images[1] = Right;
                images[2] = Left;


                InitializeDimensions();
                InitializeDependencies();


            }



            protected override void InitializeDependencies()
            {
                Center.PositionDependency = new MethodPositionDependency(GetTargetCenter);
                Center.DimensionsDependency = new MethodDimensionsDependency(GetCenterDimensions);

                //Left.SetDimensionsDependency(GetDimensions, DimensionsOverrideType.HeightOverride);
                //Right.SetDimensionsDependency(GetDimensions, DimensionsOverrideType.HeightOverride);

                //Left.StretchToFill = true;
                //Right.StretchToFill = true;


                Left.AnchorTo(Center, Alignment.LeftCentered, RectRelativity.Outside, Dimensions2D.Empty);
                Right.AnchorTo(Center, Alignment.RightCentered, RectRelativity.Outside, Dimensions2D.Empty);


            }

            private Dimensions2D GetCenterDimensions()
            {
                return new Dimensions2D(TargetRect.Dimensions.Width - (cellDimensions.Width * 2), TargetRect.Dimensions.Height);
            }

            #region//Dimensions & CenterPosition Methods
            public Vector2 GetTargetCenter()
            {
                return TargetRect.CenterPosition;
            }

            public Dimensions2D GetDimensions()
            {
                return TargetRect.Dimensions;
            }

            public void UpdateImages(ControlObjectState state)
            {
                Left.SetRegion(buttonAtlas[HorizontalAlignment.Left, state]);
                Center.SetRegion(buttonAtlas[HorizontalAlignment.Center, state]);
                Right.SetRegion(buttonAtlas[HorizontalAlignment.Right, state]);
            }



            #endregion

        }

    }


}
