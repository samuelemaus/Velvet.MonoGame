using System;
using System.Collections.Generic;
using System.IO;
using Velvet.DataIO;
using Velvet;
using Microsoft.Xna.Framework;
using System.Linq;
using System.ComponentModel;
using System.Text.RegularExpressions;


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

            
            

            TestPerson[] testPeople = Manager.LoadObjects<TestPerson>("TestData.csv");

            foreach(var person in testPeople)
            {
                Manager.Logger.Log(person.Name);
                Manager.Logger.Log(person.Age);
                Manager.Logger.Log(person.EyeColor);
                Manager.Logger.Log(person.HairColor);
                Manager.Logger.Log(person.Position);
            }


        }
    }
}
