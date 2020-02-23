using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Velvet.UI
{




    #region//Menu Objects

    #region//Views

    public enum ResizeViewBehavior {ConstrainObjectsToBoundingRect, ConstrainBoundingRectToObjects }

    #endregion

    #region//ToggleButton
    public enum ToggleButtonType {Text, RadioButton, EmbossedButton, HorizontalSwitch, VerticalSwitch}

    public enum TextToggleButtonBehavior { Default, ChangeColor, ChangeValue, ChangeColorAndValue }

    #endregion

    #region//ProgressBar

    public enum ProgressBarFillType { Default, ByInterval }

    public enum ProgressBarFillColorBehavior { Static, Dynamic }

    public enum ProgressBarColorType { Solid, Gradient }
    #endregion

    #region//Slider

    public enum SliderIntervalDivision {FullRange, RegularIntervals, IrregularIntervals, LogarithmicIntervals}



    #endregion

    #endregion

    #region//UI Elements
    public enum ControlObjectState {Default, Active, Targeted, Unselectable}
    public enum HighlighterType {CursorIndicator, ColorHighlighter, DrawableHighlighter}
    public enum HoverTrigger {Wait, Input}
    public enum HeadingType { Default}

    #endregion
}
