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
        private Model.Pigs.Birds _bird;
        private List<Model.Pigs.Birds> _birdList = new();
        public Model.Pigs.Birds Bird => _bird; 
        public Dictionary<int, Model.Pigs.Birds> BirdsDictionary { get; set; }
        public Dictionary<int, bool> BirdStatusDict { get; set; }

        public int NumberOfBirds { get; set; }
        public bool AllBirdsDestroyed => GetBirdsStatus();

        public void SubscribeBirdDeathEvent()
        {
            foreach (var bird in BirdsDictionary)
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
                BirdsDictionary[e.Id] = null;
                BirdStatusDict[e.Id] = e.IsDead;
            }
        }

        private bool GetBirdsStatus()
        {
            return !BirdStatusDict.Values.Contains(false);
        }
    }
}