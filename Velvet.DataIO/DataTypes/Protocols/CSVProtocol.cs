using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Velvet.DataIO
{
    public class CSVProtocol : IFileTypeProtocol
    {
        public string FileExtension => ".csv";

        public Regex ParseExpression { get; } = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");

        string b = "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";

        public string[] GetParsedContent(string content)
        {

            return ParseExpression.Split(content);
        }
    }
}
