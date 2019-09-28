using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet
{
    public abstract class PositionDependency
    {
       
        public virtual bool DependencyActive { get; set; }
        public abstract Vector2 PositionOverride { get; }

        public float YOverride => PositionOverride.Y;
        public float XOverride => PositionOverride.X;
        

        public virtual PositionOverrideType OverrideType { get; set; } = PositionOverrideType.FullOverride;

        public void GetPositionOverride(ref Vector2 position)
        {
            switch (OverrideType)
            {
                case PositionOverrideType.FullOverride: position = PositionOverride; break;
                case PositionOverrideType.XOverride: position.X = PositionOverride.X; break;
                case PositionOverrideType.YOverride: position.Y = PositionOverride.Y; break;
            }
        }

        

        public Vector2 GetDependencyPosition(IMovable target)
        {
            return target.Position;
        }

        public Vector2 GetDependencyBoundingRectPosition(IBoundingRect target)
        {
            return target.BoundingRect.Position;
        }

        public override string ToString()
        {
            return $"Override: {PositionOverride}, Type: {OverrideType}";
        }

    }

    #region//Dependency Types
    public class MovablePositionDependency : PositionDependency
    {
        public MovablePositionDependency(IMovable movable)
        {
            Dependency = movable;
            OverrideMethod = GetDependencyPosition;
            DependencyActive = true;
        }

        public MovablePositionDependency(IMovable movable, PositionOverrideFromMovable overrideMethod)
        {
            Dependency = movable;
            OverrideMethod = overrideMethod;
            DependencyActive = true;
        }

        public IMovable Dependency { get; set; }
        public override Vector2 PositionOverride => OverrideMethod.Invoke(Dependency);

        public PositionOverrideFromMovable OverrideMethod { get; set; }

        public override string ToString()
        {
            return $"{Dependency} + {base.ToString()}";
        }

    }

    /// <summary>
    /// <see cref="PositionDependency"/> with two <see cref="IMovable"/> dependencies.  One for the <see cref="Vector2.X"/> coordinate and one for the <see cref="Vector2.Y"/> coordinate.
    /// </summary>
    public class DualMovablePositionDependency : PositionDependency
    {
        public DualMovablePositionDependency(IMovable xDependency, IMovable yDependency)
        {
            XDependency = xDependency;
            YDependency = yDependency;
            DependencyActive = true;
            XOverrideMethod = GetDependencyPosition;
            YOverrideMethod = GetDependencyPosition;

        }

        public IMovable XDependency { get; set; }
        public IMovable YDependency { get; set; }

        public PositionOverrideFromMovable XOverrideMethod;
        public PositionOverrideFromMovable YOverrideMethod;

        private float xPosition => XOverrideMethod.Invoke(XDependency).X;
        private float yPosition => YOverrideMethod.Invoke(YDependency).Y;

        public override Vector2 PositionOverride => new Vector2(xPosition, yPosition);

    }

    /// <summary>
    /// <see cref="PositionDependency"/> which invokes a <see cref="Vector2"/> Method (<see cref="PositionOverrideFromMethod"/>) to get its <see cref="PositionOverride"/>.
    /// </summary>
    public class MethodPositionDependency : PositionDependency
    {


        public MethodPositionDependency(PositionOverrideFromMethod overrideMethod)
        {
            OverrideMethod = overrideMethod;
            DependencyActive = true;
        }

        public override Vector2 PositionOverride => OverrideMethod.Invoke();

        public PositionOverrideFromMethod OverrideMethod { get; set; }

        public override string ToString()
        {
            return $"{OverrideMethod}, {base.ToString()}";
        }

    }

    public class DualMethodPositionDependency : PositionDependency
    {
        public DualMethodPositionDependency(PositionOverrideFromMethod xMethod, PositionOverrideFromMethod yMethod)
        {
            XOverrideMethod = xMethod;
            YOverrideMethod = yMethod;
        }

        public PositionOverrideFromMethod XOverrideMethod { get; set; }
        public PositionOverrideFromMethod YOverrideMethod { get; set; }

        private float xPosition => XOverrideMethod.Invoke().X;
        private float yPosition => YOverrideMethod.Invoke().Y;
        public override Vector2 PositionOverride => new Vector2(xPosition, yPosition);



    }

    public class SplitTypeDualPositionDependency : PositionDependency
    {
        public SplitTypeDualPositionDependency(PositionDependency xDependency, PositionDependency yDependency)
        {
            XDependency = xDependency;
            YDependency = yDependency;
            DependencyActive = true;
            XDependency.DependencyActive = true;
            YDependency.DependencyActive = true;
        }

        private bool dependencyActive;
        public override bool DependencyActive
        {
            get
            {
                return dependencyActive;
            }

            set
            {
                dependencyActive = value;
                XDependency.DependencyActive = value;
                YDependency.DependencyActive = value;
            }
        }

        public PositionDependency XDependency { get; private set; }
        public PositionDependency YDependency { get; private set; }

        public override Vector2 PositionOverride => new Vector2(XDependency.XOverride, YDependency.YOverride);
    }
    #endregion


}
