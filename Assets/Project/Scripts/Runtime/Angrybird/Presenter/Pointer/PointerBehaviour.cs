using System;
using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public class PointerBehaviour : MonoBehaviour
    {
        private MousePointer mouseMovement { get; set; }
        private Vector3 _pointerWorldPosition;
        private Vector2 _pointerScreenPosition;
        private Collider2D _collider;
        public event EventHandler<Projectile> ProjectileOverlap;
        private void Awake()
        {
            mouseMovement = new MousePointer();
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        private void Start()
        {
            mouseMovement.Initialize();
            mouseMovement.Subscribe();
        }

        private void OnDisable()
        {
            mouseMovement.Unsubscribe();
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            var projectile = other.GetComponent<Projectile>();
            if (projectile != null && projectile.IsIdle && !projectile.IsSelected)
            {
                ProjectileOverlap?.Invoke(this, projectile);
            }
        }
        private void FixedUpdate()
        {
            transform.position = mouseMovement.PointerWorldPosition;
        }
    }
}