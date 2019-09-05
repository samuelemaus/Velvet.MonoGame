﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public interface IDrawableObject : IDimensions2D, IBoundingRect, IColorable, IRotatable, ITransparent, IScalable, IUpdate
    {
        //Properties
        SpriteEffects SpriteEffect { get; }
        float LayerDepth { get; }

        Vector2 Origin { get; }

        //Methods
        void Draw(SpriteBatch spriteBatch);



    }

}