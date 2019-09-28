using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Input
{
    public interface IInputHandler : IUpdate
    {
        bool HandlerActive { get; }

        double TimeInactive { get; }
        double ActiveThreshold { get; set; }


    }
}
