using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public class ConsoleLogger : ILogger
    {

        public bool LoggerActive { get; set; } = false;
        public List<string> LogMessages { get; } = new List<string>();

        public bool IsFixedLogSize => false;

        public int MaxLogCapacity => 0;


        public void Log(string message)
        {

            if (LoggerActive)
            {
                Console.WriteLine(message);
            }
            
        }
        public void Log(IEnumerable<string> messages)
        {
            foreach(var m in messages)
            {
                Log(m);
            }
        }
        public void Log(object message)
        {
            if(message != null)
            {
                Log(message.ToString());
            }

            else
            {
                Log("Null object");
            }

        }
        public void Log(IEnumerable<object> messages)
        {
            foreach(var m in messages)
            {
                Log(m.ToString());
            }
        }
    }
}
