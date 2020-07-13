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

        //todo: set enabled

        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnObjectEnabled();
            }
        }

        protected virtual void OnObjectEnabled()
        {
            Enabled?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Enabled;


        public void Destroy()
        {

        }

        protected virtual void OnObjectDestroyed()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Destroyed;

        #region//Components
        protected List<GameObjectComponent> Components { get; set; } = new List<GameObjectComponent>();

        public T GetComponent<T>(T component) where T : GameObjectComponent
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if(Components[i] is T t)
                {
                    return t;
                }
            }

            return null;
        }

        public T GetComponent<T>(uint componentID) where T : GameObjectComponent
        {
            var component = this[componentID];

            if(component is T t)
            {
                return t;
            }

            return null;
        }

        public T GetComponent<T>(string componentName) where T : GameObjectComponent
        {
            var component = this[componentName];

            if (component is T t)
            {
                return t;
            }

            return null;
        }

        public GameObjectComponent this[uint id]
        {
            get
            {
                for (int i = 0; i < Components.Count; i++)
                {
                    if(Components[i].ID == id)
                    {
                        return Components[i];
                    }
                }

                return null;
            }
        }

        public GameObjectComponent this[string name]
        {
            get
            {
                for (int i = 0; i < Components.Count; i++)
                {
                    if (Components[i].Name == name)
                    {
                        return Components[i];
                    }
                }

                return null;
            }
        }

        #endregion

    }
}
