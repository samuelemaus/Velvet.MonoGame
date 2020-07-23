using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Velvet
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

        public static float Enforce(float _value, float minValue, float maxValue, bool rollOver = false)
        {
            if (_value < minValue)
            {
                if (rollOver)
                {
                    return maxValue;
                }

                else
                {
                    return minValue;
                }

                
            }

            if (_value > maxValue)
            {
                if (rollOver)
                {
                    return minValue;
                }

                else
                {
                    return maxValue;
                }
            }

            else
            {
                return _value;
            }
        }
        public static float Enforce(float _value, ValueRange valueRange, bool rollOver = false)
        {
            if (_value < valueRange.MinimumValue)
            {
                if (rollOver)
                {
                    return valueRange.MaximumValue - (Math.Abs(_value - valueRange.MinimumValue));
                }

                else
                {
                    return valueRange.MinimumValue;
                }
                
            }

            if (_value > valueRange.MaximumValue)
            {
                if (rollOver)
                {
                    return valueRange.MinimumValue + (valueRange.MaximumValue - _value);
                }

                else
                {
                    return valueRange.MaximumValue;
                }
            }

            else
            {
                return _value;
            }

        }

        public static int Enforce(int _value, ValueRange valueRange, bool rollOver = false)
        {
            if (_value < valueRange.MinimumValue)
            {
                if (rollOver)
                {
                    return (int)valueRange.MaximumValue;
                }

                else
                {
                    return (int)valueRange.MinimumValue;
                }

                
            }

            if (_value > valueRange.MaximumValue)
            {

                if (rollOver)
                {
                    return (int)valueRange.MinimumValue;
                }

                else
                {
                    return (int)valueRange.MaximumValue;
                }

            }

            else
            {
                return (int)_value;
            }
        }


        public static int Enforce(int _value, int minValue, int maxValue, bool rollOver = false)
        {
            if (_value < minValue)
            {
                if (rollOver)
                {
                    return maxValue/* - (_value - minValue)*/;
                }

                else
                {
                    return minValue;
                }


            }

            if (_value > maxValue)
            {
                if (rollOver)
                {
                    return minValue/* + (maxValue - _value)*/;
                }

                else
                {
                    return maxValue;
                }
            }

            else
            {
                return _value;
            }
        }



    }

    

}
