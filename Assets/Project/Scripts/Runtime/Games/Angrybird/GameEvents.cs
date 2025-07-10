using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Games
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents current;

        private void Awake()
        {
            current = this;
        }

        public Action OnPointerOverlapHolder;
        public Action OnTouchDestroyableItem;
        public Action OnProjectileFlying;

        public void PointerOverlapHolder() => OnPointerOverlapHolder?.Invoke();
        public void ProjectileFlying() => OnProjectileFlying?.Invoke();
        public void DestroyableItemTrigger() => OnTouchDestroyableItem?.Invoke();
    }
}