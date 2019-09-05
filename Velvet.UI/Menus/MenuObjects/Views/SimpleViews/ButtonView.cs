using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Velvet.UI
{
    public abstract class ButtonView : ViewObject
    {
        //public override IDrawableObject Image { get; }
        public IDrawableTexture ButtonTexture { get; protected set; }
        public ControlObjectState ButtonState { get; set; }
        protected virtual Texture2D[] ButtonTextures { get; }


    }
}
