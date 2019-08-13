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



    }
}
