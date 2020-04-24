using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace Velvet.GameSystems
{
    public struct BoundingTriangle
    {


        private float lengthA;
        private float lengthB;
        private float lengthC;

        public float LengthA => lengthA;
        public float LengthB => lengthB;
        public float LengthC => lengthC;

        private float angleA;
        private float angleB;
        private float angleC;

        public float AngleA => angleA;
        public float AngleB => angleB;
        public float AngleC => angleC;


        private Vector2 vertexA;
        private Vector2 vertexB;
        private Vector2 vertexC;

        public Vector2 VertexA => vertexA;
        public Vector2 VertexB => vertexB;
        public Vector2 VertexC => vertexC;

        
        

        public BoundingTriangle(Vector2 _vertexA, Vector2 _vertexB, Vector2 _vertexC)
        {
            vertexA = _vertexA;
            vertexB = _vertexB;
            vertexC = _vertexC;

            lengthA = GetLength(vertexA, vertexB);
            lengthB = GetLength(vertexB, vertexC);
            lengthC = GetLength(vertexC, vertexA);

            angleA = GetAngle(lengthC, lengthB, lengthA);
            angleB = GetAngle(lengthC, lengthA, lengthB);
            angleC = GetAngle(lengthA, lengthB, lengthC);

            float sum = angleA + angleB + angleC;


            //angleC = 180 - (angleA + angleB);

        }

        

        ///
        private static float GetLength(Vector2 vertex1, Vector2 vertex2)
        {
            return (vertex1 - vertex2).Length();
        }


        private static float GetAngle(float a, float b, float c)
        {
            return ((a*a) + (b*b) - (c*c)) / (2 * (a*b));
        }

        public override string ToString()
        {
            return $"(Vertices: {VertexA},{VertexB},{VertexC}),(Lengths: {LengthA},{LengthB},{LengthC}),(Angles: {AngleA},{AngleB},{AngleC})";
        }

    }
}
