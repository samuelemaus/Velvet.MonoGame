using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text.RegularExpressions;

namespace Velvet.DataIO
{
    public static class DataExtensions
    {
        public static ConsoleLogger Logger = new ConsoleLogger();
        public static PropertyInfo[] GetProperties<T>(this T obj) where T : class
        {
            Type type = obj.GetType();

            return type.GetProperties();

        }
        public static FieldInfo[] GetFields<T>(this T obj) where T : class
        {
            Type type = obj.GetType();

            return type.GetFields();
        }
        public static MemberInfo[] GetMemberInfos<T>(this T obj) where T : class
        {
            List<MemberInfo> returnList = new List<MemberInfo>();

            returnList.AddRange(obj.GetProperties());
            returnList.AddRange(obj.GetFields());

            return returnList.ToArray();

        }
        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                     "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
        public static Type[] GetVariableTypes<T>(this T obj) where T : class
        {
            //Type type = obj.GetType();

            List<Type> types = new List<Type>();

            foreach (var propType in obj.GetProperties())
            {
                types.Add(propType.PropertyType);
            }

            foreach(var fieldType in obj.GetFields())
            {
                types.Add(fieldType.FieldType);
            }

            
            return types.ToArray();


        }

        public static Dictionary<Type, string> GetDataIndex(this MemberInfo[] infos)
        {
            Dictionary<Type, string> returnDict = new Dictionary<Type, string>();

            for (int i = 0; i < infos.Length; i++)
            {
                Type t = infos[i].GetUnderlyingType();
                string s = infos[i].Name;

                returnDict.Add(t, s);

            }

            return returnDict;
        }

        public static DataInfoIndex<T> ToDataInfoIndex<T>(this T obj) where T : class
        {
            return new DataInfoIndex<T>(obj);
        }

        public static DataInfoIndex<T> ToDataInfoIndex<T>(this RawFileData data) where T : class
        {
            T obj = default;



            return new DataInfoIndex<T>(obj);
        }

        public static bool IsNullable<T>(this T obj)
        {
            if (obj == null)
            { return true; }

            Type type = typeof(T);

            if (!type.IsValueType)
            {
                return true;
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }
            else
            {
                return false; // value-type
            }
            
        }

        public static bool IsParseableFromString(this Type type)
        {
            bool value = false;

            return value;
        }

        public static T TryParseFromString<T>(this T obj, string text)
        {
            Type type = obj.GetType();

            T value = default;

            if (type.IsParseableFromString())
            {


            }


            return value;
            
        }

        //public static bool DataContains(this RawFileData data, KeyValuePair<string, Type> entry)
        //{

        //}
        
        
        #region//Parse From String Methods

        public static Vector2 ParseToVector2(this string text)
        {
            Vector2 value = default;

            Regex regex = new Regex(Vector2ParseFormat);

            float x = 0;
            float y = 0;

            var matches = regex.Matches(text);

            if(matches != null && matches.Count >= 2)
            {
                string xMatch = matches[0].Value;
                string yMatch = matches[1].Value;

                x = float.Parse(xMatch);
                y = float.Parse(yMatch);

                value = new Vector2(x, y);
            }



            return value;

        }

        private static string Vector2ParseFormat = @"[-+]?[0-9]*\.?[0-9]+";

        #endregion

    }
}
