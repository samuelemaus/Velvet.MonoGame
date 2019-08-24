using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.Rendering
{
    public interface IDrawableTexture : IDrawableObject
    {
        Texture2D Texture { get; }
        string FilePath { get; }

        void SetTexture(Texture2D texture);


    }
}
