using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Velvet.DataIO
{
    public interface IFileTypeProtocol
    {
        string FileExtension { get; }

        Regex ParseExpression { get; }

        

    }
}
