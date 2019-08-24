using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Velvet.DataIO
{
    public class FileTypeProtocol : IFileTypeProtocol
    {
        public string FileExtension { get; }

        public Regex ParseExpression { get; }

        public string[] GetParsedContent(string content)
        {
            return ParseExpression.Split(content);
        }
    }
}
