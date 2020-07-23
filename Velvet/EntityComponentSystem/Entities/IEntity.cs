using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.EntityComponentSystem
{
    //Represents an Object that can contain a list of Component Type objects.
    public interface IEntity : IUpdate, IDraw
    {
        string Name { get; }
        uint ID { get; }
        ComponentList Components { get; }
        GuidManager GuidManager { get; }
    }
}
