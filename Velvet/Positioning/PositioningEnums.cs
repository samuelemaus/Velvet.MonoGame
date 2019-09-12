using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    //Positioning
    public enum Origin { Left, Center, Right }

    public enum XReference { Center, Left, Right, Unbound, BoundCoordinate }

    public enum YReference { Center, Top, Bottom, Unbound, BoundCoordinate }

    public enum BindType { FullyBound, Unbound, X_Unbound, Y_Unbound }

    public enum RectRelativity { Outside, Inside }

    public enum Orientation { Vertical, Horizontal }

    public enum PositionDependencyType { Default, Anchor}
    public enum PositionCoordinateOverride { FullOverride, XOverride, YOverride}


}
