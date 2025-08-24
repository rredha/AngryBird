using System;
using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public abstract class BaseTask : ScriptableObject
    {
        protected Pointer Pointer;
        public bool CreateInstance;
        public GameObject BehaviourPrefab;

        public abstract void Enable(object sender, Projectile projectile);

        protected abstract void Create();
        protected abstract void SetActive();
        public void SetPointer(Pointer pointer)
        {
            Pointer = pointer;
        }


        protected void Outcome(Projectile projectile, Pointer pointer)
        {
            projectile.SetStatic();
            projectile.transform.SetParent(pointer.transform);
            projectile.transform.localPosition = Vector3.zero;
        }

        public abstract void Notify(object sender, Projectile e);
    }
}