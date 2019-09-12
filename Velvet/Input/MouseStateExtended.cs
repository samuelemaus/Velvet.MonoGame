using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.Input
{
    /// <summary>
    /// Represents a mouse state with cursor position and button press information.
    /// </summary>
    public struct MouseStateExtended
    {
        float _x, _y;
        float _scrollWheelValue;
        ButtonState _leftButton;
        ButtonState _rightButton;
        ButtonState _middleButton;
        ButtonState _xButton1;
        ButtonState _xButton2;
        float _horizontalScrollWheelValue;

        /// <summary>
        /// Initializes a new instance of the MouseState.
        /// </summary>
        /// <param name="x">Horizontal position of the mouse in relation to the window.</param>
        /// <param name="y">Vertical position of the mouse in relation to the window.</param>
        /// <param name="scrollWheel">Mouse scroll wheel's value.</param>
        /// <param name="leftButton">Left mouse button's state.</param>
        /// <param name="middleButton">Middle mouse button's state.</param>
        /// <param name="rightButton">Right mouse button's state.</param>
        /// <param name="xButton1">XBUTTON1's state.</param>
        /// <param name="xButton2">XBUTTON2's state.</param>
        /// <remarks>Normally <see cref="Mouse.GetState()"/> should be used to get mouse current state. The constructor is provided for simulating mouse input.</remarks>
        public MouseStateExtended(
            float x,
            float y,
            float scrollWheel,
            ButtonState leftButton,
            ButtonState middleButton,
            ButtonState rightButton,
            ButtonState xButton1,
            ButtonState xButton2)
        {
            _x = x;
            _y = y;
            _scrollWheelValue = scrollWheel;
            _leftButton = leftButton;
            _middleButton = middleButton;
            _rightButton = rightButton;
            _xButton1 = xButton1;
            _xButton2 = xButton2;
            _horizontalScrollWheelValue = 0;
        }

        /// <summary>
        /// Initializes a new instance of the MouseState.
        /// </summary>
        /// <param name="x">Horizontal position of the mouse in relation to the window.</param>
        /// <param name="y">Vertical position of the mouse in relation to the window.</param>
        /// <param name="scrollWheel">Mouse scroll wheel's value.</param>
        /// <param name="leftButton">Left mouse button's state.</param>
        /// <param name="middleButton">Middle mouse button's state.</param>
        /// <param name="rightButton">Right mouse button's state.</param>
        /// <param name="xButton1">XBUTTON1's state.</param>
        /// <param name="xButton2">XBUTTON2's state.</param>
        /// <param name="horizontalScrollWheel">Mouse horizontal scroll wheel's value.</param>
        /// <remarks>Normally <see cref="Mouse.GetState()"/> should be used to get mouse current state. The constructor is provided for simulating mouse input.</remarks>
        public MouseStateExtended(
            float x,
            float y,
            float scrollWheel,
            ButtonState leftButton,
            ButtonState middleButton,
            ButtonState rightButton,
            ButtonState xButton1,
            ButtonState xButton2,
            float horizontalScrollWheel)
        {
            _x = x;
            _y = y;
            _scrollWheelValue = scrollWheel;
            _leftButton = leftButton;
            _middleButton = middleButton;
            _rightButton = rightButton;
            _xButton1 = xButton1;
            _xButton2 = xButton2;
            _horizontalScrollWheelValue = horizontalScrollWheel;
        }

        /// <summary>
        /// Compares whether two MouseState instances are equal.
        /// </summary>
        /// <param name="left">MouseState instance on the left of the equal sign.</param>
        /// <param name="right">MouseState instance  on the right of the equal sign.</param>
        /// <returns>true if the instances are equal; false otherwise.</returns>
        public static bool operator ==(MouseStateExtended left, MouseStateExtended right)
        {
            return left._x == right._x &&
                   left._y == right._y &&
                   left._leftButton == right._leftButton &&
                   left._middleButton == right._middleButton &&
                   left._rightButton == right._rightButton &&
                   left._scrollWheelValue == right._scrollWheelValue &&
                   left._horizontalScrollWheelValue == right._horizontalScrollWheelValue &&
                   left._xButton1 == right._xButton1 &&
                   left._xButton2 == right._xButton2;
        }

        /// <summary>
        /// Compares whether two MouseState instances are not equal.
        /// </summary>
        /// <param name="left">MouseState instance on the left of the equal sign.</param>
        /// <param name="right">MouseState instance  on the right of the equal sign.</param>
        /// <returns>true if the objects are not equal; false otherwise.</returns>
        public static bool operator !=(MouseStateExtended left, MouseStateExtended right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified object.
        /// </summary>
        /// <param name="obj">The MouseState to compare.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is MouseStateExtended)
                return this == (MouseStateExtended)obj;
            return false;
        }

        /// <summary>
        /// Gets the hash code for MouseState instance.
        /// </summary>
        /// <returns>Hash code of the object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)_x;
                hashCode = (hashCode * 397) ^ (int)_y;
                hashCode = (hashCode * 397) ^ (int)_scrollWheelValue;
                hashCode = (hashCode * 397) ^ (int)_horizontalScrollWheelValue;
                hashCode = (hashCode * 397) ^ (int)_leftButton;
                hashCode = (hashCode * 397) ^ (int)_rightButton;
                hashCode = (hashCode * 397) ^ (int)_middleButton;
                hashCode = (hashCode * 397) ^ (int)_xButton1;
                hashCode = (hashCode * 397) ^ (int)_xButton2;
                return hashCode;
            }
        }

        public static implicit operator MouseStateExtended(MouseState mouseState)
        {
            return new MouseStateExtended(mouseState.X, mouseState.Y, mouseState.ScrollWheelValue, mouseState.LeftButton, mouseState.MiddleButton, mouseState.RightButton, mouseState.XButton1, mouseState.XButton2);
        }


        #region//Methods
        private ButtonState GetButtonState(MouseButtons button)
        {
            ButtonState value = default;

            switch (button)
            {
                case MouseButtons.Left: value = this.LeftButton; break;
                case MouseButtons.Right: value = this.RightButton; break;
                case MouseButtons.Middle: value = this.MiddleButton; break;
                case MouseButtons.X1: value = this.XButton1; break;
                case MouseButtons.X2: value = this.XButton2; break;

            }

            return value;
        }
        public bool IsButtonDown(MouseButtons button)
        {
            return GetButtonState(button) == ButtonState.Pressed;
        }

        public bool IsButtonUp(MouseButtons button)
        {
            return GetButtonState(button) == ButtonState.Released;
        }



        #endregion

        #region//Content
        /// <summary>
        /// Gets horizontal position of the cursor in relation to the window.
        /// </summary>
        public float X
        {
            get
            {
                return _x;
            }
             set
            {
                _x = value;
            }
        }

        /// <summary>
        /// Gets vertical position of the cursor in relation to the window.
        /// </summary>
		public float Y
        {
            get
            {
                return _y;
            }
             set
            {
                _y = value;
            }
        }

        /// <summary>
        /// Gets cursor position.
        /// </summary>
        public Vector2 Position
        {
            get { return new Vector2(_x, _y); }
        }
        /// <summary>
        /// Gets state of the left mouse button.
        /// </summary>
		public ButtonState LeftButton
        {
            get
            {
                return _leftButton;
            }
             set { _leftButton = value; }
        }

        /// <summary>
        /// Gets state of the middle mouse button.
        /// </summary>
		public ButtonState MiddleButton
        {
            get
            {
                return _middleButton;
            }
             set { _middleButton = value; }
        }

        /// <summary>
        /// Gets state of the right mouse button.
        /// </summary>
		public ButtonState RightButton
        {
            get
            {
                return _rightButton;
            }
             set { _rightButton = value; }
        }

        /// <summary>
        /// Returns cumulative scroll wheel value since the game start.
        /// </summary>
		public float ScrollWheelValue
        {
            get
            {
                return _scrollWheelValue;
            }
             set { _scrollWheelValue = value; }
        }

        /// <summary>
        /// Returns the cumulative horizontal scroll wheel value since the game start
        /// </summary>
        public float HorizontalScrollWheelValue
        {
            get
            {
                return _horizontalScrollWheelValue;
            }
             set { _horizontalScrollWheelValue = value; }
        }

        /// <summary>
        /// Gets state of the XButton1.
        /// </summary>
		public ButtonState XButton1
        {
            get
            {
                return _xButton1;
            }
             set
            {
                _xButton1 = value;
            }
        }

        /// <summary>
        /// Gets state of the XButton2.
        /// </summary>
		public ButtonState XButton2
        {
            get
            {
                return _xButton2;
            }
             set
            {
                _xButton2 = value;
            }
        }
        #endregion


    }
}
