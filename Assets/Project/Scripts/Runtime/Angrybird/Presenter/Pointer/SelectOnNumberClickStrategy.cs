using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    [CreateAssetMenu(fileName = "Multiple Clicks", menuName = "Select Behaviour/Multiple Clicks")]
    public class SelectOnNumberClickStrategy : SelectStrategyBase
    {
       public int number;
        
       private int _clickCount;

       public override void Select(object sender, Projectile e)
        {
            if (MovementProvider.SelectEventRaised)
            {
                _clickCount++;
            }
            if (_clickCount == number)
            {
                e.IsSelected = true;
                e.SetStatic();
                e.transform.SetParent(FindFirstObjectByType<Pointer>().transform);
                _clickCount = 0;
            }
            MovementProvider.SelectEventRaised = false;
        }
    }
}