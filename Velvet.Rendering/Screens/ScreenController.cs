using System;
using System.Collections.Generic;
using System.Text;
using Velvet;

namespace Velvet.Rendering
{
    public class ScreenController
    {
        private GameScreen gameScreen = DefaultScreen;
        public GameScreen CurrentScreen { get; set; }

        public static GameScreen DefaultScreen;



    }
}
