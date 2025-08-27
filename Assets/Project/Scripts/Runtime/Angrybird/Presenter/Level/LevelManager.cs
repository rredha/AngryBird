using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager Instance;
        public Projectile Projectile { get; set; }
        public ProjectileHandler ProjectileHandler { get; private set; }
        public BirdsHandler BirdsHandler { get; private set; }
        private LevelBuilder _levelBuilder;
        
        private void Start()
        // need something that is more robust... dependency injection.
        {
            if (Instance == null && Instance != this)
            {
                Instance = this;
            }
            _levelBuilder = GetComponent<LevelBuilder>();
            ProjectileHandler = _levelBuilder.ProjectileHandler;
            BirdsHandler = _levelBuilder.BirdsHandler;
            
            ProjectileHandler.OnEmpty += OnProjectileStackEmpty_Perform;
            BirdsHandler.OnListEmpty += OnBirdListEmpty_Perform;
        }

        private void OnDisable()
        {
            ProjectileHandler.OnEmpty -= OnProjectileStackEmpty_Perform;
            BirdsHandler.OnListEmpty -= OnBirdListEmpty_Perform;
        }
        private void OnBirdListEmpty_Perform(object sender, EventArgs e)
        {
            AllBirdsDestroyed = true;
        }
        
        private void OnProjectileStackEmpty_Perform(object sender, EventArgs e)
        {
            OutOfAttempts = true;
        }

        public bool OutOfAttempts { get; private set; }
        public bool AllBirdsDestroyed { get; private set; }
        
        public void Proceed()
        {
            ProjectileHandler.GetProjectile();
            Projectile = ProjectileHandler.Current;
        }
        
    }
}