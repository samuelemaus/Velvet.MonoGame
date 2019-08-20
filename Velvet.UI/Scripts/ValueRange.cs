using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Velvet.UI
{
    public struct ValueRange
    {
        public ValueRange(float minimumValue, float maximumValue)
        {

            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }

        public ValueRange(int minimumValue, int maximumValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }

       
        public readonly float MinimumValue;
        public readonly float MaximumValue;

       

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string min = MinimumValue.ToString();
            string max = MaximumValue.ToString();

            return "{" + min + "-" + max + "}";

        }

        public static float Enforce(float _value, float minValue, float maxValue)
        {
            if (_value < minValue)
            {
                return minValue;
            }

            if (_value > maxValue)
            {
                return maxValue;
            }

            else
            {
                return _value;
            }
        }
        public static float Enforce(float _value, ValueRange valueRange)
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

        public static int Enforce(int _value, ValueRange valueRange)
        {
            if (_value < valueRange.MinimumValue)
            {
                return (int)valueRange.MinimumValue;
            }

            if (_value > valueRange.MaximumValue)
            {
                return (int)valueRange.MaximumValue;
            }

            else
            {
                return (int)_value;
            }
        }

        public static int Enforce(int _value, int minValue, int maxValue)
        {
            if (_value < minValue)
            {
                return minValue;
            }

            if (_value > maxValue)
            {
                return maxValue;
            }

            else
            {
                return _value;
            }
        }



    }
}
