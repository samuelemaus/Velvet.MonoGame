using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public interface IResizable
    {
        
        bool IsResizable { get; set; }
        bool IsResizing { get; set; }
    }
}
