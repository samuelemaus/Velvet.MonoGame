using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface ILogger
    {
        bool LoggerActive { get; set; }
        Queue<string> LogMessages { get; }
        bool IsFixedLogSize { get; }
        int MaxLogCapacity { get; }

        void Log(string message);
        void Log(IEnumerable<string> messages);
        void Log(object message);
        void Log(IEnumerable<object> messages);
        

    }
}