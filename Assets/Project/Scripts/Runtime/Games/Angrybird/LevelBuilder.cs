using System;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils.GameState
{
    public class GameConfiguration
    {
        public Transform Platform => _platform;
        private Transform _platform;

        public GameConfiguration(Transform platform)
        {
            _platform = platform;
        }
    }
    public class LevelBuilder : MonoBehaviour
    {
        public Level Data => _data;
        private Level _data;
        public GameConfiguration Config { get; private set; }
        [SerializeField] private LevelSO config;
        [SerializeField] private GameConfigurationSO gameConfig;

        private void Awake()
        {
            _data = new Level(
                config.Birds,
                config.Projectiles,
                config.BirdsLocations,
                config.Stages);
            
            Config = new GameConfiguration(gameConfig.Platform);
        }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            foreach (var t in Data.Stages)
            {
                Instantiate(t);
            }

            /*
             * As the bird location are directly tied to levels
             * it should be instantiated here
             
            foreach (var birdsLocation in Data.BirdsLocations)
            {
                Instantiate(birdsLocation);
            }
            */
        }

        public void DebugMessage(Level data)
        {
            Debug.Log("Level One : ");
            Debug.Log($"{data.Projectiles} Projectiles");
            Debug.Log($"{data.Birds} Projectiles");
        }
    }
}