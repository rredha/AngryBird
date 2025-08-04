using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public abstract class SelectStrategyBase : ScriptableObject
    {
        // need to refactor this, to make it work and instantiable.
        // create a base class for it as well.
        public MousePointer MovementProvider;

        public abstract void Select(object sender, Projectile e);
    }
}