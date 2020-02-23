using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Velvet.Rendering
{
    public class TextureRegion
    {
        public TextureRegion(Texture2D texture, Rectangle rect)
        {
            SourceTexture = texture;
            SourceRect = rect;
        }

        /// <summary>
        /// Objects associated with the particular Texture region.  Currently limited to 7 for performance reasons.
        /// </summary>
        public List<object> Tags { get; protected set; } = new List<object>(7);
        public string Name { get; set; }



        public Rectangle SourceRect { get; set; }
        public Texture2D SourceTexture { get; set; }


        #region//Public Methods
        /// <summary>
        /// Returns whether the <see cref="TextureRegion"/> is fully transparent.  Expensive operation, should only be called if absolutely necessary.
        /// </summary>
        public bool IsEmpty()
        {
            int size = SourceRect.Width * SourceRect.Height;
            Color[] buffer = new Color[size];
            SourceTexture.GetData(0, SourceRect, buffer, 0, size);
            return buffer.All(c => c == Color.Transparent);

        }

        public void AddTag(object tag)
        {
            if (!Tags.Contains(tag))
            {
                Tags.Add(tag);
            }
        }

        public void AddTags(object tag0, object tag1, object tag2 = null, object tag3 = null, object tag4 = null, object tag5 = null, object tag6 = null)
        {
            if(tag0 != null)
            {
                Tags.Add(tag0);
            }

            if (tag1 != null)
            {
                Tags.Add(tag1);
            }

            if (tag2 != null)
            {
                Tags.Add(tag2);
            }

            if (tag3 != null)
            {
                Tags.Add(tag3);
            }

            if (tag4 != null)
            {
                Tags.Add(tag4);
            }

            if (tag5 != null)
            {
                Tags.Add(tag5);
            }

            if (tag6 != null)
            {
                Tags.Add(tag6);
            }
        }

        #endregion



    }
}
