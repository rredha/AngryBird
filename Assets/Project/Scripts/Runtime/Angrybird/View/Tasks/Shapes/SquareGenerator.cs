using Model.Shape;
using UnityEngine;

namespace View.Shapes
{
    public class SquareGenerator : IShapeGenerator
    {
        private Vector3[] _vertices;
        
        public void Initialize(ShapeSO shapeData)
        {
        }
        public void Calculate(ShapeSO shapeData)
        {
            _vertices = new Vector3[shapeData.VerticesCount];
            _vertices[0] = new Vector3(0, 0, 0);
            _vertices[1] = new Vector3(0, 1, 0);
            _vertices[2] = new Vector3(1, 1, 0);
            _vertices[3] = new Vector3(1, 0, 0);
        }
        
        public Vector3[] GetVertices(ShapeSO shapeData)
        {
            for (var i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] *= shapeData.Size;
            }

            return _vertices;
        }
    }
}