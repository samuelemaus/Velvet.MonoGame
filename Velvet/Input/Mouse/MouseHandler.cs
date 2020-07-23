using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velvet.Input
{
    public class MouseHandler : IMouseHandler
    {
        #region//Content
        public MouseHandler()
        {
            Pointer = new Pointer(new MethodPositionDependency(GetMousePosition), pointerMainButtonDown);
        }


        public bool HandlerActive => TimeInactive >= ActiveThreshold;

        public double ActiveThreshold { get; set; } = 10000d;
        public double TimeInactive { get; private set; }

        private void UpdateTimeInactive(double delta)
        {

            if (CurrentMouseState == PrevMouseState)
            {
                if (TimeInactive < ActiveThreshold)
                { TimeInactive += delta; }
                
            }
            
            else
            {
                TimeInactive = 0;
            }

        }

        
        public MouseStateExtended CurrentMouseState { get; private set; }
        public MouseStateExtended PrevMouseState { get; private set; }


        private Pointer pointer;
        public Pointer Pointer { get; set; }
        #endregion

        public float ScrollWheelMovementVertical => CurrentMouseState.ScrollWheelValue - PrevMouseState.ScrollWheelValue;
        public float ScrollWheelMovementHorizontal => CurrentMouseState.HorizontalScrollWheelValue - PrevMouseState.HorizontalScrollWheelValue;

        public uint UpdateInterval => throw new NotImplementedException();

        private bool pointerMainButtonDown()
        {
            return this.BtnDown(MouseButtons.Left);
        }

        #region//Public Methods

        public Vector2 GetMousePosition()
        {
            return (CurrentMouseState.Position / 2).Round();
            //return CurrentMouseState.CenterPosition;
        }

        public bool BtnDown(MouseButtons button)
        {
            return CurrentMouseState.IsButtonDown(button) && PrevMouseState.IsButtonDown(button);
        }

        public bool BtnReleased(MouseButtons button)
        {
            return CurrentMouseState.IsButtonUp(button) && PrevMouseState.IsButtonDown(button);
        }

        public bool BtnClicked(MouseButtons button)
        {
            return CurrentMouseState.IsButtonUp(button) && PrevMouseState.IsButtonDown(button);
        }
        

        public void Update(GameTime gameTime)
        {
            PrevMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            UpdateTimeInactive(gameTime.ElapsedGameTime.TotalMilliseconds);

            if(Pointer.ActiveTargets != null)
            {
                Pointer.GetTargetedObject();
            }

        }

        #endregion



    }
}
