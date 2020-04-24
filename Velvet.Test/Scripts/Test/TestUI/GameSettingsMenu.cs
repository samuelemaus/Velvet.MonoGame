using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvet.UI;
using Velvet.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velvet
{
    public class GameSettingsMenu : Menu
    {
        public GameSettingsMenu(VelvetTestGame game)
        {
            GameData = game;
            //InitializeContent();
            InitializeMenu();
        }

        public VelvetTestGame GameData { get; private set; }

        public Window MainWindow;
        public Window SubWindow;

        ListView MainWindowListView;

        

        BoundingRect windowRect = new BoundingRect(150, 150, 75, 150);

        #region//Overrides

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MainWindow.WindowBackground.Draw(spriteBatch);
        }

        public override void InitializeMenu()
        {
            MainWindow = new Window(windowRect);
        }

        public override void ActivateMenuControls(GameTime gameTime)
        {
            base.ActivateMenuControls(gameTime);


        }


        #endregion

    }
}
