using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface IDimensions2D
    {
        Dimensions2D Dimensions { get; }

        void SetDimensions(Dimensions2D value);

        void SetWidth(float value);
        void SetHeight(float value);


    }
}
