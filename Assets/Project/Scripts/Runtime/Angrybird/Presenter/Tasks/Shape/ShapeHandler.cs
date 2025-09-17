using Model.Shape;
using UnityEngine;
using View.Shapes;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape
{
    public class ShapeHandler : MonoBehaviour
    {
        private ShapeVisual _shapeVisual;
        private IShapeGenerator _shapeGenerator;
        public Vector3[] vertices { get; private set; }

        //public ShapeSO ShapeSo { get; set; }

        /*
        private void Awake()
        {
            _shapeGenerator = new GenericShape();
            _shapeVisual = GetComponent<ShapeVisual>();
            
            _shapeGenerator.Initialize(ShapeSo);
            _shapeGenerator.Calculate(ShapeSo);
            vertices = _shapeGenerator.GetVertices(ShapeSo);
        }

        private void Start()
        {
            _shapeVisual.Draw(_shapeGenerator.GetVertices(ShapeSo));
        }
        */

        public void Setup(ShapeSO shapeSo)
        {
            _shapeGenerator = new GenericShape();
            _shapeVisual = GetComponent<ShapeVisual>();
            
            _shapeGenerator.Initialize(shapeSo);
            _shapeGenerator.Calculate(shapeSo);
            vertices = _shapeGenerator.GetVertices(shapeSo);
            _shapeVisual.Draw(_shapeGenerator.GetVertices(shapeSo));
        }
    }
}