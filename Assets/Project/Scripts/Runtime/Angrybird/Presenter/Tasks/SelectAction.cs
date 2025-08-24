using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class SelectAction : MonoBehaviour
    {
        [SerializeField] private BaseTask task;
        [SerializeField] private Pointer pointer;
        private PointerBehaviour _behaviour;
        private void Awake()
        {
            _behaviour = pointer.GetComponent<PointerBehaviour>();
            if (task.CreateInstance)
            {
                Instantiate(task.BehaviourPrefab);
            }
            task.SetPointer(pointer);
            _behaviour.ProjectileOverlap += task.Enable;
        }
        private void OnDisable()
        {
            _behaviour.ProjectileOverlap -= task.Enable;
        }
    }
}