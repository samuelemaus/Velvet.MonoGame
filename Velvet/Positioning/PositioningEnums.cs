using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    //Positioning
    public enum Origin { Left, Center, Right }

    public enum XReference { Center, Left, Right }

    public enum YReference { Center, Top, Bottom}

    public enum BindType { FullyBound, Unbound, X_Unbound, Y_Unbound }

    public enum RectRelativity { Outside, Inside }

    public enum Orientation { Vertical, Horizontal }

    public enum PositionDependencyType { Default, Anchor}
    public enum PositionOverrideType { FullOverride, XOverride, YOverride}
    public enum DimensionsOverrideType { FullOverride, WidthOverride, HeightOverride}
    /// <summary>
    /// Determines how a <see cref="PositionTether"/> measures its <see cref="PositionTether.CurrentDifferential"/>.
    /// </summary>
    public enum TetherDistanceMeasurementType { TotalDistance, IndependentCoordinateDistance}


}
