using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace Velvet.GameSystems
{
    public struct Triangle : IDimensions2D
    {
        public float LengthA => GetLength(VertexA, VertexB);
        public float LengthB => GetLength(VertexB, VertexC);
        public float LengthC => GetLength(VertexC, VertexA);

        //public readonly float AngleA;
        //public readonly float AngleB;
        //public readonly float AngleC;

        public Dimensions2D Dimensions => throw new NotImplementedException();

        public readonly Vector2 VertexA;
        public readonly Vector2 VertexB;
        public readonly Vector2 VertexC;

        public Triangle(Vector2 vertexA, Vector2 vertexB, Vector2 vertexC)
        {
            VertexA = vertexA;
            VertexB = vertexB;
            VertexC = vertexC;
        }

        

        ///
        private float GetLength(Vector2 vertex1, Vector2 vertex2)
        {
            Vector2 difference = vertex2 - vertex1;

            return Math.Abs((difference.X * difference.Y));
        }

        public void SetDimensions(Dimensions2D value)
        {
            throw new NotImplementedException();
        }

        public void SetWidth(float value)
        {
            throw new NotImplementedException();
        }

        public void SetHeight(float value)
        {
            throw new NotImplementedException();
        }

        //private float GetAngle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3)
        //{

        //}

    }
}
