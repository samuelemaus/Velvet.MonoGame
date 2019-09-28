using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Velvet;
using Velvet.Rendering;

namespace Velvet.UI
{
    public class HUDLogger : CompositeImage, ILogger
    {
        public HUDLogger()
        {

            if (IsFixedLogSize)
            {
                LogMessages = new Queue<string>(MaxLogCapacity);
                textImages = new List<TextImage>(MaxLogCapacity);
            }

            else
            {
                LogMessages = new Queue<string>();
            }

            LoggerActive = true;
        }

        public bool LoggerActive { get; set; }

        public Queue<string> LogMessages { get; set; }

        public override IEnumerable<IDrawableObject> Images => textImages;
        private List<TextImage> textImages { get; set; }
        private Color color = Color.Red;

        public bool IsFixedLogSize { get; set; } = true;
        public int MaxLogCapacity { get; set; } = 30;

        

        public TextAlignment TextAlignment { get; set; }

        public void Log(string message)
        {
            AddToLog(message);
        }

        public void Log(IEnumerable<string> messages)
        {
            throw new NotImplementedException();
        }

        public void Log(object message)
        {
            throw new NotImplementedException();
        }

        public void Log(IEnumerable<object> messages)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {

        }

        private void AddToLog(string newMessage)
        {
            TextImage textImage = new TextImage(newMessage)
            {
                Alignment = TextAlignment,
                Color = color
            };

            LogMessages.Enqueue(newMessage);
        }

        private void UpdatePositions()
        {
            
        }
    }
}
