using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.Rendering
{
    public struct BoundingRect : IEquatable<BoundingRect>
    {
        public readonly Rectangle Rectangle;
        public readonly Vector2 Offset;
        public readonly Vector2 Padding;

        private void Initialize(Rectangle rectangle, Vector2 offset, Vector2 padding)
        {

            float x = rectangle.X;
            float y = rectangle.Y;
            float width = rectangle.Width;
            float height = rectangle.Height;

            if (offset != Vector2.Zero)
            {
                x += offset.X;
                y += offset.Y;
            }

            if(padding != Vector2.Zero)
            {
                 width  += padding.X;
                 height += padding.Y;
            }

           
            
        }

        public bool Equals(BoundingRect other)
        {
            throw new NotImplementedException();
        }
    }
}
