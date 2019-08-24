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

        //public T Load<T>(string path) where T : class
        //{

        //}

        public void Load(string path)
        {
            bool initialValidated = Reader.DataValidated_Initial(path);

            if (initialValidated)
            {
                string[] formatted = Reader.GetFormattedData(path);
                Logger.Log(formatted.Length.ToString());
                
            }
        }

        public void RLoad(string path)
        {
            bool initialValidated = Reader.DataValidated_Initial(path);

            if (initialValidated)
            {
                RawFileData data = Reader.GetRawFileData(path);
                //Logger.Log(data.ToString());

                
            }

        }

        public static string DefaultDirectory = "Content";

        private static string FullDirectoryPath = Path.GetFullPath(@"..\..\..\" + DefaultDirectory + @"\");

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
