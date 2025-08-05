using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pigs
{
    // fix to be when m_BirdsDictionary == 0
    public class BirdsHandler
    {
        public GameObject Prefab;
        private Model.Pigs.Birds _bird;
        private Spawner _spawner;
        private Dictionary<int, Model.Pigs.Birds> _birdsDictionary;
        public event EventHandler OnListEmpty;

        public BirdsHandler(Spawner spawner)
        {
            _spawner = spawner;
        }

        public void Subscribe()
        {
            
        }

        public void Unsubscribe()
        {
            _spawner.SpawnedRef.GetComponent<Model.Pigs.Birds>().OnDeath -= OnDeath_Perform;
        }
        public void CreateBirds(int number, List<Transform> locations)
        {
            _birdsDictionary = new Dictionary<int, Model.Pigs.Birds>();
            
            for (int i = 0; i < number; i++)
            {
                _spawner.SpawnAt(Prefab, locations[i]);
                var bird = _spawner.SpawnedRef.GetComponent<Model.Pigs.Birds>();
                bird.Id = i;
                _birdsDictionary.TryAdd(bird.Id, bird);
                bird.OnDeath += OnDeath_Perform;
            }
        }
        public void CreateBirds(List<Transform> locations)
        {
            _birdsDictionary = new Dictionary<int, Model.Pigs.Birds>();
            
            for (var i = 0; i < locations.Count; i++)
            {
                _spawner.SpawnAt(Prefab, locations[i]);
                var bird = _spawner.SpawnedRef.GetComponent<Model.Pigs.Birds>();
                bird.Id = i;
                _birdsDictionary.TryAdd(bird.Id, bird);
                bird.OnDeath += OnDeath_Perform;
            }
        }

        ~BirdsHandler()
        {
            Unsubscribe();
        }
        private void OnDeath_Perform(object sender, EventArgs e)
        {
            if (_birdsDictionary.Count == 1)
            {
                OnListEmpty?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _birdsDictionary.Remove(_birdsDictionary.Keys.Count - 1);
            }
        }
    }
}