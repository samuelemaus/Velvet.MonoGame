using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velvet.Rendering;
using System.Linq;

namespace Velvet.UI
{
    public class WindowBackground : CompositeImage
    {
        #region//Constructors
        public WindowBackground()
        {
            windowAtlas = UIResources.WindowTextures;
            InitializeImages();
        }

        public WindowBackground(TextureAtlas atlas, BoundingRect targetRect)
        {
            windowAtlas = atlas;
            TargetRect = targetRect;
            InitializeImages();
            DrawMethod = DrawImages;
        }

        public WindowBackground(Window parent)
        {
            windowAtlas = UIResources.WindowTextures;
            ParentWindow = parent;
            TargetRect = ParentWindow.BoundingRect;
            InitializeImages();
            DrawMethod = DrawImages;
        }
        #endregion

        public Window ParentWindow { get; private set; }

        private void InitializeImages()
        {
            
            cellDimensions = windowAtlas.CellDimensions;
            
            BoundingRect defaultCornerRect = new BoundingRect(Vector2.Zero, cellDimensions);
            BoundingRect defaultHorizontalSidesRect = new BoundingRect(Vector2.Zero, GetHorizontalSideDimensions());
            BoundingRect defaultVerticalSidesRect = new BoundingRect(Vector2.Zero, GetVerticalSideDimensions());
            BoundingRect defaultFillRect = new BoundingRect(TargetRect.CenterPosition, GetFillDimensions());



            BottomLeft = new RectImage(defaultCornerRect);
            BottomCenter = new RectImage(defaultHorizontalSidesRect);
            BottomRight = new RectImage(defaultCornerRect);
            TopLeft = new RectImage(defaultCornerRect);
            TopCenter = new RectImage(defaultHorizontalSidesRect);
            TopRight = new RectImage(defaultCornerRect);
            LeftCenter = new RectImage(defaultVerticalSidesRect);
            RightCenter = new RectImage(defaultVerticalSidesRect);
            CenterFill = new RectImage(defaultFillRect);

            BottomLeft.SetRegion(windowAtlas[Alignment.BottomLeft]);
            BottomCenter.SetRegion(windowAtlas[Alignment.BottomCentered]);
            BottomRight.SetRegion(windowAtlas[Alignment.BottomRight]);
            TopLeft.SetRegion(windowAtlas[Alignment.TopLeft]);
            TopCenter.SetRegion(windowAtlas[Alignment.TopCentered]);
            TopRight.SetRegion(windowAtlas[Alignment.TopRight]);
            LeftCenter.SetRegion(windowAtlas[Alignment.LeftCentered]);
            RightCenter.SetRegion(windowAtlas[Alignment.RightCentered]);
            CenterFill.SetRegion(windowAtlas[Alignment.Centered]);


            corners[0] = TopLeft;
            corners[1] = TopRight;
            corners[2] = BottomLeft;
            corners[3] = BottomRight;

            sides[0] = TopCenter;
            sides[1] = BottomCenter;
            sides[2] = LeftCenter;
            sides[3] = RightCenter;

            images[0] = CenterFill;
            images[1] = TopLeft;
            images[2] = TopRight;
            images[3] = BottomLeft;
            images[4] = BottomRight;
            images[5] = TopCenter;
            images[6] = BottomCenter;
            images[7] = LeftCenter;
            images[8] = RightCenter;

            foreach(var img in images)
            {
                img.Color = Color.HotPink;
                img.Alpha = 0.95f;
            }



            InitializeDependencies();
            InitializeDimensions();

        }


        #region//Images
        private TextureAtlas windowAtlas;
        private RectImage BottomLeft;
        private RectImage BottomCenter;
        private RectImage BottomRight;
        private RectImage TopLeft;
        private RectImage TopCenter;
        private RectImage TopRight;
        private RectImage LeftCenter;
        private RectImage RightCenter;
        private RectImage CenterFill;

        private RectImage[] corners = new RectImage[4];
        private RectImage[] sides = new RectImage[4];
        private RectImage[] images = new RectImage[9];

        public override IEnumerable<IDrawableObject> Images => images;


