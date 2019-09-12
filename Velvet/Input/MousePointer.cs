using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet.Input
{
    public class MousePointer : IPointer
    {
        
        public MousePointer(IInputHandler parent)
        {
            ParentHandler = parent;
        }
        public IInputHandler ParentHandler { get; protected set; }
        public object TargetedObject { get; set; }
        public Vector2 Position { get; set; }
        public PositionDependency PositionDependency { get; set; }

    }
}
