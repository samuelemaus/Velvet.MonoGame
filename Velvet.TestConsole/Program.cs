using System;
using System.Collections.Generic;
using System.IO;
using Velvet.DataIO;
using Velvet;
using Microsoft.Xna.Framework;
using System.Linq;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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

            //DataManager Manager = new DataManager();
            //Manager.Logger.LoggerActive = true;
            //Manager.Reader.Logger = Manager.Logger;

            //TestPerson[] testPeople = Manager.LoadObjects<TestPerson>("TestData.csv");

            //foreach(var person in testPeople)
            //{
            //    Manager.Logger.Log(person.Name);
            //    Manager.Logger.Log(person.Age);
            //    Manager.Logger.Log(person.EyeColor);
            //    Manager.Logger.Log(person.HairColor);
            //    Manager.Logger.Log(person.Position);
            //}

            XDocument tileSetDoc = XDocument.Load(@"C:\Users\samem\OneDrive\Documents\Velvet\Velvet.MonoGame\Velvet.TestConsole\Content\zelda_gbc.tsx");

            XElement tileSet = tileSetDoc.Element("tileset");

            var nodes = tileSet.Nodes();

            string Name = tileSet.Element("image").Attribute("source").Value.ToString().TrimStart(new char[3] { '.', '.', '/' });

            Console.WriteLine(tileSet.ToString());



        }
    }
}
