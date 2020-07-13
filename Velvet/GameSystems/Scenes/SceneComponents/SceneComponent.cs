using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{
    public class SceneComponent : Component<GameScene>, IUpdate
    {

        public int UpdateOrder { get; set; }

        public virtual void Update(GameTime gameTime)
        {

        }

    }
}
