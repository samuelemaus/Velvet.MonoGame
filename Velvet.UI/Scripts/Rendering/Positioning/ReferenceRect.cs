using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Velvet
{
    /// <summary>
    /// Wrapper class for Rectangle struct to be used as a Reference-Type.
    /// </summary>
    public class ReferenceRect
    {
        public ReferenceRect()
        {
            Content = new Rectangle();
        }

        public ReferenceRect(Rectangle _content)
        {
            Content = _content;

        }

        public Rectangle Content;

        public PositionAnchor Anchor;






        public bool IsAnchored;

    }
}
