using System;
using System.Collections.Generic;
using System.Text;
using Velvet.UI;

namespace Velvet.GameSystems
{

    public interface IGameEventContext
    {

    }

    public class GameEvent
    {

        public int EventPriority { get; protected set; }

        public GameEventTrigger Trigger { get; protected set; }

        protected virtual void Initialize()
        {

        }

    }
}
