using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.EntityComponentSystem
{
    public class EntityList
    {

        public EntityList(IEntityContainer parent)
        {
            ParentContainer = parent;

        }

        IEntityContainer ParentContainer;

        protected List<Entity> entities;


    }
}
