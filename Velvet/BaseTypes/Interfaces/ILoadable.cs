using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public interface ILoadable
    {
        void LoadContent();
        void UnloadContent();

    }
}
