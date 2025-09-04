using System;
using System.Collections.Generic;
using Model.Shape;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Bubble;
using UnityEngine;
using View.Shapes;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape
{
    public class BubblePopBehaviour : MonoBehaviour, ITaskBehaviour
    {
        [SerializeField] private ShapeVisual shapeVisual;
        public ShapeVisual Visual => shapeVisual;
        [SerializeField]private GameObject _bubblePrefab;
        [SerializeField]private ShapeSO _shapeData;
        private IShapeGenerator _shapeGenerator;
        public Vector3[] vertices { get; private set; }
        private readonly List<BubbleHandler> _bubbles = new();
        private int _shapeCount;

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public event EventHandler TaskComplete;
        public event EventHandler TaskStart;

        /*
        private void OnEnable()
        {
            TaskStart += OnTaskStart_Draw;
            TaskStart += OnTaskStart_Instantiate;
        }
        */


        private void Awake()
        {
            _shapeGenerator = new GenericShape();
            _shapeGenerator.Initialize(_shapeData);
            _shapeGenerator.Calculate(_shapeData);
            vertices = _shapeGenerator.GetVertices(_shapeData);
            
            _shapeCount = vertices.Length;
        }

        public void Execute()
        {
            shapeVisual.Draw(vertices);
            SpawnBubbles();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Cleanup()
        {
            throw new NotImplementedException();
        }

        private void SpawnBubbles()
        {
            foreach (var vertex in vertices)
            {
                Debug.Log(vertex);
                var obj = Instantiate(
                    _bubblePrefab,
                    vertex,
                    Quaternion.identity,
                    transform);
                _bubbles.Add(obj.GetComponent<BubbleHandler>());
            }
            foreach (var bubbleHandler in _bubbles)
            {
                bubbleHandler.OnBubbleClicked += BubbleClicked_Countdown; 
            } 
        }

        private void BubbleClicked_Countdown(object sender, EventArgs eventArgs)
        {
            _shapeCount--;
            if (_shapeCount != 0) return;
            TaskComplete?.Invoke(this, EventArgs.Empty);
        }
            private void OnDisable()
            {
                foreach (var bubbleHandler in _bubbles)
                {
                    bubbleHandler.OnBubbleClicked -= BubbleClicked_Countdown;
                }
            }
    }
}