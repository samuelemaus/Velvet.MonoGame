using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet
{
    public abstract class PositionDependency/* : IPositionDependency*/
    {
       
        public bool DependencyActive { get; set; }
        public abstract Vector2 PositionOverride { get; }

        public float YOverride => PositionOverride.Y;
        public float XOverride => PositionOverride.X;

        public PositionCoordinateOverride CoordinateOverride { get; set; } = PositionCoordinateOverride.FullOverride;

        public void GetPositionOverride(ref Vector2 position)
        {
            switch (CoordinateOverride)
            {
                case PositionCoordinateOverride.FullOverride: position = PositionOverride; break;
                case PositionCoordinateOverride.XOverride: position.X = PositionOverride.X; break;
                case PositionCoordinateOverride.YOverride: position.Y = PositionOverride.Y; break;
            }
        }

    }

    #region//Dependency Types
    public class MovablePositionDependency : PositionDependency
    {
        public MovablePositionDependency(IMovable movable)
        {
            Dependency = movable;
            OverrideMethod = DependencyPosition;
            DependencyActive = true;
        }

        public IMovable Dependency { get; set; }
        public override Vector2 PositionOverride => OverrideMethod.Invoke(Dependency);

        public PositionOverrideFromMovable OverrideMethod { get; set; }

        public Vector2 DependencyPosition(IMovable target)
        {
            return target.Position;
        }
    }
    public class DualMovablePositionDependency : PositionDependency
    {
        public DualMovablePositionDependency(IMovable xDependency, IMovable yDependency)
        {
            XDependency = xDependency;
            YDependency = yDependency;
        }

        public IMovable XDependency { get; set; }
        public IMovable YDependency { get; set; }

        public PositionOverrideFromMovable XOverrideMethod;
        public PositionOverrideFromMovable YOverrideMethod;

        private float xPosition => XOverrideMethod.Invoke(XDependency).X;
        private float yPosition => YOverrideMethod.Invoke(YDependency).Y;

        public override Vector2 PositionOverride => new Vector2(xPosition, yPosition);

    }
    public class MethodPositionDependency : PositionDependency
    {


        public MethodPositionDependency(PositionOverrideFromMethod overrideMethod)
        {
            OverrideMethod = overrideMethod;
            DependencyActive = true;
        }

        public override Vector2 PositionOverride => OverrideMethod.Invoke();

        public PositionOverrideFromMethod OverrideMethod { get; set; }

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
    #endregion


}
