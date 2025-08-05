using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    [CreateAssetMenu(fileName = "Hover", menuName = "Select Behaviour/Hover")]
    public class SelectOnOverlapStrategy : SelectStrategyBase
    {
        // still a little problem with this one, bug, projectile follows mouse.
        public override void Select(object sender, Projectile e)
        {
            e.IsSelected = true;
            e.SetStatic();
            e.transform.SetParent(FindFirstObjectByType<Pointer>().transform);
            MovementProvider.SelectEventRaised = false;
        }
    }
}