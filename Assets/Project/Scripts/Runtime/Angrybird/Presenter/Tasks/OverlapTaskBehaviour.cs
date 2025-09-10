using System;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class OverlapTaskBehaviour : ITaskBehaviour
    {
        public int OverlapAreaRadius { get; set; }
        public BaseTaskData TaskData { get; set; }
        public bool IsTaskStarted { get; set; }
        public event EventHandler TaskComplete;
        private bool _isActive; // prevent behaviour from executing when the bird is dropping

        public OverlapTaskBehaviour(int radius)
        {
            OverlapAreaRadius = radius;
        }
        public void Initialize()
        {
            _isActive = true;
            if (_isActive)
                TaskComplete?.Invoke(this, EventArgs.Empty);
        }
        public void Cleanup()
        {
            _isActive = false;
        }
    }
}