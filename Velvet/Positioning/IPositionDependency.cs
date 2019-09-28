using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet
{
    public interface IDependency
    {
        
        bool DependencyActive { get; }
        
        float XOverride { get; }
        float YOverride { get; }



    }
}
