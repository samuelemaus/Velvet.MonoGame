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
        public static MemberInfo[] GetBasicMemberInfos(this Type type)
        {
            List<MemberInfo> returnList = new List<MemberInfo>();

            returnList.AddRange(type.GetProperties());
            returnList.AddRange(type.GetFields());

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
        public static DataInfoIndex<T> ToDataInfoIndex<T>(this Type type) where T : class
        {
            return new DataInfoIndex<T>(type);
        }
        public static MemberInfo GetMemberInfoByArguments(this Type type, string memberName, Type memberType)
        {
            MemberInfo value = default;

            foreach(var info in type.GetBasicMemberInfos())
            {
                if(memberName == info.Name && memberType == info.GetUnderlyingType())
                {
                    return info;
                }
            }

            return value;
        }
        public static void SetMemberValue(this MemberInfo info, object target, object value)
        {
            MemberTypes type = info.MemberType;



            if(type == MemberTypes.Property)
            {
                PropertyInfo prop = info as PropertyInfo;

                try
                {
                    if (value != null && value != "")
                    {
                        prop.SetValue(target, value);
                    }
                    else
                    {
                        prop.GetValue(target);
                    }

                   
                }

                catch(Exception)
                {
                    prop.GetValue(target);
                }



            }

            else if(type == MemberTypes.Field)
            {
                FieldInfo field = info as FieldInfo;

                try
                {
                    field.SetValue(target, value);
                }

                catch (Exception)
                {
                    field.GetValue(target);
                }
            }
            
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

        public static TypeParser GetParserByType(this Type type)
        {
            TypeParser value = default;

            foreach (var parser in TypeParser.Parsers)
            {
                if (type == parser.Type)
                {
                    value = parser;

                    return value;
                }
            }

            bool checkForParent = value == default;

            if (checkForParent)
            {
                foreach (var parser in TypeParser.Parsers)
                {
                    if (type.BaseType == parser.Type)
                    {
                        value = parser;
                    }
                }
            }


            return value;
        }




    }


}
