using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird
{
    public class ProjectileHandler
    {
        public Projectile Current => m_Current;
        private bool m_StackEmpty => m_Stack.Count == 0;
        
        private Projectile m_Current;
        private Projectile m_Next;
        
        private Spawner m_Spawner;
        private Stack<Projectile> m_Stack;

        public event EventHandler OnEmpty;

        public ProjectileHandler(Spawner spawner)
        {
            m_Spawner = spawner;
            m_Stack = new Stack<Projectile>();
            /*
            CacheProjectiles(number);
            PopFirstProjectile();
            m_Current.OnProjectileUsed += CurrentProjectileUsed_Perform;
            OnEmpty += OnEmpty_Notify;
            */
        }
        ~ProjectileHandler()
        {
            m_Current.OnProjectileUsed -= CurrentProjectileUsed_Perform;
            OnEmpty -= OnEmpty_Notify;
        }

        private void OnEmpty_Notify(object sender, EventArgs e)
        {
            Debug.Log("It is empty my dear");
        }

        private void CurrentProjectileUsed_Perform(object sender, EventArgs e)
        {
            if (m_StackEmpty && Current.IsUsed)
            {
                OnEmpty?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                GetNext();
            }
            
        }

        public void CacheProjectiles(int number)
        {
            for (int i = 0; i < number; i++)
            {
                m_Spawner.SpawnProjectile();
        
                var projectile = m_Spawner.SpawnedProjectile.GetComponent<Projectile>();
                projectile.gameObject.SetActive(false);
        
                m_Stack.Push(projectile);
            }
        }
        public void PopFirstProjectile()
        {
            m_Current = m_Stack.Pop();
            m_Current.gameObject.SetActive(true);
        }

        public void GetProjectile()
        {
            if (!m_StackEmpty)
            {
                m_Next = m_Stack.Pop();
                m_Next.gameObject.SetActive(true);
                m_Current = m_Next;
            }
        }

        private void GetNext()
        {
            if (!m_StackEmpty && Current.IsUsed)
            {
                m_Next = m_Stack.Pop();
                m_Next.gameObject.SetActive(true);
                m_Current = m_Next;
            }
        }
    }
}