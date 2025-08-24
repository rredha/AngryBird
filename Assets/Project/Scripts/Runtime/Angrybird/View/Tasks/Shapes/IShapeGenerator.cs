using Model.Shape;
using UnityEngine;

namespace View.Shapes
{
    public interface IShapeGenerator
    {
        void Initialize(ShapeSO shapeData);
        Vector3[] GetVertices(ShapeSO shapeData);
        void Calculate(ShapeSO shapeData);
    }
}