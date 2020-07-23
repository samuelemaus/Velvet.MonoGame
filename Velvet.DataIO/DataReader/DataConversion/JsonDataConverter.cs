using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Velvet.BaseExtensions;

namespace Velvet.DataIO
{
    public class JsonDataConverter : DataConverter
    {

        protected string LoadJsonDataToString(string filePath)
        {
            using (var reader = new StreamReader(File.OpenRead(filePath)))
            {
                string data = "";

                while (!reader.EndOfStream)
                {
                    data += reader.ReadLine();
                }
                return data;
            }
        }

        public override T CreateObject<T>(string filePath)
        {
            string data = LoadJsonDataToString(filePath);

            T obj = default;

            JsonConverter converter;

            using (var reader = new JsonTextReader(new StringReader(data)))
            {
                //obj = converter.ReadJson(reader, obj.GetType(), null, serializer);
            }
            
            return default;
        }

        public JObject ReadJObject(string filePath)
        {
            try
            {
                string fullPath = GetFullPath(filePath);

                return JObject.Parse(LoadJsonDataToString(fullPath));
            }
            catch (Exception)
            {

                throw;
            }           

            return null;
        }

        public override void WriteToFile(string outputFilePath)
        {

        }
    }
}
