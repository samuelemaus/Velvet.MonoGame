using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    /// <summary>
    /// Drawable object which combines one or more IDrawableObjects into one Draw and Update call.
    /// </summary>
    public interface IDrawableComposite : IDrawableObject
    {

        IDrawableObject[] Images { get; }



    }
}
