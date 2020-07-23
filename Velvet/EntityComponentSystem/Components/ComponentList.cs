using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Velvet.EntityComponentSystem
{
    public class ComponentList
    {
        public ComponentList(Entity parent)
        {
            ParentEntity = parent;
        }

        public ComponentList(Entity parent, params Component[] components)
        {

        }


        #region//Content
        protected Entity ParentEntity { get; set; }

        protected List<Component> components;

        protected List<Component> addQueue;
        protected List<Component> removeQueue;

        protected bool componentsInAddQueue;
        protected bool componentsInRemoveQueue;

        protected List<IUpdate> updateableComponents;
        protected List<IDraw> drawableComponents;

        #endregion

        #region//Indexers and Acessors
        public Component this[int i]
        {
            get => components[i];
        }
        public Component this[string name]
        {
            get
            {
                foreach(var component in components)
                {
                    if (component.Name == name)
                    {
                        return component;
                    }
                }

                return null;
            }
        }
        public Component this[uint id]
        {
            get
            {
                foreach (var component in components)
                {
                    if (component.ID == id)
                    {
                        return component;
                    }
                }

                return null;
            }
        }
        public Component this[object obj]
        {
            get
            {
                var components = GetComponentsByTag(obj);

                if(components != null && components.Count == 1)
                {
                    return components[0];
                }

                return null;
            }
        }
        public Component GetComponent<T>() where T : Component
        {
            var componentsOfType = GetComponentsByType<T>();

            if(componentsOfType != null && componentsOfType.Count == 1)
            {
                return componentsOfType[0];
            }

            return null;
        }


        public List<Component> GetComponentsByType<T>() where T : Component
        {
            return (from component in components where component.GetType().Equals(typeof(T)) select component).ToList();
        }
        public List<Component> GetComponentsByTag(object tag)
        {
            return (from component in components where component.Tags.Contains(tag) select component).ToList();
        }


        #endregion

        #region//Public Methods

        public void Add(Component component)
        {
            AddToAddQueue(component);
        }
        public void Remove(Component component)
        {
            AddToRemoveQueue(component);
        }
        public void Remove(uint id)
        {
            if (this.Contains(id))
            {
                AddToRemoveQueue(this[id]);
            }

            else
            {
                throw new Exception($"{ParentEntity} does not contain Component with the ID of {id}.");
            }
        }
        public void Remove(string name)
        {
            if (this.Contains(name))
            {
                Remove(this[name]);
            }

            else
            {
                throw new Exception($"{ParentEntity} does not contain Component with the Name of {name}.");
            }
        }
        public void RemoveAll()
        {
            for (int i = 0, len = components.Count; i < len; ++i)
            {
                AddToRemoveQueue(components[i]);
            }
        }

        public bool Contains(Component component)
        {
            return components.Contains(component);
        }

        public bool Contains(string name)
        {
            return this[name] != null;
        }

        public bool Contains(uint id)
        {
            return this[id] != null;
        }


        #region//XNA Methods


        public void DrawComponents(SpriteBatch spriteBatch)
        {
            for (int i = 0, len = drawableComponents.Count; i < len; ++i)
            {
                drawableComponents[i].Draw(spriteBatch);
            }
        }


        public void UpdateComponents(GameTime gameTime)
        {
            UpdateQueues();

            for (int i = 0, len = updateableComponents.Count; i < len; ++i)
            {
                updateableComponents[i].Update(gameTime);
            }

        }



        #endregion

        #endregion

        #region//Non-Public Methods

        protected void UpdateDrawablesAndUpdateables()
        {
            updateableComponents = GetUpdateableComponents();
            drawableComponents = GetDrawableComponents();
        }

        protected void UpdateQueues()
        {
            if (componentsInAddQueue)
            {
                ProcessAddQueue();
                UpdateDrawablesAndUpdateables();

            }

            if (componentsInRemoveQueue)
            {
                ProcessRemoveQueue();
                UpdateDrawablesAndUpdateables();

            }

        }
        protected bool AddToBaseList(Component component)
        {
            if (!components.Contains(component))
            {
                components.Add(component);
                if (component.EnableOnAddition)
                {
                    component.IsEnabled = true;
                }
                return true;
            }

            return false;
        }
        protected bool RemoveFromBaseList(Component component)
        {
            if (components.Contains(component))
            {
                components.Remove(component);
                return true;
            }

            return false;
        }

        protected bool AddToAddQueue(Component component)
        {
            if (!addQueue.Contains(component))
            {
                addQueue.Add(component);
                componentsInAddQueue = true;
                return true;
            }

            return false;
        }
        protected bool AddToRemoveQueue(Component component)
        {
            if (!removeQueue.Contains(component))
            {
                removeQueue.Add(component);
                componentsInRemoveQueue = true;
                return true;
            }

            return false;
        }




        protected void ProcessAddQueue()
        {
            for (int i = 0, len = addQueue.Count; i < len; ++i)
            {
                bool addSuccessful = AddToBaseList(addQueue[i]);

                if (!addSuccessful)
                {
                    //todo;
                }
            }

            addQueue.Clear();
            componentsInAddQueue = false;
        }
        protected void ProcessRemoveQueue()
        {

            for (int i = 0, len = removeQueue.Count; i < len; ++i)
            {
                bool removeSuccessful = RemoveFromBaseList(removeQueue[i]);

                if (!removeSuccessful)
                {
                    //todo;
                }
            }

            removeQueue.Clear();
            componentsInRemoveQueue = false;
        }

        protected List<IDraw> GetDrawableComponents()
        {
            return (from component in components.OfType<IDraw>() select component).ToList();
        }
        protected List<IUpdate> GetUpdateableComponents()
        {
            return (from component in components.OfType<IUpdate>() select component).ToList();
        }

        #endregion
    }
}
