using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velvet.GameSystems
{

    public abstract class GameObject : GameData
    {
        public GameScene Scene { get; protected set; }

        public string Name { get; protected set; }

        public uint ID { get; protected set; }

        protected List<object> Tags { get; set; } = new List<object>();
        
    }
}
