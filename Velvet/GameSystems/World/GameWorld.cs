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

        public GameWorld(string tileMapName)
        {
            TileMap = new TileMap(tileMapName);
            Camera = new OrthoCamera(SceneController.Instance.Renderer.Viewport);
        }       

        public Dimensions2D Dimensions { get; protected set; }

        private BoundingRect worldBounds;
        public BoundingRect WorldBounds => worldBounds;
        public static float CullRange { get; set; } = 150f;
        public OrthoCamera Camera { get; set; }
        public TileMap TileMap { get; set; } = new TileMap();
        public TileSpace[,] Spaces { get; set; }
        public bool Initialized { get; private set; }

        #region//XNA Methods
        public void LoadContent()
        {
            TileMap.LoadContent();
            //for (int i = 0; i < GameObjects.Count; i++)
            //{
            //    GameObjects[i].LoadContent();
            //}
            worldBounds = TileMap.BoundingRect;
            Camera.WorldBounds = new BoundingRect(WorldBounds.Dimensions.Center, WorldBounds.Dimensions);
        }

        public void UnloadContent()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TileMap.Draw(spriteBatch);
            //for (int i = 0; i < GameObjects.Count; i++)
            //{
            //    GameObjects[i].Draw(spriteBatch);
            //}
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
        }
        #endregion


    }
}
