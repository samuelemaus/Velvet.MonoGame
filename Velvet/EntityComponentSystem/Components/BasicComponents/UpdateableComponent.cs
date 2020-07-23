using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.EntityComponentSystem
{
    public class UpdateableComponent : Component, IUpdate
    {


        public uint UpdateInterval { get; set; } = 1;

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
