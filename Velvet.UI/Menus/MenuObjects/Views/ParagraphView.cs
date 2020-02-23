using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Velvet.UI
{
    //Function for returning Paragraph Words derived from https://stackoverflow.com/questions/4970538/how-to-get-all-words-of-a-string-in-c

    public class ParagraphView : MenuView
    {
        public ParagraphView(string input)
        {
            ParagraphText = input;
        }

        public ParagraphView(string input, BoundingRect targetRect, float writeSpeed = default)
        {

        }

        public ParagraphView(string input, Vector2 topLeftPosition)
        {

        }



        private string paragraphText;
        public string ParagraphText
        {
            get
            {
                return paragraphText;
            }

            set
            {
                paragraphText = value;
                ParagraphWords = GetParagraphWords(value);
            }
        }
        public string[] ParagraphWords { get; private set; }
        private string[] GetParagraphWords(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select m.Value;

            return words.ToArray();
        }

        /// <summary>
        /// The <see cref="Vector2"/> Position at which each word of the <see cref="ParagraphText"/> is written.  This array is updated any time that the text changes or the <see cref="BoundingRect"/> changes.
        /// </summary>
        public Vector2[] WordPositions { get; private set; }
        private Vector2[] GetWordPositions(string[] words)
        {



            return default;
        }

        public int WordCount => ParagraphWords.Length;


        #region//Settings
        /// <summary>
        /// The speed at which the <see cref="ParagraphText"/> is written, measured in milliseconds.  If 0, the text is drawn instantly.
        /// </summary>
        public float WriteSpeed { get; set; } = 0;
        /// <summary>
        /// The amount of space between each word.
        /// </summary>
        public float SpaceWidth { get; set; }
        public bool TabFirstWord { get; set; } = false;
        

        public float BoundaryPaddingHeight { get; set; } = 0;
        public float BoundaryPaddingWidth { get; set; } = 0;

        #endregion

    }
}
