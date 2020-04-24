using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet.GameSystems
{

    //Credit for concept to R.B. Whitaker: http://rbwhitaker.wikidot.com/circle-collision-detection

    public struct BoundingCircle : IEquatable<BoundingCircle>
    {
        private Vector2 centerPosition;
        private float radius;

        public Vector2 CenterPosition => centerPosition;
        public float Radius => radius;

        public BoundingCircle(Vector2 _center, float _radius)
        {
            centerPosition = _center;
            radius = _radius;
        }

        public bool Contains(Vector2 position)
        {
            return ((position - CenterPosition).Length() <= Radius);
        }
        public bool Intersects(BoundingCircle other)
        {
            return ((other.CenterPosition - CenterPosition).Length() < (other.Radius - Radius));
        }

        //public bool Intersects(Rectangle rect)
        //{

        //}

        public bool Equals(BoundingCircle other)
        {
            return (this.CenterPosition == other.CenterPosition & this.Radius == other.Radius);
        }


    }
}
