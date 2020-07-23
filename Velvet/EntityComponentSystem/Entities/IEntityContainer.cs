using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.EntityComponentSystem
{
    public interface IEntityContainer
    {
        EntityList Entities { get; }
    }
}
