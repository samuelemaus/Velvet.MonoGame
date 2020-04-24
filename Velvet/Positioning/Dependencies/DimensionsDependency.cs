using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{

    public abstract class DimensionsDependency
    {
        public bool DependencyActive { get; set; }
        public abstract Dimensions2D DimensionsOverride { get; }

        public float WidthOverride => DimensionsOverride.Width;
        public float HeightOverride => DimensionsOverride.Height;


        private DimensionsOverrideType overrideType = DimensionsOverrideType.FullOverride;
        public DimensionsOverrideType OverrideType
        {
            get
            {
                return overrideType;
            }

            set
            {
                overrideType = value;
            }
        }
        
        public void GetDimensionsOverride(ref Dimensions2D dimensions)
        {
            switch (OverrideType)
            {
                case DimensionsOverrideType.FullOverride: dimensions = DimensionsOverride; break;
                case DimensionsOverrideType.WidthOverride: dimensions.Width = DimensionsOverride.Width; break;
                case DimensionsOverrideType.HeightOverride: dimensions.Height = DimensionsOverride.Height; break;
            }
        }

        public void GetDimensionsOverride(ref Dimensions2D dimensions, ref Vector2 scale)
        {
            switch (OverrideType)
            {
                case DimensionsOverrideType.FullOverride: dimensions = DimensionsOverride * scale; break;
                case DimensionsOverrideType.WidthOverride: dimensions.Width = DimensionsOverride.Width * scale.X; break;
                case DimensionsOverrideType.HeightOverride: dimensions.Height = DimensionsOverride.Height * scale.Y; break;
            }
        }

        

        public Dimensions2D GetDependencyDimensions(IBoundingRect target)
        {
            return target.BoundingRect.Dimensions;
        }

    }

    #region//Dependency Types

    /// <summary>
    /// <see cref="DimensionsDependency"/> which uses one <see cref="IBoundingRect"/> object as its dependency.
    /// </summary>
    public class SizedObjectDimensionsDependency : DimensionsDependency
    {
        public SizedObjectDimensionsDependency(IBoundingRect dependency, DimensionsOverrideType overrideType = DimensionsOverrideType.FullOverride)
        {
            Dependency = dependency;
            OverrideMethod = GetDependencyDimensions;
            DependencyActive = true;
            OverrideType = overrideType;
        }

        public override Dimensions2D DimensionsOverride => OverrideMethod.Invoke(Dependency);

        public IBoundingRect Dependency { get; set; }

        public DimensionsOverrideFromSized OverrideMethod { get; set; }




    }
    /// <summary>
    /// <see cref="DimensionsDependency"/> with two <see cref="IBoundingRect"/> dependencies.  One for the <see cref="Dimensions2D.Width"/> and one for the <see cref="Dimensions2D.Height"/>.
    /// </summary>
    public class DualObjectDimensionsDependency : DimensionsDependency
    {
        public DualObjectDimensionsDependency(IBoundingRect widthDependency, IBoundingRect heightDependency)
        {
            WidthDependency = widthDependency;
            HeightDependency = heightDependency;
            DependencyActive = true;
            WidthOverrideMethod = GetDependencyDimensions;
            HeightOverrideMethod = GetDependencyDimensions;

        }

        public override Dimensions2D DimensionsOverride => new Dimensions2D(width, height);

        public IBoundingRect WidthDependency { get; set; }
        public IBoundingRect HeightDependency { get; set; }

        public DimensionsOverrideFromSized WidthOverrideMethod;
        public DimensionsOverrideFromSized HeightOverrideMethod;

        public float width => WidthOverrideMethod.Invoke(WidthDependency).Width;
        public float height => HeightOverrideMethod.Invoke(HeightDependency).Height;
 
    }
    /// <summary>
    /// <see cref="DimensionsDependency"/> which invokes a <see cref="Dimensions2D"/> Method (<see cref="DimensionsOverrideFromMethod"/>) to get its <see cref="DimensionsOverride"/>
    /// </summary>
    public class MethodDimensionsDependency : DimensionsDependency
    {
        public MethodDimensionsDependency(DimensionsOverrideFromMethod overrideMethod, DimensionsOverrideType overrideType = DimensionsOverrideType.FullOverride)
        {
            OverrideMethod = overrideMethod;
            DependencyActive = true;
            OverrideType = overrideType;

        }

        public override Dimensions2D DimensionsOverride => OverrideMethod.Invoke();
        public DimensionsOverrideFromMethod OverrideMethod { get; set; }

    }

    /// <summary>
    /// <see cref="DimensionsDependency"/> which invokes two <see cref="Dimensions2D"/> Methods.  One for the <see cref="Dimensions2D.Width"/> and one for the <see cref="Dimensions2D.Height"/>.
    /// </summary>
    public class DualMethodDimensionsDependency : DimensionsDependency
    {

        public DualMethodDimensionsDependency(DimensionsOverrideFromMethod widthMethod, DimensionsOverrideFromMethod heightMethod)
        {
            WidthOverrideMethod = widthMethod;
            HeightOverrideMethod = heightMethod;
            DependencyActive = true;
            OverrideType = DimensionsOverrideType.FullOverride;

        }

        public override Dimensions2D DimensionsOverride => new Dimensions2D(width, height);

        float width => WidthOverrideMethod.Invoke().Width;
        float height => HeightOverrideMethod.Invoke().Height;

        public DimensionsOverrideFromMethod WidthOverrideMethod;
        public DimensionsOverrideFromMethod HeightOverrideMethod;
    }

    #endregion
}
