using System;
using System.Collections.Generic;
using System.Text;


namespace Velvet.EntityComponentSystem
{

    public abstract class Component
    {
        public Component(Entity parent, string name = null)
        {
            ParentEntity = parent;
            if(name != null)
            {
                Name = name;
            }

            else
            {
                Name = this.GetType().Name;
            }
        }

        public Component()
        {

        }

        public Entity ParentEntity { get; protected set; }
        public uint ID { get; protected set; }
        public string Name { get; protected set; }
        public List<object> Tags { get; set; } = new List<object>();

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                if(value == true)
                {
                    OnComponentEnabled();
                }

                else
                {
                    OnComponentDisabled();
                }
            }
        }

        protected virtual void OnComponentEnabled()
        {
            Enabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnComponentDisabled()
        {
            Disabled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Enabled;
        public event EventHandler<EventArgs> Disabled;

        public void Destroy()
        {
            OnComponentDestroyed();
        }
        protected virtual void OnComponentDestroyed()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
            if (!DestroyOnRemoval)
            {
                ParentEntity.Components.Remove(this);
            }
        }

        public event EventHandler<EventArgs> Destroyed;

        public bool DestroyOnRemoval { get; set; } = false;
        public bool EnableOnAddition { get; set; } = true;




    }

}
