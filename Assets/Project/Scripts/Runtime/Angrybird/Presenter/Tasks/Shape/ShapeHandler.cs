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

        [SerializeField] private ShapeSO shapeData;
        private void Awake()
        {
            _shapeGenerator = new GenericShape();
            _shapeVisual = GetComponent<ShapeVisual>();
            
            _shapeGenerator.Initialize(shapeData);
            _shapeGenerator.Calculate(shapeData);
            vertices = _shapeGenerator.GetVertices(shapeData);
        }

        private void Start()
        {
            _shapeVisual.Draw(_shapeGenerator.GetVertices(shapeData));
        }
    }
}