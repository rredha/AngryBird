using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class SelectAction : MonoBehaviour
    {
        [SerializeField] private TaskHandler taskHandler;
        [SerializeField] private Pointer pointer;
        private void Awake()
        {
            pointer.Setup();
            Subscribe();
        }

        public void Unsubscribe()
        {
            pointer.Behaviour.ProjectileOverlap -= taskHandler.Enable;
        }

        public void Subscribe()
        {
            pointer.Behaviour.ProjectileOverlap += taskHandler.Enable;
        }
        
    }
}