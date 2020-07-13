using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{
    public class GuidManager
    {

        private List<uint> UsedGuids = new List<uint>();
        public uint GetGuid(uint digits)
        {

            uint guid = 0;

            bool guidCreated = false;

            while (!guidCreated)
            {
                int min = GetMinRandomNumber(digits);
                int max = GetMaxRandomNumber(digits);

                uint newGuid = (uint)Random.Next(min, max);

                if (!UsedGuids.Contains(newGuid))
                {
                    guid = newGuid;
                    UsedGuids.Add(guid);
                    guidCreated = true;
                }
            }

            return guid;
        }

        private Random Random { get; set; } = new Random();

        private int GetMaxRandomNumber(uint digits)
        {
            int singleDig = 1;

            for (int i = 0; i < digits; i++)
            {
                singleDig *= 10;
            }

            return singleDig - 1;
        }
        private int GetMinRandomNumber(uint digits)
        {
            if(digits == 1)
            {
                return 0;
            }

            int singleDig = 1;

            for (int i = 1; i < digits; i++)
            {
                singleDig *= 10;
            }

            return singleDig;
        }

    }
}
