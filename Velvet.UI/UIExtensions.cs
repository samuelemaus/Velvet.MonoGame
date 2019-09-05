﻿using System;
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
    public static class UIExtensions
    {

        public static Dictionary<ToggleButtonType, string> ToggleTypeImagePaths = new Dictionary<ToggleButtonType, string>()
        {

        };

        public static Vector2 ToVector2(this Direction direction, float speed)
        {
            Vector2 returnVector = default;

            float actualSpeed = Math.Abs(speed);

            switch (direction)
            {
                case Direction.Up:

                    returnVector = new Vector2(0, -actualSpeed);

                    break;

                case Direction.Down:

                    returnVector = new Vector2(0, actualSpeed);

                    break;

                case Direction.Left:

                    returnVector = new Vector2(-actualSpeed, 0);

                    break;

                case Direction.Right:

                    returnVector = new Vector2(actualSpeed, 0);

                    break;
            }

            return returnVector;
        }

        


    }
}
