using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Velvet
{
    public static class Math
    {
        public static float EnforceValueRange(float _value, float minValue, float maxValue)
        {
            if(_value < minValue)
            {
                return minValue;
            }

            if( _value > maxValue)
            {
                return maxValue;
            }

            else
            {
                return _value;
            }

        }
        public static float EnforceValueRange(float _value, ValueRange valueRange)
        {
            if (_value < valueRange.MinimumValue)
            {
                return valueRange.MinimumValue;
            }

            if (_value > valueRange.MaximumValue)
            {
                return valueRange.MaximumValue;
            }

            else
            {
                return _value;
            }

        }


    }
}
