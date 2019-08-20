using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet.UI
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);

    }
}
