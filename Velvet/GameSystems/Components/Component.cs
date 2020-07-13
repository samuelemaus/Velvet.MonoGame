using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{

    public abstract class Component<T>
    {
        public T Parent { get; protected set; }
        public uint ID { get; protected set; }
        public string Name { get; protected set; }
        protected List<object> Tags { get; set; } = new List<object>();
        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnComponentEnabled();
            }
        }

        protected virtual void OnComponentEnabled()
        {
            Enabled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Enabled;


        public void Destroy()
        {
            OnComponentDestroyed();
        }
        protected virtual void OnComponentDestroyed()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Destroyed;



    }

}
