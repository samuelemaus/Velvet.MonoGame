using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Velvet.DataIO
{
    

    public class FileParser
    {

        public FileParser()
        {

        }

        public ILogger Logger;

        public IFileTypeProtocol Protocol { get; set; }


        public string[] GetParsedContent(string content)
        {
            var matches = Protocol.ParseExpression.Matches(content);

            string[] returnList = new string[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                returnList[i] = matches[i].Value;
            }

            return returnList;

        }
        
    }

}
