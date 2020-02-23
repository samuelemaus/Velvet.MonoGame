using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Velvet
{
    public static class BaseExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="extensionLength"></param>
        /// <returns></returns>
        public static string TrimFileExtension(this string s, int extensionLength = 3)
        {
            return s.Substring(0, s.Length - (extensionLength + 1));
        }
        public static string GetFileExtension(this string s)
        {
            return s.Substring(s.LastIndexOf('.'));
        }
        public static List<string> DebugMemberInfoList<T>(this T obj) where T : class
        {
            var returnList = new List<string>();

            var infos = typeof(T).GetMembers();

            foreach(var info in infos)
            {
                string name = "";
                string value = "";

                switch (info.MemberType)
                {
                    case MemberTypes.Property:

                        var prop = info as PropertyInfo;
                        name = prop.Name;
                        value = prop.GetValue(obj).ToString();
                        returnList.Add($"{name}: {value}");
                        break;

                    case MemberTypes.Field:
                        var field = info as FieldInfo;
                        name = field.Name;
                        value = field.GetValue(obj).ToString();
                        returnList.Add($"{name}: {value}");
                        break;
                }

                
            }

            return returnList;
        }
        public static List<string> ToStringList<T>(this IEnumerable<T> collection)
        {
            var returnList = new List<string>();

            foreach(var c in collection)
            {
                returnList.Add(c.ToString());
            }

            return returnList;
        }
        public static MemberInfo[] GetBasicMemberInfos(this Type type)
        {
            List<MemberInfo> returnList = new List<MemberInfo>();

            returnList.AddRange(type.GetProperties());
            returnList.AddRange(type.GetFields());

            return returnList.ToArray();
        }

        public static bool ContainsType<T>(this List<T> list, Type type)
        {
            bool value = false;

            foreach(var obj in list)
            {
                if(obj.GetType() == type)
                {
                    value = true;   
                }
            }

            return value;
        }

        public static bool IsPowerOfTwo(int n)
        {
            return (int)(Math.Ceiling((Math.Log(n) / Math.Log(2)))) == (int)(Math.Floor(((Math.Log(n) / Math.Log(2)))));
        }

        public static ValueRange RotationRange = new ValueRange(0, (float)6.283184);

        private static float maxRotation = (float)(Math.PI * 2);

    }
}
