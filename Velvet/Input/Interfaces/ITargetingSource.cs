using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Input
{
    public interface ITargetingSource : IMovable
    {
        object TargetedObject { get; }



    }
}
