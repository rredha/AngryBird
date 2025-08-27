using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private GameConfigurationSO gameConfig;
        [SerializeField] private LevelSO levelData;
        
        private LevelManager _levelManager;
        public ProjectileHandler ProjectileHandler { get; private set; }
        public BirdsHandler BirdsHandler { get; private set; }

        private Spawner _spawner;
        private void Awake()
        {
            _spawner = GetComponent<Spawner>();
            _levelManager = GetComponent<LevelManager>();
            ProjectileHandler = new ProjectileHandler(_spawner)
            {
                Prefab = levelData.ProjectilePrefab
            };
            BirdsHandler = new BirdsHandler(_spawner)
            {
                Prefab = levelData.BirdPrefab
            };
        }

        public void Init()
        {
            SetupEnvironment();
            SetupTargets();
            SetupProjectiles();
        }

        public void PopFirstProjectile()
        {
            ProjectileHandler.PopFirstProjectile(); // needs to rethink
            _levelManager.Projectile = ProjectileHandler.Current;
 
        }
        public void Clean()
        {
            // to improve
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
        private void SetupProjectiles()
        {
          ProjectileHandler.CacheProjectiles(levelData.Projectiles, gameConfig.Platform);
        }
        private void SetupTargets()
        {
          BirdsHandler.CreateBirds(levelData.BirdsLocations);
        }
        private void SetupEnvironment()
        {
            foreach (var t in levelData.Stages)
            {
                Instantiate(t);
            }
        }
        public void DebugMessage(Model.Level.Level data)
        {
            Debug.Log("Level One : ");
            Debug.Log($"{data.Projectiles} Projectiles");
            Debug.Log($"{data.Birds} Projectiles");
        }
    }
}