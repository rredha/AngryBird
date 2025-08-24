using System;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape
{
    public interface ITaskBehaviour
    {
        public void Execute();

        public event EventHandler TaskComplete;
    }
}