using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet
{
    public interface IPositionDependency
    {
        IMovable Dependency { get; }
        bool DependencyActive { get; }
        




    }
}
