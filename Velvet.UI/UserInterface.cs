using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Velvet.UI
{
    public class UserInterface
    {
        public UserInterface()
        {
            Renderer = new UIRenderer();
            Controller = new UIController();

        }

        public static UIRenderer Renderer { get; private set; }
        public static UIController Controller { get; private set; }
        public static SoundBank SoundBank { get; private set; }
        


        public void LoadContent(ContentManager content, RenderTarget2D renderTarget)
        {
            Renderer.LoadContent(content, renderTarget);
            
            
            
            

        }





        #region//Settings

        public static float HoverDelayInMilliseconds { get; set; } = 900f;



        #endregion


    }
}
