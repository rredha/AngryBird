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
        [SerializeField] private SelectStrategyBase _selectStrategy;
        
        public event EventHandler<Projectile> OnProjectileOverlap;
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

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<Projectile>())
            {
                var projectile = other.GetComponent<Projectile>();
                OnProjectileOverlap?.Invoke(this, projectile);
            }
        }

        private void FixedUpdate()
        {
            transform.position = MouseMovement.PointerWorldPosition;
        }
    }
}