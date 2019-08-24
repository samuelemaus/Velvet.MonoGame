using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace Velvet.DataIO
{
    public class DataReader
    {
        public ILogger Logger;

        public string[] GetFormattedData(string path)
        {
            List<string> returnList = new List<string>();

            Parser.Logger = Logger;

            Logger.Log(nameof(DataValidated_Initial) + ": Succeeded.");

            string extension = GetFileExtension(path);

            foreach(var p in DataManager.ImplementedProtocols)
            {
                if(extension == p.FileExtension)
                {
                    Parser.Protocol = p;
                }
            } 

            List<string> unformatted = LoadUnformattedData(DataManager.GetFullPath(path));

            //Logger.Log("Unformatted Data: ");
            Logger.Log("UF Count: " + unformatted.Count.ToString());

            foreach (var d in unformatted)
            {
                string[] formatted = Parser.GetParsedContent(d);
               
                returnList.AddRange(formatted);

            }

            Logger.Log(returnList);
            return returnList.ToArray();

        }

        public RawFileData GetRawFileData(string path)
        {
            RawFileData returnData = RawFileData.Empty;

            int numFields = 0;
            int numItems = 0;
            string[] headings;
            string[][] entries;

            string extension = GetFileExtension(path);

            foreach (var p in DataManager.ImplementedProtocols)
            {
                if (extension == p.FileExtension)
                {
                    Parser.Protocol = p;
                }
            }

            

            List<string> unformatted = LoadUnformattedData(DataManager.GetFullPath(path));

            numItems = unformatted.Count;

            headings = Parser.GetParsedContent(unformatted[0]);

            numFields = headings.Length;

            entries = new string[numItems][];

            for (int i = 0; i < numItems; i++)
            {
                entries[i] = Parser.GetParsedContent(unformatted[i]);
            }

            returnData = new RawFileData(headings, entries, numFields);

            return returnData;

        }

        List<string> LoadUnformattedData(string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
            List<string> searchList = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                searchList.Add(line);
            }
            return searchList;
        }

        Parser Parser = new Parser();


        #region//Validation
        public string GetFileExtension(string path)
        {
            int nameEndIndex = path.LastIndexOf('.');

            if(nameEndIndex < 0)
            {
                //throw new FileLoadException("The provided string was not recognized as a file-path.");

                return string.Empty;

            }


            int extensionLength = (path.Length - nameEndIndex);



            char[] chars = path.ToCharArray(nameEndIndex,extensionLength);

            string extension = "";

            foreach(char c in chars)
            {
                extension += c;
            }

            //Logger.Log("Extension: " + extension);

            return extension;


        }
        protected bool IsFileExtension(string path)
        {
            bool value = (GetFileExtension(path) != string.Empty);

            //Logger.Log(nameof(IsFileExtension) + ": " + value);

            return value;

            
        }
        protected bool IsSupportedFileExtension(string extension)
        {

            bool value = DataManager.FileExtensions().Contains(extension);

            Logger.Log(nameof(IsSupportedFileExtension) + ": " + value);

            //if(value == false)
            //{
            //    Logger.Log("Supported Extensions: ");

            //    foreach(var s in DataManager.FileExtensions())
            //    {
            //        Logger.Log(s);
            //    }
            //}

            return value;
        }
        protected bool ProtocolIsImplemented(string extension)
        {
            bool value = false;

            foreach(var protocol in DataManager.ImplementedProtocols)
            {
                if (extension == protocol.FileExtension)
                {
                    value = true;
                }
            }

            //Logger.Log(nameof(ProtocolIsImplemented) + ": " + value);

            return value;
        }
        public bool DataValidated_Initial(string path)
        {
            bool value = false;

            string extension = "";

            if (!IsFileExtension(path))
            {
                return value;
            }

            else
            {
                extension = GetFileExtension(path);

                if (IsSupportedFileExtension(extension) && ProtocolIsImplemented(extension))
                {
                    value = true;
                    return value;
                }

            }

            return value;
        }
        #endregion
    }
}
