using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet.Input
{
    public class GamePadHandler : IGamePadHandler
    {
        public bool HandlerActive { get; }
        public double TimeInactive { get; }
        public double ActiveThreshold { get; set; } = 10000d;

        public Pointer pointer;
        public IPointer Pointer => pointer;

        public PlayerIndex[] ActivePlayers = new PlayerIndex[4];

        public GamePadState PrevGamePadState { get; protected set; }
        public GamePadState CurrentGamePadState { get; protected set; }

        public uint UpdateInterval { get; set; } = 1;

        private void UpdateTimeInactive(double delta)
        {

        }

        public void Update(GameTime gameTime)
        {
            PrevGamePadState = CurrentGamePadState;
            
        }
    }
}
