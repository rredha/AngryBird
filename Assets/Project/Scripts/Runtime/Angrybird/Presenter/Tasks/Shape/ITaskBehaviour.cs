using System;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape
{
    public interface ITaskBehaviour
    {
        public event EventHandler TaskComplete;
        public void Initialize();
        public void Cleanup();
    }
}