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
        private OverlapBasedTask _taskData; 
        public bool IsTaskStarted { get; set; }
        public event EventHandler TaskComplete;

        public OverlapTaskBehaviour(int radius)
        {
            OverlapAreaRadius = radius;
        }
        public void Initialize()
        {
            if (IsTaskStarted)
            {
                TaskComplete?.Invoke(this, EventArgs.Empty);
                IsTaskStarted = false;
            }
        }
        public void Cleanup()
        {
            IsTaskStarted = false;
        }
    }
}