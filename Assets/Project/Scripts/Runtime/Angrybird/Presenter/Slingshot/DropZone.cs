using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Slingshot

{
    public class DropZone : MonoBehaviour
    {
        public bool IsOverlapping { get;  set; }
        public Projectile _projectile { get; private set; }

        private void Awake()
        {
            IsOverlapping = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent(out Projectile proj);
            _projectile = proj;
            
            if (_projectile != null && !_projectile.IsThrown)
            {
                IsOverlapping = true;
            }
            else
            {
                IsOverlapping = false;
            }
        }
    }
}