using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    [CreateAssetMenu(fileName = "Task", menuName = "Select Behaviour/Task")]
    public class SelectOnTaskCompleteStrategy : SelectStrategyBase
    {

        public GameObject TaskPrefab; // add prefab from other project.
        private bool _taskComplete;
        public override void Select(object sender, Projectile e)
        {
            if (!_taskComplete) return;
            
            e.IsSelected = true;
            e.SetStatic();
            e.transform.SetParent(FindFirstObjectByType<Pointer>().transform);
            MovementProvider.SelectEventRaised = false;
        }
    }
}