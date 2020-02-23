using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    /// <summary>
    /// Drawable object which combines two or more IDrawableObjects into one Draw and Update call.
    /// </summary>
    public interface IDrawableComposite : IDrawableObject
    {

        IEnumerable<IDrawableObject> Images { get; }



    }
}
