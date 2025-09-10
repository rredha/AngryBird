using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape
{
    public interface ITaskBehaviour
    {
        //public BaseTaskData TaskData { get; set; }
        public bool IsTaskStarted { get; set; }
        public event EventHandler TaskComplete;
        public void Initialize();
        public void Cleanup();
    }
}