        #endregion


        protected override void InitializeDependencies()
        {
            CenterFill.PositionDependency = new MethodPositionDependency(GetTargetCenter);
            CenterFill.DimensionsDependency = new MethodDimensionsDependency(GetFillDimensions);

            //Sides 
            LeftCenter.DimensionsDependency = new MethodDimensionsDependency(GetVerticalSideDimensions);
            RightCenter.DimensionsDependency = new MethodDimensionsDependency(GetVerticalSideDimensions);
            TopCenter.DimensionsDependency = new MethodDimensionsDependency(GetHorizontalSideDimensions);
            BottomCenter.DimensionsDependency = new MethodDimensionsDependency(GetHorizontalSideDimensions);

            LeftCenter.AnchorTo(CenterFill, Alignment.LeftCentered, RectRelativity.Outside, Dimensions2D.Empty);
            RightCenter.AnchorTo(CenterFill, Alignment.RightCentered, RectRelativity.Outside, Dimensions2D.Empty);
            TopCenter.AnchorTo(CenterFill, Alignment.TopCentered, RectRelativity.Outside, Dimensions2D.Empty);
            BottomCenter.AnchorTo(CenterFill, Alignment.BottomCentered, RectRelativity.Outside, Dimensions2D.Empty);

            //Corners
            TopLeft.AnchorTo(CenterFill, Alignment.TopLeft, RectRelativity.Outside, Dimensions2D.Empty);
            TopRight.AnchorTo(CenterFill, Alignment.TopRight, RectRelativity.Outside, Dimensions2D.Empty);
            BottomLeft.AnchorTo(CenterFill, Alignment.BottomLeft, RectRelativity.Outside, Dimensions2D.Empty);
            BottomRight.AnchorTo(CenterFill, Alignment.BottomRight, RectRelativity.Outside, Dimensions2D.Empty);

        }
        public BoundingRect TargetRect { get; set; }

        private Dimensions2D cellDimensions;

    


        public override BoundingRect BoundingRect => ((IBoundingRect)ParentWindow).BoundingRect;


        #region//Dimensions & Positions Methods
        public Dimensions2D GetHorizontalSideDimensions()
        {
            return new Dimensions2D(TargetRect.Dimensions.Width - (cellDimensions.Width * 2), cellDimensions.Height);
        }

        public Dimensions2D GetVerticalSideDimensions()
        {
            return new Dimensions2D(cellDimensions.Width, TargetRect.Dimensions.Height - (cellDimensions.Height * 2));
        }

        public Dimensions2D GetFillDimensions()
        {
            return TargetRect.Dimensions - (cellDimensions * 2);
        }

        public Vector2 GetTargetCenter()
        {
            return TargetRect.CenterPosition;
        }
        public void ResizeTo(Dimensions2D dimensions)
        {
            Vector2 position = TargetRect.CenterPosition;
            TargetRect = new BoundingRect(position, dimensions);
        }
        #endregion

        protected override void DrawImages(SpriteBatch spriteBatch)
        {
            //UIResources.Effect.Parameters["texture1"].SetValue(windowAtlas.SourceTexture);
            //UIResources.Effect.CurrentTechnique.Passes[0].Apply();
            base.DrawImages(spriteBatch);
            
        }

        private Dictionary<RectImage, Alignment> imageAlignments = new Dictionary<RectImage, Alignment>();
        private void initializeDict()
        {
            imageAlignments.Add(TopLeft, Alignment.TopLeft);
            imageAlignments.Add(TopCenter, Alignment.TopCentered);
            imageAlignments.Add(TopRight, Alignment.TopRight);
            imageAlignments.Add(LeftCenter, Alignment.LeftCentered);
            imageAlignments.Add(CenterFill, Alignment.Centered);
            imageAlignments.Add(RightCenter, Alignment.RightCentered);
            imageAlignments.Add(BottomLeft, Alignment.BottomLeft);
            imageAlignments.Add(BottomCenter, Alignment.BottomCentered);
            imageAlignments.Add(BottomRight, Alignment.BottomRight);
        }

    }
}
