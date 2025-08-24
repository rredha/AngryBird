using System;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class OverlapTaskBehaviour : MonoBehaviour, ITaskBehaviour
    {
        public event EventHandler TaskComplete;
        public void Execute()
        {
            TaskComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}