using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Runtime.Angrybird.Model.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pigs
{
    public class BirdsHandler
    {
        public GameObject Prefab;
        private Model.Pigs.Birds _bird;
        private List<Model.Pigs.Birds> _birdList = new();
        public Model.Pigs.Birds Bird => _bird; 
        private Spawner _spawner;
        private Dictionary<int, Model.Pigs.Birds> _birdsDictionary;
        private Dictionary<int, bool> _birdStatusDict;

        public int NumberOfBirds { get; set; }
        public bool AllBirdsDestroyed => GetBirdsStatus();

        public BirdsHandler(Spawner spawner)
        {
            _spawner = spawner;
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
            //_birdsDictionary = new Dictionary<int, Model.Pigs.Birds>();
            _birdsDictionary = new Dictionary<int, Model.Pigs.Birds>();
            _birdStatusDict = new Dictionary<int, bool>();
            
            for (var i = 0; i < locations.Count; i++)
            {
                _spawner.SpawnAt(Prefab, locations[i]);
                var bird = _spawner.SpawnedRef.GetComponent<Model.Pigs.Birds>();
                bird.Id = i;
                _birdsDictionary.Add(bird.Id, bird);
                _birdStatusDict.Add(bird.Id, false);
                SubscribeBirdDeathEvent();
            }
        }

        private void SubscribeBirdDeathEvent()
        {
            foreach (var bird in _birdsDictionary)
            {
                bird.Value.OnDeath += OnDeath_Perform;
            }
        }
        private void UnsubscribeBirdDeathEvent()
        {
            foreach (var bird in _birdList)
            {
                bird.OnDeath -= OnDeath_Perform;
            }
        }
        ~BirdsHandler()
        {
            UnsubscribeBirdDeathEvent();
        }
        
        private void OnDeath_Perform(object sender, BirdDataEventArgs e)
        {
            if (e.IsDead)
            {
                _birdsDictionary[e.Id] = null;
                _birdStatusDict[e.Id] = e.IsDead;
            }

        }

        private bool GetBirdsStatus()
        {
            return !_birdStatusDict.Values.Contains(false);
        }
    }
}