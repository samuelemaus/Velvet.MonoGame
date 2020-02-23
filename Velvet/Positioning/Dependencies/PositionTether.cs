using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet
{
    public abstract class PositionTether : PositionDependency, IPositionTether
    {
        public float XTolerance { get; set; }
        public float YTolerance { get; set; }

        public TetherDistanceMeasurementType DistanceMeasurement { get; set; }
        protected virtual Vector2 CurrentDifferential => DifferentialCalculation.Invoke();

        private delegate Vector2 DifferentialCalculationDelegate();

        private DifferentialCalculationDelegate DifferentialCalculation { get; set; }
        
        private Vector2 CalculateTotalDistance()
        {
            return default;
        }

        

        private bool XDifferentialExceedsTolerance;
        private bool YDifferentialExceedsTolerance;


        public override void GetPositionOverride(ref Vector2 position)
        {


            base.GetPositionOverride(ref position);
        }

        public void GetDependencyPositionTethered(IMovable target)
        {

        }

    }




}
