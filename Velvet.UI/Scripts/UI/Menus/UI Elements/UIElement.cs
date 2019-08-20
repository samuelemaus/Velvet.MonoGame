using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet.UI
{

    /// <summary>
    /// Combines various MenuObjects in a specified manner. E.g., A list with labels and radio-boxes.
    /// </summary>
    public class UIElement : IAnchorableObject
    {
        public List<MenuObject> MenuObjects = new List<MenuObject>();
        public int ObjectsCount { get; }
        public Vector2 Dimensions { get; }
        public ReferenceRect CurrentRect { get; set; }



    }
}
