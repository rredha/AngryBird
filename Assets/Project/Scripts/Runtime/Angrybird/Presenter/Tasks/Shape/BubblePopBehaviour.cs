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
        [SerializeField]private GameObject _bubblePrefab;
        [SerializeField]private ShapeSO _shapeData;
        private IShapeGenerator _shapeGenerator;
        public Vector3[] vertices { get; private set; }
        private readonly List<BubbleHandler> _bubbles = new();
        private int _shapeCount;

        public event EventHandler TaskComplete;
        public event EventHandler TaskStart;

        private void OnEnable()
        {
            TaskStart += OnTaskStart_Draw;
            TaskStart += OnTaskStart_SayHello;
            TaskStart += OnTaskStart_Instantiate;
        }

        private void OnTaskStart_SayHello(object sender, EventArgs e)
        {
            Debug.Log("Hello");
        }

        private void Awake()
        {
            _shapeGenerator = new GenericShape();
            _shapeGenerator.Initialize(_shapeData);
            _shapeGenerator.Calculate(_shapeData);
            vertices = _shapeGenerator.GetVertices(_shapeData);
            
            _shapeCount = vertices.Length;
        }

        private void OnTaskStart_Draw(object sender, EventArgs e)
        {
            shapeVisual.Draw(vertices);
        }
        private void OnTaskStart_Instantiate(object sender, EventArgs e)
        {
            SpawnBubbles();
        }
        public void Execute()
        {
            TaskStart?.Invoke(this, EventArgs.Empty);
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
                TaskStart -= OnTaskStart_Draw;
                TaskStart -= OnTaskStart_Instantiate;
                foreach (var bubbleHandler in _bubbles)
                {
                    bubbleHandler.OnBubbleClicked -= BubbleClicked_Countdown;
                }
            }
    }
}