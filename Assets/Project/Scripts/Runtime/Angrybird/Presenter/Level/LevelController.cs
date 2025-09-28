using System;
using System.Collections.Generic;
using Arcade;
using Project.Scripts.Runtime.Angrybird.Model.Level;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Level
{
    public class LevelController : MonoBehaviour
    {
      [SerializeField] private List<LevelSO> availableLevels;
      [SerializeField] private LevelSO _levelSo;
      [SerializeField] private GameConfigurationSO configurationSo;
      [SerializeField] private GameObject projectilePrefab;
      [SerializeField] private GameObject birdPrefab;
      [SerializeField] private Spawner spawner;
      
      private LevelManager _levelManager;
      private LevelBuilder _builder;
      private LevelData _levelData;

      // Configure level selection tasks.
      // On play start a level.
      // On level finished, unload.
      // On replay, reload.
      private void Awake()
      {
        
        DontDestroyOnLoad(gameObject);
      }

      private void OnLevelSetup_GetHandlers(object sender, GameHandlersEventArgs gameHandlersEventArgs)
      {
          if (_levelManager == null)
              Debug.LogError("No Level Manager found");
          _levelManager.ProjectileHandler = gameHandlersEventArgs.ProjectileHandler;
          _levelManager.BirdsHandler = gameHandlersEventArgs.BirdsHandler;
      }


      private void LoadLevelDataFromScriptableObject(LevelSO levelSo)
      {
          _levelData = new LevelData(
              levelSo.LevelIndex,
              levelSo.Birds, levelSo.Projectiles,
              levelSo.ProjectileLocation, levelSo.BirdsLocations,
              levelSo.Stages);
      }

      private void OnDisable()
      {
          _builder.LevelSetup -= OnLevelSetup_GetHandlers;
      }
      public void LoadLevel()
      {
          _levelManager = GetComponent<LevelManager>();
          LoadLevelDataFromScriptableObject(_levelSo);
          _builder = new LevelBuilder(projectilePrefab, birdPrefab, _levelData,  spawner);
          _builder.LevelSetup += OnLevelSetup_GetHandlers;
          
          _builder.Init();
          _builder.LevelData = _levelData;
          _levelManager.Setup();
      }

      public void UnLoadLevel()
      {
          _builder.Clean();
          _levelManager.UnloadLevel();
      }

      public void RestartLevel()
      {
      }
    }
}
