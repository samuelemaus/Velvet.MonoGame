using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet
{
    public interface IUpdate
    {
        uint UpdateInterval { get; }
        void Update(GameTime gameTime);
    }
}
