using Model.Shape;
using UnityEngine;

namespace View.Shapes
{
    public class GenericShape : IShapeGenerator
    {
        private const float CircleDegree = 360f;
        public Vector3[] Vertices { get; private set; }

        public void Initialize(ShapeSO shapeData)
        {
            
        }
        public Vector3[] GetVertices(ShapeSO shapeData)
        {
            return Vertices;
        }
        public void Calculate(ShapeSO shapeData)
        {
            var stepSize = CircleDegree / shapeData.VerticesCount;
            Vertices = new Vector3[shapeData.VerticesCount];
            for (int i = 0; i < shapeData.VerticesCount; i++)
            {
                var angle = (-90 + shapeData.Offset + i * stepSize) * Mathf.Deg2Rad;

                var x = shapeData.Center.x + shapeData.Radius * Mathf.Cos(angle);
                var y = shapeData.Center.y + shapeData.Radius * Mathf.Sin(angle);
                Vertices[i] = new Vector3(x, y, 0);
                Vertices[i] *= shapeData.Size;
            }
        }
    }
}