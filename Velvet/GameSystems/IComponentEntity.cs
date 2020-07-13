using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{
    //Represents an Object that can contain a list of Component Type objects.
    public interface IComponentEntity<T>
    {
        
        GuidManager GuidManager { get; }
    }
}
