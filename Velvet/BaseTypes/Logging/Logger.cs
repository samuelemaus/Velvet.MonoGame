using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public class Logger : ILogger
    {

        public bool LoggerActive { get; set; } = false;
        public List<string> LogMessages { get; set; } = new List<string>();
        public int MaxLogCapacity { get; } = 0;
        public bool IsFixedLogSize => MaxLogCapacity != 0;
        public void Log(string message)
        {
            if (!IsFixedLogSize)
            {
                LogMessages.Add(message);
            }

            else
            {
                LogMessages.RemoveAt(0);
                LogMessages.Add(message);
            }
            
        }

        public void Log(IEnumerable<string> messages)
        {
            throw new NotImplementedException();
        }

        public void Log(dynamic message)
        {
            throw new NotImplementedException();
        }

        public void Log(IEnumerable<dynamic> messages)
        {
            throw new NotImplementedException();
        }
    }



}
