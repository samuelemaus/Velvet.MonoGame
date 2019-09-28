using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.Rendering
{

    public class TextureAtlas
    {
        public TextureAtlas(Texture2D sourceTexture, int divisions)
        {
            SourceTexture = sourceTexture;
            Divisions = divisions;

            Subdivisions = divisions * divisions;


            InitializeRegions();
        }


        #region//Dimensions & Divisions

        private IndexArrangement indexArrangement = IndexArrangement.HorizontalAscending;
        public IndexArrangement IndexArrangement
        {
            get
            {
                return indexArrangement;
            }
            set
            {
                indexArrangement = value;
                ArrangeRegions(value);
                
            }
        }
        
        /// <summary>
        /// The number of times 
        /// </summary>
        public int Divisions { get; set; }

        public int Subdivisions { get; private set; }

        public Dimensions2D CellDimensions { get; set; }
        public Dimensions2D SourceDimensions => new Dimensions2D(SourceTexture.Width, SourceTexture.Height);

        /// <summary>
        /// Number of pixels between each <see cref="TextureRegion"/>
        /// </summary>
        public int Offset { get; set; }

        public int NumColumns => (int)(SourceDimensions.Width / CellDimensions.Width);
        public int NumRows => (int)(SourceDimensions.Height / CellDimensions.Height);

        private bool SquareRatio => SourceDimensions.IsSquare;
        
        
        

        #endregion
        public Texture2D SourceTexture { get; set; }

        public TextureRegion[] Regions;
        #region//Indexers
        public TextureRegion this[object tag]
        {
            get
            {
                TextureRegion region = default;

                bool noMatchFound = true;

                foreach(var reg in Regions)
                {
                    if(reg.Tags.Contains(tag))
                    {
                        region = reg;
                        noMatchFound = false;
                    }
                }

                if (noMatchFound)
                {
                    throw new NullReferenceException($"The TextureAtlas does not contain a TextureRegion with the given object: {tag.ToString()}");
                    
                }

                return region;
            }
        }

        public TextureRegion this[object tag1, object tag2]
        {
            get
            {
                TextureRegion region = default;

                bool noMatchFound = true;

                foreach (var reg in Regions)
                {
                    if (reg.Tags.Contains(tag1) && reg.Tags.Contains(tag2))
                    {
                        region = reg;
                        noMatchFound = false;
                    }
                }

                if (noMatchFound)
                {
                    throw new NullReferenceException($"The TextureAtlas does not contain a TextureRegion with the given objects: {tag1.ToString()}, {tag2.ToString()}");

                }

                return region;
            }
        }

        public TextureRegion this[object tag1, object tag2, object tag3]
        {
            get
            {
                TextureRegion region = default;

                bool noMatchFound = true;

                foreach (var reg in Regions)
                {
                    if (reg.Tags.Contains(tag1) && reg.Tags.Contains(tag2) && reg.Tags.Contains(tag3))
                    {
                        region = reg;
                        noMatchFound = false;
                    }
                }

                if (noMatchFound)
                {
                    throw new NullReferenceException($"The TextureAtlas does not contain a TextureRegion with the given objects: {tag1.ToString()}, {tag2.ToString()}, {tag3.ToString()}");
                }

                return region;
            }
        }

        public TextureRegion this[object tag1, object tag2, object tag3, object tag4]
        {
            get
            {
                TextureRegion region = default;

                bool noMatchFound = true;

                foreach (var reg in Regions)
                {
                    if (reg.Tags.Contains(tag1) && reg.Tags.Contains(tag2) && reg.Tags.Contains(tag3) && reg.Tags.Contains(tag4))
                    {
                        region = reg;
                        noMatchFound = false;
                    }
                }

                if (noMatchFound)
                {
                    throw new NullReferenceException($"The TextureAtlas does not contain a TextureRegion with the given objects: {tag1.ToString()}, {tag2.ToString()}, {tag3.ToString()},{tag4.ToString()} ");
                }

                return region;
            }
        }

        public TextureRegion this[int i]
        {
            get
            {

                return Regions[ValueRange.Enforce(i, 0, Regions.Length)];
            }
        }
        public TextureRegion this[string name]
        {
            get
            {
                TextureRegion region = default;

                bool noMatchFound = true;

                foreach (var reg in Regions)
                {


                    if (reg.Name == name)
                    {
                        region = reg;
                        noMatchFound = false;
                    }
                }

                if (noMatchFound)
                {
                    throw new NullReferenceException($"The TextureAtlas does not contain a TextureRegion with the given Name: {name}");

                }

                return region;
            }
        }

        /// <summary>
        /// Indexer for retrieving Texture Region using an undefined object List.  Substantially less efficient than retrieving with a known quantity of objects.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public TextureRegion this[List<object> tags]
        {
            get
            {
                TextureRegion region = default;

                bool noMatchFound = true;

                foreach(var reg in Regions)
                {
                    bool containsAll = true;

                    foreach(var tag in tags)
                    {
                        if (!reg.Tags.Contains(tag))
                        {
                            containsAll = false;
                        }
                    }

                    if (containsAll)
                    {
                        region = reg;
                        noMatchFound = false;
                    }

                }

                if (noMatchFound)
                {
                    throw new NullReferenceException($"The TextureAtlas does not contain a TextureRegion with the given object: {tags.ToString()}");

                }

                return region;

            }
        }

        #endregion

        private void InitializeRegions()
        {
            var textureSourceRect = SourceDimensions.ToRectangle();

            var regionsSourceRects = textureSourceRect.SubdivideToGrid(Divisions);

            Regions = new TextureRegion[regionsSourceRects.Length];

            for (int i = 0; i < regionsSourceRects.Length; i++)
            {
                Regions[i] = new TextureRegion(SourceTexture, regionsSourceRects[i]);   
            }

            CellDimensions = new Dimensions2D(regionsSourceRects[0].Width, regionsSourceRects[0].Height);
        }

        //TODO: This
        private void ArrangeRegions(IndexArrangement arrangement)
        {

        }

        private Rectangle GetRectAtStartingCorner(IndexArrangement arrangement)
        {
            

            int rightSide = 0;
            int leftSide = 0;
            int topSide = 0;
            int bottomSide = 0;

            switch (arrangement.CornerOfFirstIndex.X)
            {
                case XReference.Right:

                    leftSide = rightSide - (int)CellDimensions.Width;
                    rightSide = (int)SourceDimensions.Width;
                    break;

                case XReference.Left:

                    leftSide = 0;
                    rightSide = (int)CellDimensions.Width;
                    break;
            }

            switch (arrangement.CornerOfFirstIndex.Y)
            {
                case YReference.Top:

                    bottomSide = (int)CellDimensions.Height;
                    topSide = 0;
                    break;

                case YReference.Bottom:

                    bottomSide = (int)SourceDimensions.Height;
                    topSide = bottomSide - (int)CellDimensions.Height;
                    break;
                    

            }


            Rectangle value = new Rectangle(leftSide, topSide, (rightSide - leftSide), (bottomSide - topSide));

            return value;
        }

       

        

    }
}
