using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.GameSystems
{
    public class GameEventController
    {
        public List<GameEvent> EventQueue = new List<GameEvent>();

        public IGameEventContext CurrentContext { get; protected set; }

        protected virtual void OrderGameEvents(List<GameEvent> events)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        protected virtual void OnEventQueueUpdated()
        {

        }

    }
}
