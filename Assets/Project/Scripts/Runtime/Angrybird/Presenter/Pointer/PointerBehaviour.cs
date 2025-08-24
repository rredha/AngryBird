using System;
using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public class PointerBehaviour : MonoBehaviour
    {
        public MousePointer MouseMovement { get; private set; }
        private Vector3 _pointerWorldPosition;
        private Vector2 _pointerScreenPosition;
        private Collider2D _collider;
        private int _overlapTriggerCount;
        public bool OverlapTriggered { get; set; }
        
        
        public event EventHandler<Projectile> ProjectileOverlap;
        private void Awake()
        {
            MouseMovement = new MousePointer(Camera.main);
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }
    
        private void Start()
        {
            MouseMovement.Initialize();
            MouseMovement.Subscribe();
        }

        private void OnDisable()
        {
            MouseMovement.Unsubscribe();
        }


        public void ResetOverlapTriggerCount()
        {
            _overlapTriggerCount = 0;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ResetOverlapTriggerCount();
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            var projectile = other.GetComponent<Projectile>();
            if (projectile != null && projectile.IsIdle && !projectile.IsSelected && !OverlapTriggered)
            {
                ProjectileOverlap?.Invoke(this, projectile);
                OverlapTriggered = true;
            }
        }

        private void FixedUpdate()
        {
            transform.position = MouseMovement.PointerWorldPosition;
        }
    }
}