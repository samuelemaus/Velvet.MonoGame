using System;
using System.Collections.Generic;
using System.Text;
using Velvet;

namespace Velvet.GameSystems
{
    public interface IWorldContainer
    {
        GameWorld World { get; set; }
    }
}
