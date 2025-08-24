using Model.Shape;
using UnityEngine;

namespace View.Shapes
{
    public class TriangleGenerator : IShapeGenerator
    {
        private float _height;
        private float _sideLength;
        private Vector3[] _vertices;
        public void Initialize(ShapeSO shapeData)
        {
            _height = shapeData.Radius / 2f;
            _sideLength = _height * Mathf.Sqrt(3f) / 2f;
        }
        public void Calculate(ShapeSO shapeData)
        {
            _vertices = new Vector3[shapeData.VerticesCount];
            _vertices[0] = shapeData.Center + new Vector3(0, _height * 2f/3f, 0);
            _vertices[1] = shapeData.Center + new Vector3(- _sideLength/2f, - _height / 3f, 0);
            _vertices[2] = shapeData.Center + new Vector3(  _sideLength/2f, - _height / 3f, 0);

            for (var i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] *= shapeData.Size;
            }
        }
        
        public Vector3[] GetVertices(ShapeSO shapeData)
        {
            return _vertices;
        }
    }
}