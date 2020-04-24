using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Velvet.Input
{
    public delegate bool KeyHoldDelegate(Keys key);

    public class InputManager : IInputManager
    {

        #region//Factory
        protected InputManager(params IInputHandler[] handlers)
        {
            InputHandlers = handlers;

            for (int i = 0; i < InputHandlers.Length; i++)
            {
                if (InputHandlers[i] is MouseHandler m)
                {
                    Mouse = m;
                }

                else if (InputHandlers[i] is KeyboardHandler k)
                {
                    Keyboard = k;
                }

                else if (InputHandlers[i] is GamePadHandler g)
                {
                    GamePad = g;
                }
            }
        }

        public static InputManager CreateInputManager()
        {
            return new InputManager(new MouseHandler(), new KeyboardHandler(), new GamePadHandler());
        }

        public static InputManager CreateInputManager<T>() where T : IInputHandler, new()
        {
            return new InputManager(new T());
        }

        public static InputManager CreateInputManager<T,U>() where T : IInputHandler, new()
                                                             where U : IInputHandler, new()
        {
            return new InputManager(new T(), new U());
        }
        #endregion

        public MouseHandler Mouse { get; } 
        public KeyboardHandler Keyboard { get; }
        public GamePadHandler GamePad { get; }
        public Pointer ActivePointer { get; protected set; }
        public IInputHandler[] InputHandlers { get; protected set; }

        #region//Public Methods
        public void Update(GameTime gameTime)
        {
            foreach(var handler in InputHandlers)
            {
                handler.Update(gameTime);
            }
            
        }

        public void SetPointerTargets(IEnumerable<IBoundingRect> targets)
        {
            if(Mouse!= null)
            {
                Mouse.Pointer.ActiveTargets = targets.ToArray();
            }
        }

        #endregion

        #region//Static Helpers

        public static Dictionary<Keys, Direction> KeyDirections = new Dictionary<Keys, Direction>()
        {
            {Keys.Up, Direction.Up },
            {Keys.Down, Direction.Down },
            {Keys.Left, Direction.Left },
            {Keys.Right, Direction.Right }
        };

        #endregion


    }


}
