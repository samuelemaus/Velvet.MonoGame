using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Velvet.DataIO
{
    public abstract class DataConverter
    {


        public bool ValidatedAsType<T>(RawFileData data) where T : class
        {
            Type t = typeof(T);

            bool typeValidated = false;

            string[] objMemberNames = t.ToDataInfoIndex<T>().Index.Keys.AsEnumerable().ToArray();

            string[] rawDataTypeNames = data.FieldHeadings;

            for (int i = 0; i < objMemberNames.Length; i++)
            {
                typeValidated = false;

                for (int j = 0; j < rawDataTypeNames.Length; j++)
                {
                    if(objMemberNames[i] == rawDataTypeNames[j])
                    {
                        typeValidated = true;
                    }
                }

                if (!typeValidated)
                {
                    return false;
                }

            }

            return typeValidated;

        }
        public bool ValuesCompatibleWithMemberTypes<T>(RawFileData data, int lineIndex) where T : class
        {
           

            bool value = false;

            //var objIndex = default;

            //for (int i = 0; i < data.NumMembers; i++)
            //{
            //    Type targetType = null;

            //    foreach(var entry in objIndex.Index)
            //    {
            //        if(entry.Key == data.Entries[lineIndex][i])
            //        {
            //            targetType = entry.Value;
            //        }

            //    }

            //}

            return value;


        }

        #region//Data-to-Objects Conversion
        public T[] CreateObjectsFromData<T>(RawFileData data) where T : class, new()
        {
            int objectsCount = data.Entries.Length;

            T[] objects = new T[objectsCount];

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i] = CreateObjectFromData<T>(data, i);

            }


            return objects;
        }
        public T CreateObjectFromData<T>(RawFileData data, int lineIndex) where T : class, new()
        {
            T value = new T();

            Type t = typeof(T);

            var dataIndex = t.ToDataInfoIndex<T>();

            Type[] typeMap = GetObjectTypeMap(dataIndex, data);

            object[] parameters = new object[data.NumMembers];

            for (int i = 0; i < parameters.Length; i++)
            {

                string valueText = data.Entries[lineIndex][i];

                parameters[i] = CreateFromTypeParser(typeMap[i], valueText, typeMap[i]);

                MemberInfo info = t.GetMemberInfoByArguments(data.FieldHeadings[i], typeMap[i]);

                info.SetMemberValue(value, parameters[i]);

            }


            return value;
        }

        public abstract T CreateObject<T>(string filePath);
        public abstract void WriteToFile(string outputFilePath);

        private Type[] GetObjectTypeMap<T>(DataInfoIndex<T> index, RawFileData data) where T : class
        {
            Type[] types = new Type[data.NumMembers];

            for (int i = 0; i < data.NumMembers; i++)
            {
                foreach(var entry in index.Index)
                {
                    if(data.FieldHeadings[i] == entry.Key)
                    {
                        types[i] = entry.Value;
                    }
                }

            }   

            return types;
        }
        protected object CreateFromTypeParser(Type type, string text, object args = null)
        {
            var parser = type.GetParserByType();
            try
            {
                return parser.InvokeParser(text, args);
            }

            catch (Exception)
            {
                return default;
            }
        }
        #endregion


        #region//Objects-to-Data Conversion




        #endregion


    }
}
