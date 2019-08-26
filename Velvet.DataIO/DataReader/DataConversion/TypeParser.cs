using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Velvet.DataIO
{
    public delegate T ParseFromString<T>(string text, object args = null);

    public abstract class TypeParser
    {
        #region//Content

        public abstract object InvokeParser(string text, object args = null);
        public virtual Type Type { get;  }
        public static List<object> ParseMethods { get; } = new List<object>();
        public static List<TypeParser> Parsers { get; } = new List<TypeParser>();

        public static ParseFromString<T> GetParseMethodByType<T>()
        {
            ParseFromString<T> returnDel = default;

            bool checkParent = returnDel == default;

            Type t = typeof(T);


            foreach (var p in ParseMethods)
            {


                if (p.GetType() == t)
                {
                    var parser = p as TypeParser<T>;

                    returnDel = parser.ParseFromStringMethod;
                }

            }

            if (checkParent)
            {
                foreach (var p in ParseMethods)
                {
                    Type parentType = p.GetType();

                    if (t == parentType)
                    {
                        var parser = p as TypeParser<T>;

                        returnDel = parser.ParseFromStringMethod;
                    }

                }
            }


            return returnDel;
        }
        #endregion

        #region//Pre-Implemented TypeParsers

        #region//Primitives
        static readonly TypeParser<string> StringParser = new TypeParser<string>(ParseToString);
        static readonly TypeParser<int> IntParser = new TypeParser<int>(ParseToInt);
        static readonly TypeParser<float> FloatParser = new TypeParser<float>(ParseToFloat);
        static readonly TypeParser<double> DoubleParser = new TypeParser<double>(ParseToDouble);
        static readonly TypeParser<bool> BoolParser = new TypeParser<bool>(ParseToBool);
        static readonly TypeParser<char> CharParser = new TypeParser<char>(ParseToChar);
        static readonly TypeParser<Enum> EnumParser = new TypeParser<Enum>(ParseToEnum);

        #endregion

        #region//XNA Types
        static readonly TypeParser<Vector2> Vector2Parser = new TypeParser<Vector2>(ParseToVector2);
        #endregion

        #region//ParseFromStringMethods & RegexFormats

        #region//Primitives
        static string ParseToString(string text, object args = null)
        {
            return text;
        }
        static int ParseToInt(string text, object args = null)
        {
            return int.Parse(text);
        }
        static float ParseToFloat(string text, object args = null)
        {
            return float.Parse(text);
        }
        static double ParseToDouble(string text, object args = null)
        {
            return double.Parse(text);
        }
        static bool ParseToBool(string text, object args = null)
        {
            bool isBinary = text == "1" || text == "0";

            if (!isBinary)
            {
                return bool.Parse(text);
            }

            else if (text == "1")
            {
                return true;
            }

            else
            {
                return false;
            }
            
        }
        static char ParseToChar(string text, object args = null)
        {
            return char.Parse(text);
        }
        static Enum ParseToEnum(string text, object args)
        {
            Enum value = default;

           

            Type runtimeType = args.GetType();
            PropertyInfo propInfo = runtimeType.GetProperty("UnderlyingSystemType");
            Type type = (Type)propInfo.GetValue(args, null);

            bool isEnum = type.IsEnum;

            if (isEnum)
            {
                value = (Enum)Enum.Parse(type, text);
            }



            return value;
        }

        #endregion

        #region//XNA Types
        static Vector2 ParseToVector2(string text, object args = null)
        {
            Vector2 value = default;

            Regex regex = new Regex(@"[-+]?[0-9]*\.?[0-9]+");

            float x = 0;
            float y = 0;

            var matches = regex.Matches(text);

            if (matches != null && matches.Count >= 2)
            {
                string xMatch = matches[0].Value;
                string yMatch = matches[1].Value;

                x = float.Parse(xMatch);
                y = float.Parse(yMatch);

                value = new Vector2(x, y);
            }



            return value;

        }
        #endregion


        //
        //static string Vector2ParseFormat = @"[-+]?[0-9]*\.?[0-9]+";

        #endregion

        //////////
        #endregion


    }

    public class TypeParser<T> : TypeParser
    {

        public TypeParser(ParseFromString<T> parseFromString, string pattern = "")
        {
            
            ParseFromStringMethod = parseFromString;

            Parsers.Add(this);
            ParseMethods.Add(this.ParseFromStringMethod);

        }

        public override Type Type => typeof(T);
        

        public ParseFromString<T> ParseFromStringMethod;

        public override object InvokeParser(string text, object args = null)
        {
            return ParseFromStringMethod.Invoke(text, args);
        }

    }

}

