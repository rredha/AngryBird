using UnityEngine;
namespace Arcade.Project.Runtime.Games.AngryBird.Utils

{
    public class DropZone : MonoBehaviour
    {
        public Collider2D Col { get; set; }
        public bool IsOverlapping { get; private set; }
        public Projectile Projectile { get; set; }

        private void Awake()
        {
            IsOverlapping = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent(out Projectile proj);
            Projectile = proj;
            
            if (Projectile != null && !Projectile.IsFlying)
            {
                // dispatch the event
                IsOverlapping = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsOverlapping = false;
        }
    }
}