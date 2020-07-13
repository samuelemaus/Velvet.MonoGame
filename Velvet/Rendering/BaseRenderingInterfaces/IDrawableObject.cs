using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface IDrawableObject : IBoundingRect, IColorable, IRotatable, ITransparent, IScalable
    {
        //Properties
        SpriteEffects SpriteEffect { get; set;  }
        Vector2 Origin { get; }
        float LayerDepth { get; set;  }

        //Methods
        void Draw(SpriteBatch spriteBatch);



    }

}
