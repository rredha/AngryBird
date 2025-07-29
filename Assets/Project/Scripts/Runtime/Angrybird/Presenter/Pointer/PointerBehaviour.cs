using System;
using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public class PointerBehaviour : MonoBehaviour
    {
        private MousePointer _mouseMovement;
        private Vector3 _pointerWorldPosition;
        private Vector2 _pointerScreenPosition;
        private Collider2D _collider;
        
        public event EventHandler<Projectile> OnProjectileOverlap;
        private void Awake()
        {
            _mouseMovement = new MousePointer(Camera.main);
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }
    
        private void Start()
        {
            _mouseMovement.Initialize();
            _mouseMovement.Subscribe();
        }

        private void OnDisable()
        {
            _mouseMovement.Unsubscribe();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<Projectile>() && _mouseMovement.SelectEventRaised)
            {
                var projectile = other.GetComponent<Projectile>();
                OnProjectileOverlap?.Invoke(this, projectile);
                _mouseMovement.SelectEventRaised = false;
            }
        }

        private void FixedUpdate()
        {
            transform.position = _mouseMovement.PointerWorldPosition;
        }
    }
}