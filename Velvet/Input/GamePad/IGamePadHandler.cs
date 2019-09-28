using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Input
{
    public interface IGamePadHandler : IInputHandler
    {
        IPointer Pointer { get; }

    }
}
