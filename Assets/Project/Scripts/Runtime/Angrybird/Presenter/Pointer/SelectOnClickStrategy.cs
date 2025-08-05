using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    [CreateAssetMenu(fileName = "Single Clicks", menuName = "Select Behaviour/Single Clicks")]
    public class SelectOnClickStrategy : SelectStrategyBase
    {

        public override void Select(object sender, Projectile e)
        {
            if (MovementProvider.SelectEventRaised)
            {
                e.IsSelected = true;
                e.SetStatic();
                e.transform.SetParent(FindFirstObjectByType<Pointer>().transform);
            }
            MovementProvider.SelectEventRaised = false;
        }
    }
}