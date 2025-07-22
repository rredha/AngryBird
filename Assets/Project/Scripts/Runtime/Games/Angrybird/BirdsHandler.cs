using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird
{
    // these should get from a scriptableobject or level thingy
    // fix to be when m_BirdsDictionary == 0
    public class BirdsHandler
    {
        private Birds m_Bird;
        private Spawner m_Spawner;
        private Dictionary<int, Birds> m_BirdsDictionary;
        public event EventHandler OnListEmpty;
        public BirdsHandler(int number, List<Transform> locations, Spawner spawner)
        {
            m_Spawner = spawner;
            m_BirdsDictionary = new Dictionary<int, Birds>();
            
            for (int i = 0; i < number; i++)
            {
                m_Spawner.SpawnBirds(locations[i]);
                var bird = m_Spawner.SpawnedBird.GetComponent<Birds>();
                bird.Id = i;
                m_BirdsDictionary.TryAdd(bird.Id, bird);
                bird.OnDeath += OnDeath_Perform;
            }
        }

        ~BirdsHandler()
        {
            m_Spawner.SpawnedBird.GetComponent<Birds>().OnDeath -= OnDeath_Perform;
        }
        private void OnDeath_Perform(object sender, EventArgs e)
        {
            if (m_BirdsDictionary.Count == 1)
            {
                OnListEmpty?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                m_BirdsDictionary.Remove(m_BirdsDictionary.Keys.Count - 1);
            }
        }
    }
}