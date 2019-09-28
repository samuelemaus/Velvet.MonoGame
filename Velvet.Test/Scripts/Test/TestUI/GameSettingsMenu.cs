using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.UI;
using Velvet.Rendering;

namespace Velvet
{
    public class GameSettingsMenu : Menu
    {
        public GameSettingsMenu(VelvetTestGame game)
        {
            GameData = game;
        }

        public VelvetTestGame GameData { get; private set; }


        #region//Overrides



        #endregion

    }
}
