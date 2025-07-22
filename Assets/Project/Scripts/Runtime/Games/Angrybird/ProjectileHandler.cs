using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird
{
    public class ProjectileHandler
    {
        public Projectile Current => m_Current = GetCurrent();
        private Projectile m_Current;
        private Spawner m_Spawner;
        
        private Stack<Projectile> m_Stack;

        public event EventHandler OnEmpty;

        public ProjectileHandler(int number, Spawner spawner)
        {
            m_Spawner = spawner;
            m_Stack = new Stack<Projectile>();
            for (int i = 0; i < number; i++)
            {
                m_Spawner.SpawnProjectile();
        
                var projectile = m_Spawner.SpawnedProjectile.GetComponent<Projectile>();
                projectile.gameObject.SetActive(false);
        
                m_Stack.Push(projectile);
            }
        }

        public void Create(Transform location) => 
            m_Spawner.SpawnNewProjectile(location);
        public Projectile GetCurrent()
        {
            if (m_Stack.Count == 1)
                OnEmpty?.Invoke(this, EventArgs.Empty);
                
            m_Current = m_Stack.Pop();
            m_Current.gameObject.SetActive(true);
      
            return m_Current;
        }
        
    }
}