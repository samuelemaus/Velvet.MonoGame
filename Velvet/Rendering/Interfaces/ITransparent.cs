using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Rendering
{
    public interface ITransparent
    {
        float Alpha { get; }
        void SetAlpha(float alpha);

    }
}
