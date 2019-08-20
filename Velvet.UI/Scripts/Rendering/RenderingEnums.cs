using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Velvet.UI
{

    //Positioning
    public enum Origin { Left, Center, Right }

    public enum XReference { Center, Left, Right, Unbound, BoundCoordinate }

    public enum YReference { Center, Top, Bottom, Unbound, BoundCoordinate }

    public enum BindType { FullyBound, Unbound, X_Unbound, Y_Unbound }

    public enum RectRelativity { Outside, Inside }

    public enum Orientation {Vertical, Horizontal}


    //Text
    public enum InfoDensity { Default, Cozy, Spacious, SetByValue }

    public enum TextAlignment { Left, Center, Right }

    public enum TextCase { Default, AllUpper, AllLower, TitleCase }

    public enum HighlightType { TextColor, SolidRect, LineRect }


}
