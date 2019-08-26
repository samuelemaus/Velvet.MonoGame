using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Velvet.DataIO
{
    public class DataManager
    {
        public ILogger Logger = new ConsoleLogger();
        public DataReader Reader { get; set; } = new DataReader();
        public DataConverter Converter { get; set; } = new DataConverter();

        public T[] LoadObjects<T>(string path) where T : class, new()
        {
            var data = Reader.GetRawFileData(path);

            return Converter.CreateObjectsFromData<T>(data);

        }
        public T Load<T>(string path, int lineIndex) where T : class, new()
        {
            var data = Reader.GetRawFileData(path);

            return Converter.CreateObjectFromData<T>(data, lineIndex);

        }

        #region//Setings
        public static string DefaultDirectory = "Content";

        private static string FullDirectoryPath = Path.GetFullPath(@"..\..\..\" + DefaultDirectory + @"\");
        #endregion


        public static string GetFullPath(string entry)
        {
            return FullDirectoryPath + entry;
        }

        public static List<string> FileExtensions()
        {
            List<string> returnList = new List<string>();

            foreach(var p in ImplementedProtocols)
            {
                returnList.Add(p.FileExtension);
            }

            return returnList;
        }

        public static readonly List<IFileTypeProtocol> ImplementedProtocols = new List<IFileTypeProtocol>()
        {

            new CSVProtocol()

        };



        

    }
}
