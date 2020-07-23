using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.EntityComponentSystem
{
    public abstract class Entity : IEntity
    {
        
        public IEntityContainer ParentContainer { get; protected set; }

        public virtual GuidManager GuidManager { get; set; }
        public virtual ComponentList Components { get; set; }

        public string Name { get; protected set; }

        public uint ID { get; protected set; }

        protected virtual void InitializeComponents()
        {
            Components = new ComponentList(this);
        }


        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                if(value == true)
                {
                    OnEntityEnabled();
                }
                else
                {
                    OnEntityDisabled();
                }
            }
        }

        public uint UpdateInterval { get; set; } = 1;

        protected virtual void OnEntityEnabled()
        {
            Enabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEntityDisabled()
        {
            Disabled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Enabled;
        public event EventHandler<EventArgs> Disabled;


        public void Destroy()
        {
            
        }

        protected virtual void OnEntityDestroyed()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
        }

        public void Update(GameTime gameTime)
        {
            Components.UpdateComponents(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Components.DrawComponents(spriteBatch);
        }



        public event EventHandler<EventArgs> Destroyed;
    }
}
