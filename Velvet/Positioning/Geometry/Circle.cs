using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{

    //Credit for concept to R.B. Whitaker: http://rbwhitaker.wikidot.com/circle-collision-detection

    public struct Circle : IEquatable<Circle>
    {
        public readonly Vector2 Center;
        public readonly float Radius;

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Contains(Vector2 position)
        {
            return ((position - Center).Length() <= Radius);
        }
        public bool Intersects(Circle other)
        {
            return ((other.Center - Center).Length() < (other.Radius - Radius));
        }

        //public bool Intersects(Rectangle rect)
        //{

        //}

        public bool Equals(Circle other)
        {
            return (this.Center == other.Center & this.Radius == other.Radius);
        }


    }
}
