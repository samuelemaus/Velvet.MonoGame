using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Velvet.Input;

namespace Velvet.GameSystems
{
    public abstract class GameWorldScene : GameScene, IWorldContainer
    {
        public GameWorld World { get; set; }


        #region//Private Helpers

        protected static Keys up = Keys.Up;
        protected static Keys down = Keys.Down;
        protected static Keys right = Keys.Right;
        protected static Keys left = Keys.Left;

        #endregion

        //protected virtual void MoveCharacterWithKey(CharacterObject charObj, float speed)
        //{
        //    HoldToMove(charObj, speed, Input.Keyboard.KeyPressed);
        //    HoldToMove(charObj, speed, Input.Keyboard.KeyDown);
        //}

        //protected virtual void HoldToMove(CharacterObject charObj, float speed, KeyHoldDelegate keyHold)
        //{
        //    if (keyHold.Invoke(Keys.Up))
        //    {
        //        if (!charObj.FacingDirection.Equals(Direction.Up))
        //        {
        //            charObj.FacingDirection = Direction.Up;
        //        }
        //        charObj.MoveY(-speed);
        //    }

        //    if (keyHold.Invoke(Keys.Down))
        //    {
        //        if (!charObj.FacingDirection.Equals(Direction.Down))
        //        {
        //            charObj.FacingDirection = Direction.Down;
        //        }
        //        charObj.MoveY(speed);
        //    }

        //    if (keyHold.Invoke(Keys.Left))
        //    {
        //        if (!charObj.FacingDirection.Equals(Direction.Left))
        //        {
        //            charObj.FacingDirection = Direction.Left;
        //        }
        //        charObj.MoveX(-speed);
        //    }

        //    if (keyHold.Invoke(Keys.Right))
        //    {
        //        if (!charObj.FacingDirection.Equals(Direction.Right))
        //        {
        //            charObj.FacingDirection = Direction.Right;
        //        }
        //        charObj.MoveX(speed);
        //    }
        //}

        //protected virtual void PressToMove(CharacterObject charObj, float speed)
        //{

        //}

        //protected bool CharacterIsFacing(CharacterObject charObj, Direction dir)
        //{
        //    return charObj.FacingDirection.Equals(dir);
        //}

        

    }

    
}
