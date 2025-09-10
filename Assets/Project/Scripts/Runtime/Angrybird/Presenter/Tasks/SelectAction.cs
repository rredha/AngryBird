using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class SelectAction : MonoBehaviour
    {
        [SerializeField] private TaskHandler taskHandler;
        [SerializeField] private Pointer pointer;
        private PointerBehaviour _pointerBehaviour;
        private void Awake()
        {
            _pointerBehaviour = pointer.GetComponent<PointerBehaviour>();
            _pointerBehaviour.ProjectileOverlap += taskHandler.Enable;
        }
        private void OnDisable()
        {
            _pointerBehaviour.ProjectileOverlap -= taskHandler.Enable;
        }
    }
}