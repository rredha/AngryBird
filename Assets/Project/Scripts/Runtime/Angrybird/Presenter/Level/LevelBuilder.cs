using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public class LevelBuilder
    {
        private readonly GameConfigurationSO _gameConfig;
        private readonly LevelSO _levelData;
        private Spawner _spawner;
        public int LevelIndex => _levelData.LevelIndex;
        private readonly ProjectileHandler _projectileHandler;
        private readonly BirdsHandler _birdsHandler;

        public LevelBuilder(
            ProjectileHandler projectileHandler, BirdsHandler birdsHandler,
            LevelSO levelData, GameConfigurationSO gameConfigurationSo,
            Spawner spawner)
        {
            _projectileHandler = projectileHandler;
            _birdsHandler = birdsHandler;
            _levelData = levelData;
            _gameConfig = gameConfigurationSo;
            _spawner = spawner;
        }

        public void Init()
        {
            SetupEnvironment();
            SetupTargets();
            SetupProjectiles();
        }
        private void SetupProjectiles()
        { 
            _projectileHandler.CacheProjectiles(_levelData.Projectiles, _gameConfig.Platform);
        }
        private void SetupTargets()
        {
          _birdsHandler.CreateBirds(_levelData.BirdsLocations);
        }
        private void SetupEnvironment()
        {
            foreach (var t in _levelData.Stages)
            {
                _spawner.Spawn(t.gameObject);
            }
        }
    }
}