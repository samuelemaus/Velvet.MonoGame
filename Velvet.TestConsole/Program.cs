using System;
using System.Collections.Generic;
using System.IO;
using Velvet.DataIO;
using Microsoft.Xna.Framework;
using System.Linq;
using System.ComponentModel;

namespace Velvet.TestConsole
{
    class Program
    {

        public static void WriteList(List<string> strings)
        {
            foreach(var s in strings)
            {
                Console.WriteLine(s);
            }
        }

        static void Main(string[] args)
        {


            DataManager Manager = new DataManager();
            Manager.Logger.LoggerActive = true;
            Manager.Reader.Logger = Manager.Logger;

            

            RawFileData data = Manager.Reader.GetRawFileData("TestData.csv");

            //Manager.Converter.RawData = data;
            

            TestPerson person = new TestPerson();

            bool test = Manager.Converter.ValidatedAsType<TestPerson>(person,data);

            //string[] types = Manager.Converter.GetObjectTypeNames<TestPerson>(person);

            Manager.Logger.Log(test.ToString());


            //Manager.Logger.Log(person.GetVariableTypes());

            Manager.Logger.Log(person.ToDataInfoIndex().ToString());

            //string vectorTest = "(5.92379, 7.1275)";

            //Vector2 vector2 = vectorTest.ParseToVector2();

            //Manager.Logger.Log(vector2.ToString());

            int vectorIndex = 0;

            for (int i = 0; i < data.FieldHeadings.Length; i++)
            {
                if(data.FieldHeadings[i] == "Position")
                {
                    vectorIndex = i;
                    Manager.Logger.Log(vectorIndex.ToString());
                }
            }

            for (int i = 0; i < data.Entries.Length; i++)
            {
                string vector = data.Entries[i][vectorIndex];
                Manager.Logger.Log("string: " + vector);

                Vector2 converted = vector.ParseToVector2();
                Manager.Logger.Log("vector: " + converted.ToString());

                Console.WriteLine();        
            }


            

        }
    }
}
