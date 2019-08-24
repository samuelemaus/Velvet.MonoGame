using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Rendering
{
    public interface IDrawableObject : IBoundingRect, IColorable, IRotatable, ITransparent, IScalable, IUpdate
    {
        //Properties
        SpriteEffects SpriteEffect { get; }


        //Methods
        void Draw(SpriteBatch spriteBatch);
        

    }

}
