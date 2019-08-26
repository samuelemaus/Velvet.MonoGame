using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.DataIO
{
    public struct RawFileData
    {

        public readonly string[] FieldHeadings;
        public readonly string[][] Entries;

        public readonly int NumMembers;

        public RawFileData(string[] headings, string[][] entries, int numFields)
        {
            FieldHeadings = headings;
            Entries = entries;
            NumMembers = numFields;
        }


        private static RawFileData empty = new RawFileData(new string[0], new string[0][], 0);

        public static RawFileData Empty => empty;

        public override string ToString()
        {
            string value = "";

            string numFields = "(" + NumMembers + ")";

            value += numFields;

            for (int i = 0; i < Entries.Length; i++)
            {
                value += " (Field: " + FieldHeadings[i].ToString() +": ";

                for (int j = 0; j < Entries[i].Length; j++)
                {
                    if(Entries[i][j] == "")
                    {
                        value += "null";
                    }
                    else
                    {
                        value += Entries[i][j];
                    }

                    if(j != Entries[i].Length - 1)
                    {
                        value += ", ";
                    }

                }

                value += ") ";
            }



            return value;
        }

    }
}
