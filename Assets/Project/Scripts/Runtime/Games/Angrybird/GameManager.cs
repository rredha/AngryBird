using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityHFSM;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils.GameState
{
  public partial class GameManager : MonoBehaviour
  {
    public static GameManager Instance;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject birdPrefab;

    private GameConfigurationSO _configuration;
    
    [SerializeField] private GameObject wonUI;
    [SerializeField] private Transform lostUI;
    
    private Spawner _spawner;
    
    private Projectile _projectile;
    
    private StateMachine _gameStateMachine;

    private ProjectileHandler _projectileHandler;
    private BirdsHandler _birdsHandler;
    private LevelBuilder _level;
    
    private bool _noAttemptsLeft;
    private bool _birdsDestroyed;
    private bool _projectileUsed;

    private void Awake()
    {
      Instance = this;

      _spawner = GetComponent<Spawner>();
      _level = GetComponent<LevelBuilder>();
      _projectileHandler = new ProjectileHandler(_spawner)
      {
        Prefab = projectilePrefab
      };
      _birdsHandler = new BirdsHandler(_spawner)
      {
        Prefab = birdPrefab
      };
      
      _projectileHandler.CacheProjectiles(_level.Data.Projectiles, _level.Config.Platform);
      _birdsHandler.CreateBirds(_level.Data.Birds, _level.Data.BirdsLocations);
      
      SetupStateMachine();
    }

    private void SetupStateMachine()
    {
      _gameStateMachine = new StateMachine();
      _gameStateMachine.AddState("Init",
        new State(
          onEnter: state => InitStateEnter(),
          onExit: state => InitStateExit()
        ));
      _gameStateMachine.AddState("Playing",
        new State(
          onEnter: state => PlayingStateEnter(),
          onExit: state => PlayingStateExit()
            ));
      
     _gameStateMachine.AddState("Won",
       new State(
         onEnter: state => WonStateEnter()
       ));
     _gameStateMachine.AddState("Lost",
       new State(
         onEnter: state => LostStateEnter()
       ));
      
     _gameStateMachine.AddTransition(new Transition(
       "Init", "Playing", transition => true));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Init", transition => _projectile.IsUsed));
     
     //m_GameStateMachine.AddTransition(new Transition(
     //  "Playing", "Won", transition => m_BirdsDestroyed ));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Lost", transition => _projectileHandler.IsStackEmpty && _projectileHandler.Current.IsThrown && _projectileHandler.Current.IsTouchingGround));
     
     _gameStateMachine.SetStartState("Init");
     _gameStateMachine.Init();
    }

    private void Update()
    {
      _gameStateMachine.OnLogic();
    }
  }

  // Setup state
  public partial class GameManager
  {
    private void InitStateEnter()
    {
      _projectileHandler.GetProjectile(); // needs to rethink
      _projectileHandler.OnEmpty += OnProjectileStackEmpty_Perform;
      _projectile = _projectileHandler.Current;
      
      _projectileUsed = false;
      _birdsDestroyed = false;
    }
    private void InitStateExit()
    {
      _projectileHandler.OnEmpty -= OnProjectileStackEmpty_Perform;
    }
    private void OnProjectileStackEmpty_Perform(object sender, EventArgs e)
    {
      _noAttemptsLeft = true;
    }
  }
  // Playing state
  public partial class GameManager
  {
    private void PlayingStateEnter()
    {
      _birdsHandler.OnListEmpty += OnBirdListEmpty_Perform;
    }

    private void OnBirdListEmpty_Perform(object sender, EventArgs e)
    {
      _birdsDestroyed = true;
    }
    private void PlayingStateExit()
    {
      _projectileHandler.OnEmpty -= OnProjectileStackEmpty_Perform;
      _birdsHandler.OnListEmpty -= OnBirdListEmpty_Perform;
    }
  }
  
  // Won State
  public partial class GameManager
  {
    private void WonStateEnter() => Debug.Log("Congratulations");
  }
  
  // Lost State
  public partial class GameManager
  {
    private void LostStateEnter()
    {
      Instantiate(lostUI);
    }
  }
}