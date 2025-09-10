using System;
using System.Collections.Generic;
using Arcade;
using Model.Shape;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Bubble;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;
using View.Shapes;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape
{
    public class BubblePopTaskBehaviour : ITaskBehaviour
    {
        private BubblePopBehaviour _bubblePopBehaviour;

        public BubblePopTaskBehaviour(BubblePopBehaviour bubblePopBehaviour)
        {
            _bubblePopBehaviour = bubblePopBehaviour;
        }
        private bool _isActive;

        public BaseTaskData TaskData { get; set; }
        public bool IsTaskStarted { get; set; }
        public event EventHandler TaskComplete;
        public void Initialize()
        {
            _isActive = true;
            if (_isActive)
            {
                //_bubblePopBehaviour.AllBubblesPopped += OnTaskComplete;
                //TaskComplete += OnTaskComplete_Notify;
            }
        }
        public void Cleanup()
        {
            _isActive = false;
            //_bubblePopBehaviour.AllBubblesPopped -= OnTaskComplete;
        }
    }
}