using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public enum LevelStatusEnum
    {
       Completed,
       UnCompleted
    };
    public class LevelManager : MonoBehaviour
    {
        public int CurrentLevel;
        [SerializeField] private LevelSO levelSo;
        [SerializeField] private GameConfigurationSO configurationSo;
        [SerializeField] private Spawner spawner;
        
        public Projectile Projectile { get; set; }
        public ProjectileHandler ProjectileHandler { get; private set; }
        public BirdsHandler BirdsHandler { get; private set; }
        public LevelBuilder Builder { get; private set; }
        public int Attempt => ProjectileHandler.ProjectileLeft;
        private int _numberOfBirds;
        public LevelStatusEnum LevelStatus => BirdsHandler.AllBirdsDestroyed ? LevelStatusEnum.Completed : LevelStatusEnum.UnCompleted;
        public bool OutOfAttempts { get; private set; }

        public void Setup()
        {
            if (spawner == null)
                Debug.LogError("Spawner not yet initialized.");
            
            ProjectileHandler = new ProjectileHandler(spawner)
            {
                Prefab = levelSo.ProjectilePrefab
            };
            BirdsHandler = new BirdsHandler(spawner)
            {
                Prefab = levelSo.BirdPrefab,
                NumberOfBirds = levelSo.Birds
            };
            Builder = new LevelBuilder(ProjectileHandler, BirdsHandler, levelSo, configurationSo, spawner);
            Builder.Init();
            IsInitialized = true;
        }

        public bool IsInitialized;
        private void Start()
        // need something that is more robust... dependency injection.
        {
            //CurrentLevel = Builder.LevelIndex;
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

        public void Clean()
        {
            var projectiles = FindObjectsByType<Projectile>(FindObjectsSortMode.None);
            var birds = FindObjectsByType<Model.Pigs.Birds>(FindObjectsSortMode.None);
            var obstacles = FindObjectsByType<Obstacles>(FindObjectsSortMode.None);
            foreach (var projectile in projectiles)
            {
                Destroy(projectile.gameObject);
            }
            foreach (var bird in birds)
            {
                Destroy(bird.gameObject);
            }
            foreach (var obstacle in obstacles)
            {
                Destroy(obstacle.gameObject);
            }

        }

        public void Reset()
        {
            OutOfAttempts = false;
            IsInitialized = false;
        }
    }
}