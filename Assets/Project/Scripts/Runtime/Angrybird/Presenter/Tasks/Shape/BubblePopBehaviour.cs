using System;
using System.Collections.Generic;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Bubble;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arcade
{
    public class BubblePopBehaviour : MonoBehaviour, ITaskBehaviour
    {
        [SerializeField] private GameObject bubblePrefab;
        [SerializeField] private BubblePopBasedTask taskData; // fix later, resolving dependency by external script
        private ShapeHandler _shapeHandler;
        private List<BubbleHandler> _bubbleHandlers;
        private int _bubblesLeft;
        private bool _isActive;

        public bool IsTaskStarted { get; set; }
        public event EventHandler TaskComplete;

        public void Initialize()
        {
            _shapeHandler = GetComponent<ShapeHandler>();
            if (IsTaskStarted)
            {
                _shapeHandler.Setup(taskData.Shape);
                Begin();
                IsTaskStarted = false;
            }
        }
        public void Cleanup()
        {
            IsTaskStarted = false;
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            TaskComplete -= OnTaskComplete_Unsubscribe;
            TaskComplete -= OnTaskComplete_Destroy;
        }


        private void Awake()
        {
            _bubbleHandlers = new List<BubbleHandler>();
            _shapeHandler = GetComponent<ShapeHandler>();
        }

        private void Begin()
        {
            _bubblesLeft = _shapeHandler.vertices.Length;

            var projectileOrigin = new Vector3(-6.74991322f, -3.43700671f, 0f); // hard coded fix later
            transform.position = projectileOrigin; 
            foreach (var vertex in _shapeHandler.vertices)
            {
                //var position = vertex - projectileOrigin - transform.localPosition; 
                var bubble = Instantiate(bubblePrefab, vertex, Quaternion.identity, transform);
                _bubbleHandlers.Add(bubble.GetComponent<BubbleHandler>());
            }
            SubscribeBubbleHandler();
        }

        private void Subscribe()
        {
            TaskComplete += OnTaskComplete_Unsubscribe;
            TaskComplete += OnTaskComplete_Destroy;
        }
        private void SubscribeBubbleHandler()
        {
            foreach (var bubbleHandler in _bubbleHandlers)
            {
                bubbleHandler.OnBubbleClicked += BubbleClicked_Countdown; 
            }
        }
        private void UnsubscribeBubbleHandler()
        {
            foreach (var bubbleHandler in _bubbleHandlers)
            {
                bubbleHandler.OnBubbleClicked -= BubbleClicked_Countdown; 
            }
        }

        private void BubbleClicked_Countdown(object sender, EventArgs e)
        {
            _bubblesLeft--;
            if (_bubblesLeft != 0) return;
            TaskComplete?.Invoke(this, EventArgs.Empty);
        }
        
        #region Events
        private void OnTaskComplete_Unsubscribe(object sender, EventArgs e)
        {
            UnsubscribeBubbleHandler();
        }
        private void OnTaskComplete_Destroy(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
        private void OnTaskComplete_Notify(object sender, EventArgs e)
        {
            Debug.Log("Task Completed");
        }
        #endregion
    }
}
