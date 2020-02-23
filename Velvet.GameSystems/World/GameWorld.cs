using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velvet.GameSystems
{
    public class GameWorld
    {

        public GameWorld()
        {

        }

       

        public Dimensions2D Dimensions { get; protected set; }

        private BoundingRect worldBounds;
        public BoundingRect WorldBounds
        {
            get
            {
                return worldBounds;
            }

            set
            {
                worldBounds = value;
            }
        }
        public static float CullRange { get; set; } = 150f;

        public List<GameObject> GameObjects = new List<GameObject>();
        public IDrawableComposite WorldMap { get; set; }

        public TileMap TileMap { get; set; } = new TileMap();

        protected virtual void InitializeWorld()
        {
            worldBounds = new BoundingRect(Vector2.Zero, Dimensions);
        }

        #region//XNA Methods
        public void LoadContent()
        {
            TileMap.LoadContent();
        }

        public void UnloadContent()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {

        }
        #endregion


    }
}
