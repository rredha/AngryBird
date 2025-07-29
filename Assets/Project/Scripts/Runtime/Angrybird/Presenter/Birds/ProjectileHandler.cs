using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class ProjectileHandler
    {
        public GameObject Prefab;
        public Projectile Current => _current;
        public bool IsStackEmpty => _stackEmpty;
        private bool _stackEmpty => _stack.Count == 0;
        
        private Projectile _current;
        private Projectile _next;
        
        private Spawner _spawner;
        private Stack<Projectile> _stack;

        public event EventHandler OnEmpty;

        public ProjectileHandler(Spawner spawner)
        {
            _spawner = spawner;
            _stack = new Stack<Projectile>();
        }

        private void DebugMessage()
        {
            Debug.Log("Initialized successfully !");
        }
        ~ProjectileHandler() => Unsubscribe();

        public void Subscribe()
        {
            _current.OnProjectileUsed -= CurrentProjectileUsed_Perform;
        }
        public void Unsubscribe()
        {
            _current.OnProjectileUsed -= CurrentProjectileUsed_Perform;
        }

        private void OnEmpty_Notify(object sender, EventArgs e)
        {
            Debug.Log("It is empty my dear");
        }

        private void CurrentProjectileUsed_Perform(object sender, EventArgs e)
        {
            if (_stackEmpty && Current.IsUsed)
            {
                OnEmpty?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                GetNext();
            }
            
        }

        public void CacheProjectiles(int number, Transform platform)
        {
            for (int i = 0; i < number; i++)
            {
                _spawner.SpawnAt(Prefab, platform);

                var projectile = _spawner.SpawnedRef.GetComponent<Projectile>();
                projectile.gameObject.SetActive(false);
        
                _stack.Push(projectile);
            }
        }
        public void PopFirstProjectile()
        {
            _current = _stack.Pop();
            _current.gameObject.SetActive(true);
        }

        public void GetProjectile()
        {
            if (!_stackEmpty)
            {
                _next = _stack.Pop();
                _next.gameObject.SetActive(true);
                _current = _next;
            }
            else
            {
                OnEmpty?.Invoke(this, EventArgs.Empty);
            }
        }

        private void GetNext()
        {
            if (!_stackEmpty && Current.IsUsed)
            {
                _next = _stack.Pop();
                _next.gameObject.SetActive(true);
                _current = _next;
            }
        }
    }
}