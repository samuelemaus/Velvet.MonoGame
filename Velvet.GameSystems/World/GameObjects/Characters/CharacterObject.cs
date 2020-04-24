using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velvet.Rendering;

namespace Velvet.GameSystems
{
    public class CharacterObject : GameObject, IUpdate
    {

        #region//Constructors

        public CharacterObject(string filePath)
        {
            Image = new CharacterImage(this,filePath);
        }


        #endregion

        #region//Content
        private Direction facingDirection = Direction.Down;
 
        public Direction FacingDirection
        { 
            get => facingDirection;
            set
            { 
                Image.SetRegion(Image.TextureAtlas[value]);
                SetField<Direction>(ref facingDirection, value);
            }
        }
        public CharacterImage Image { get; set; }

        #endregion


        public override void LoadContent()
        {
            if(Image != null)
            {
                SceneController.CurrentScene.LoadImageContent(Image);
                Image.InitializeTextureAtlas();
            }
            
        }

        protected virtual void Initialize()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }


        public override void Update(GameTime gameTime)
        {
            
        }

        public override void UnloadContent()
        {

        }
    }
}
