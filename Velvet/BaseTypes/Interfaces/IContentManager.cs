using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Velvet
{
    public interface IContentManager
    {
        ContentManager Content { get; set; }

        string RootDirectory { get; set; }

    }
}
