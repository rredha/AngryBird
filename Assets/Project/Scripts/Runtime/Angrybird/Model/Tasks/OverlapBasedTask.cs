using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Strategy/Overlap Based Task", fileName = "Overlap", order = 0)]
    public class OverlapBasedTask : BaseTaskData
    {
        public int OverlapAreaRadius;

        private void OnEnable()
        {
            TaskBehaviour = new OverlapTaskBehaviour(OverlapAreaRadius);
        }
    }
}