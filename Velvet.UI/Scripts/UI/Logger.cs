using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public class Logger
    {
        public List<string> LogMessages = new List<string>();

        public int MaxLogCapacity { get; } = 0;

        public bool IsFixedLogSize => MaxLogCapacity != 0;

    }



}
