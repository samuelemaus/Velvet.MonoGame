using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public delegate bool PassObjectBool(object args);

    public delegate T PassObjectDelegate<T>(object args);

    public delegate void PassObjectAction(object args);

    public delegate void UpdateMethod(GameTime gameTime);
}
