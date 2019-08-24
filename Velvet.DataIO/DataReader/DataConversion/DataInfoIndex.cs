using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Velvet.DataIO
{

    /// <summary>
    /// A basic data structure containing a dictionary of all of the MemberInfo of an object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct DataInfoIndex<T> where T : class
    {
        public readonly Dictionary<string, Type> Index;

        public DataInfoIndex(T obj)
        {
            Index = GetDataIndex(obj);
        }

        private static Dictionary<string, Type> GetDataIndex<T> (T obj) where T : class
        {
            MemberInfo[] infos = obj.GetMemberInfos();

            Dictionary<string, Type> returnDict = new Dictionary<string, Type>();

            for (int i = 0; i < infos.Length; i++)
            {
                Type t = infos[i].GetUnderlyingType();
                string s = infos[i].Name;

                returnDict.Add(s, t);

            }

            return returnDict;
        }

        public override bool Equals(object obj)
        {
            return Index.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string value = "";

            foreach(var entry in Index)
            {
                string s = "(" + entry.Key + ": " + entry.Value.ToString() + ") ";

                value += s;
            }


            return value;
        }
    }
}
