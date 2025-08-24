using Model.Shape;
using UnityEngine;

namespace View.Shapes
{
    public class CircleGenerator : IShapeGenerator
    {
        // move this to shape handler for this class only
        // private int bubbleCount;
        private int _segments;
        private Vector3[] _vertices;
        public void Initialize(ShapeSO shapeData)
        {
            _segments = 10;
        }

        public Vector3[] GetVertices(ShapeSO shapeData) => _vertices;

        public void Calculate(ShapeSO shapeData)
        {
            var stepSize = 2f * Mathf.PI / _segments;
            for (var i = 0; i < _segments; i++)
            {
                var angle = i * stepSize;
                var x = Mathf.Cos(angle) * shapeData.Radius * shapeData.Size;
                var y = Mathf.Sin(angle) * shapeData.Radius * shapeData.Size;
                
                _vertices[i] = new Vector3(x, y, 0);
            }
        }
    }
}