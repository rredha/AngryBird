using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class ProjectileHandler
    {
        public Projectile Current => _current;
        public bool IsStackEmpty => _stackEmpty;
        private bool _stackEmpty => _stack.Count == 0;
        
        private Projectile _current;
        private Projectile _next;
        
        private Stack<Projectile> _stack = new();
        public int ProjectileLeft => _stack.Count;
        public event EventHandler OnEmpty;
        ~ProjectileHandler() => Unsubscribe();
        public void Subscribe()
        {
            Current.OnProjectileUsed += CurrentProjectileUsed_Perform;
            //_current.OnProjectileUsed += CurrentProjectileUsed_Perform;
        }
        public void Unsubscribe()
        {
            Current.OnProjectileUsed -= CurrentProjectileUsed_Perform;
            //_current.OnProjectileUsed -= CurrentProjectileUsed_Perform;
        }
        private void CurrentProjectileUsed_Perform(object sender, EventArgs e)
        {
            if (_stackEmpty && Current.IsThrown)
            {
                OnEmpty?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                GetNext();
            }
        }

        public void AddToStack(Projectile projectile)
        {
           _stack.Push(projectile); 
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
            if (!_stackEmpty && Current.IsThrown)
            {
                _next = _stack.Pop();
                _next.gameObject.SetActive(true);
                _current = _next;
            }
        }
    }
}