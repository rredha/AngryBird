using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Strategy/Click Based Task", fileName = "N Clicks", order = 0)]
    public class ClickBasedTask : BaseTaskData
    {
        public int Threshold;

        private void OnEnable()
        {
            TaskBehaviour = new ClickTaskBehaviour(Threshold);
        }
    }
}