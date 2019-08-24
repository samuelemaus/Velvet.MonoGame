using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.IO;


namespace Velvet.DataIO
{
    public class DataConverter
    {
        public RawFileData RawData { get; set; }

        TypeConverter Converter { get; set; } = new TypeConverter();

        public bool ValidatedAsType<T>(T obj, RawFileData data) where T : class
        {
            bool typeValidated = false;

            string[] objMemberNames = obj.ToDataInfoIndex().Index.Keys.AsEnumerable().ToArray();
            



            //RawData Info
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

        int GetLongerArray(string[] array1, string[] array2)
        {
            int length1 = array1.Length;
            int length2 = array2.Length;

            int value = 0;

            if(length1 >= length2)
            {
                value = length1;
            }

            else
            {
                value = length2;
            }

            return value;
        }

        public bool CanSafelyConvert;

        //protected DataInfoIndex<T> ExtractToDataInfoIndex<T>(T obj, RawFileData data) where T : class
        //{
        //    Type[] targetTypes = obj.GetVariableTypes();



        //}

        public bool ValuesCompatibleWithMemberTypes<T>(T obj, RawFileData data, int lineIndex) where T : class
        {
            bool value = false;

            var objIndex = obj.ToDataInfoIndex();

            for (int i = 0; i < data.NumMembers; i++)
            {
                Type targetType = null;

                foreach(var entry in objIndex.Index)
                {
                    if(entry.Key == data.Entries[lineIndex][i])
                    {
                        targetType = entry.Value;
                    }

                }



            }

            return value;


        }

        public  T[] LoadObjects<T>(T obj, RawFileData data) where T : class
        {
            int objectsCount = data.Entries.Length;

            T[] objects = new T[objectsCount];

            DataInfoIndex<T> objDataIndex = obj.ToDataInfoIndex();

            if (ValidatedAsType(obj, data))
            {
                Type[] targetTypes = GetTypeMap<T>(objDataIndex, data);

                for (int i = 0; i < objects.Length; i++)
                {
                    T newObject = default;

                    string[] valuesString = data.Entries[i];

                    for (int j = 0; j < data.NumMembers; j++)
                    {
                        Type targetType = targetTypes[j];

                        string typeValueString = valuesString[j];

                        object value = targetType.TryParseFromString(typeValueString);

                        ITypeDescriptorContext context;

                        
                        

                    }


                    objects[i] = newObject;

                }
            }


            return objects;

        }
      
        private Type[] GetTypeMap<T>(DataInfoIndex<T> index, RawFileData data) where T : class
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
    }
}
