using System;
using Project.Scripts.Runtime.Angrybird.Managers;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public enum LevelStatusEnum
    {
       Completed,
       UnCompleted
    };
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public int CurrentLevel;
        public Projectile Projectile { get; set; }
        public ProjectileHandler ProjectileHandler { get; private set; }
        public BirdsHandler BirdsHandler { get; private set; }
        private LevelBuilder _levelBuilder;
        private int _numberOfBirds;
        private LevelStatusEnum _levelStatus;
        public LevelStatusEnum LevelStatus => BirdsHandler.AllBirdsDestroyed ? LevelStatusEnum.Completed : LevelStatusEnum.UnCompleted;

        public bool OutOfAttempts { get; private set; }



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
            CurrentLevel = _levelBuilder.LevelIndex;
            ProjectileHandler.OnEmpty += OnProjectileStackEmpty_Perform;
        }

        private void OnDisable()
        {
            ProjectileHandler.OnEmpty -= OnProjectileStackEmpty_Perform;
        }
        private void OnProjectileStackEmpty_Perform(object sender, EventArgs e)
        {
            OutOfAttempts = true;
        }
        public void Proceed()
        {
            ProjectileHandler.GetProjectile();
            Projectile = ProjectileHandler.Current;
        }
    }
}