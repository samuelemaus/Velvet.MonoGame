using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace Velvet.UI
{
    public class Button : ControlObject
    {
        public override IBindableViewObject View => view;

        public override Dimensions2D Dimensions => throw new NotImplementedException();

        public override DimensionsDependency DimensionsDependency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected ButtonView view;

        public override ControlObjectState GetCurrentState()
        {
            throw new NotImplementedException();
        }

        public override void SetWidth(float value)
        {
            throw new NotImplementedException();
        }

        public override void SetHeight(float value)
        {
            throw new NotImplementedException();
        }
    }
}
