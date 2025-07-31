using System;
using System.Runtime.CompilerServices;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public class LevelBuilder : MonoBehaviour
    {
        public Model.Level.Level Data => _data;
        private Model.Level.Level _data;
        public GameConfiguration Config { get; private set; }
        [SerializeField] private GameConfigurationSO gameConfig;
        [SerializeField] private LevelSO config;

        public ProjectileHandler ProjectileHandler { get; private set; }
        public BirdsHandler BirdsHandler { get; private set; }
        public Projectile Projectile { get; private set; }

        public bool OutOfAttempts { get; private set; }
        public bool AllBirdsDestroyed { get; private set; }

        private Spawner _spawner;
        
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject birdPrefab;

        private void Awake()
        {
            _data = new Model.Level.Level(
                config.Birds,
                config.Projectiles,
                config.BirdsLocations,
                config.Stages);
            
            Config = new GameConfiguration(gameConfig.Platform);
            
            _spawner = GetComponent<Spawner>();
            ProjectileHandler = new ProjectileHandler(_spawner)
            {
                Prefab = projectilePrefab
            };
            BirdsHandler = new BirdsHandler(_spawner)
            {
                Prefab = birdPrefab
            };
            
            ProjectileHandler.OnEmpty += OnProjectileStackEmpty_Perform;
            BirdsHandler.OnListEmpty += OnBirdListEmpty_Perform;
        }
        private void OnBirdListEmpty_Perform(object sender, EventArgs e)
        {
            AllBirdsDestroyed = true;
        }

        private void OnDisable()
        {
            ProjectileHandler.OnEmpty -= OnProjectileStackEmpty_Perform;
            BirdsHandler.OnListEmpty -= OnBirdListEmpty_Perform;
        }

        public void Initialize()
        {
            SetupEnvironment();
            SetupTargets();
            SetupProjectiles();
            
            ProjectileHandler.PopFirstProjectile(); // needs to rethink
            Projectile = ProjectileHandler.Current;
        }

        public void Clean()
        {
            
        }
        private void SetupProjectiles()
        {
          ProjectileHandler.CacheProjectiles(Data.Projectiles, Config.Platform);
        }

        private void SetupTargets()
        {
          BirdsHandler.CreateBirds(Data.Birds, Data.BirdsLocations);
        }

        private void SetupEnvironment()
        {
            foreach (var t in Data.Stages)
            {
                Instantiate(t);
            }
        }
        public void Proceed()
        {
            ProjectileHandler.GetProjectile();
            Projectile = ProjectileHandler.Current;
        }
        private void OnProjectileStackEmpty_Perform(object sender, EventArgs e)
        {
            OutOfAttempts = true;
        }

        public void DebugMessage(Model.Level.Level data)
        {
            Debug.Log("Level One : ");
            Debug.Log($"{data.Projectiles} Projectiles");
            Debug.Log($"{data.Birds} Projectiles");
        }
    }
}