using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Model.Level;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public class LevelBuilder
    {
        private readonly Spawner _spawner;
        
        private ProjectileHandler _projectileHandler;
        private BirdsHandler _birdsHandler;
        
        private GameObject _projectilePrefab;
        private GameObject _birdPrefab;
        public LevelData LevelData { get; set; }

        public event EventHandler<GameHandlersEventArgs> LevelSetup;

        public LevelBuilder(
            GameObject projectilePrefab, GameObject birdPrefab,
            LevelData levelData,
            Spawner spawner)
        {
            _projectilePrefab = projectilePrefab;
            _birdPrefab = birdPrefab;
            
            this.LevelData = levelData;
            
            _spawner = spawner;

        }
        public LevelData LoadLevelFromScriptableObject(int index, LevelSO levelSo)
        {
            var levelData = new LevelData(index,
                levelSo.Birds, levelSo.Projectiles,
                levelSo.ProjectileLocation, levelSo.BirdsLocations,
                levelSo.Stages
            );
            return levelData;
        }
        public void Init()
        {
            _spawner.OnObjectSpawned += OnObjectSpawned_CreateGameHandlers;
            SetupProjectiles(LevelData);
            SetupEnvironment(LevelData);
            SetupTargets(LevelData);
            
            OnLevelSetup(new GameHandlersEventArgs(_projectileHandler, _birdsHandler));
        }

        private void OnObjectSpawned_CreateGameHandlers(GameObject obj)
        {
            if (_projectileHandler == null)
                _projectileHandler = new ProjectileHandler();
            if (_birdsHandler == null)
                _birdsHandler = new BirdsHandler();
        }

        private void SetupProjectiles(LevelData levelData)
        {
            for (int i = 0; i < levelData.Projectiles; i++)
            {
                if (_spawner == null)
                    Debug.LogError("No spwaner found.");
                _spawner.SpawnAt(_projectilePrefab, levelData.ProjectileLocation);

                var projectile = _spawner.GetLastSpawned().GetComponent<Projectile>();
                projectile.gameObject.SetActive(false);
        
                if (_projectileHandler == null)
                    Debug.LogError("Projectile Handler not set.");
                _projectileHandler.AddToStack(projectile);
            }
        }
        private void SetupTargets(LevelData levelData)
        {
          var birdsDictionary = new Dictionary<int, Model.Pigs.Birds>();
          var birdStatusDict = new Dictionary<int, bool>();
          _birdsHandler.BirdsDictionary = birdsDictionary;
          _birdsHandler.BirdStatusDict = birdStatusDict;
            
          for (var i = 0; i < levelData.BirdsLocations.Count; i++)
          {
              _spawner.SpawnAt(_birdPrefab, levelData.BirdsLocations[i]);
              var bird = _spawner.GetLastSpawned().GetComponent<Model.Pigs.Birds>();
              bird.Id = i;
              _birdsHandler.BirdsDictionary.Add(bird.Id, bird);
              _birdsHandler.BirdStatusDict.Add(bird.Id, false);
              _birdsHandler.SubscribeBirdDeathEvent();
          }
        }
        private void SetupEnvironment(LevelData levelData)
        {
            foreach (var t in levelData.Stages)
            {
                _spawner.SpawnAt(t.gameObject, t);
            }
        }
        protected virtual void OnLevelSetup(GameHandlersEventArgs gameHandlers)
        {
            _spawner.OnObjectSpawned -= OnObjectSpawned_CreateGameHandlers;
            LevelSetup?.Invoke(this, gameHandlers);
        }
        public void Clean() => _spawner.DestroyAllSpawned();
    }

    public class GameHandlersEventArgs : EventArgs
    {
        public BirdsHandler BirdsHandler;
        public ProjectileHandler ProjectileHandler;

        public GameHandlersEventArgs(ProjectileHandler projectileHandler, BirdsHandler birdsHandler)
        {
            ProjectileHandler = projectileHandler;
            BirdsHandler = birdsHandler;
        }
    }
}