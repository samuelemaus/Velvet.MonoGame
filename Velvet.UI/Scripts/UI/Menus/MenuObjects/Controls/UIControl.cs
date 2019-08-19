using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.UI
{
    public class UIControl
    {
        protected virtual bool _CanSelect()
        {
            bool value = true;

            //if (ObjectReadOnly)
            //{
            //    value = false;
            //}

            return value;
        }

        public virtual Action ObjectSelected { get; set; }
        public virtual void OnObjectSelected()
        {
            ObjectSelected.Invoke();
        }

        public delegate bool CanBeHovered();
        public CanBeHovered CanHover;

        public virtual Action ObjectHovered { get; set; }
        public virtual void OnObjectHOvered()
        {
            ObjectHovered.Invoke();
        }

    }
}
