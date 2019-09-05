using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public static class BaseExtensions
    {

        public static BoundingRect ToBoundingRect(this Rectangle rect)
        {
            return new BoundingRect(rect);
        }

    }
}
