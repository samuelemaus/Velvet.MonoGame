using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public interface ILogMessage
    {
        string LogMessage { get; set; }

        void SendToLogger(Logger logger); 

    }
}
