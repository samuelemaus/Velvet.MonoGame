using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.Rendering
{
    public interface IDrawableString : IDrawableObject
    {
        string Text { get; }
        TextAlignment Alignment { get; }

        void SetText(string text);

    }
}
