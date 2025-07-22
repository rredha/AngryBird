using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Arcade.Project.Runtime.Games.AngryBird.Utils.Events;
using UnityEngine.Serialization;
using UnityHFSM;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils.GameState
{
  public class Level
  {
    public int NumberOfBirds { get; set; }
    public int NumberOfProjectiles { get; set; }
    public Transform[] BirdsLocations { get; set; }
  }
  public partial class GameManager : MonoBehaviour
  {
    public static GameManager current;

    [SerializeField] private GameConfigurationSO m_Configuration;
    
    [SerializeField] private GameObject WonUI;
    [SerializeField] private GameObject LostUI;
    
    [SerializeField] private Pointer m_Pointer;
    [SerializeField] private Spawner m_Spawner;
    
    private GameObject m_ActiveProjectile;
    private Projectile m_Projectile;
    [SerializeField] private Transform m_ProjectileLocation;
    private Birds m_Bird;
    [SerializeField] private List<Transform> m_BirdLocations;
    
    private StateMachine m_GameStateMachine;

    private ProjectileHandler _projectileHandler;
    private BirdsHandler _birdsHandler;
    private Level _levelOne;
    
    private bool m_NoMoreAttemptsLeft;
    private bool m_BirdsDestroyed;
    private bool m_ProjectileUsed;

    private void Awake()
    {
      current = this; // singleton
      
      _levelOne = new Level();
      _levelOne.NumberOfBirds = 3;
      _levelOne.NumberOfProjectiles = 3;
      
      _projectileHandler = new ProjectileHandler(3, m_Spawner);
      _birdsHandler = new BirdsHandler(_levelOne.NumberOfBirds, m_BirdLocations, m_Spawner);
      
      
      SetupStateMachine();
    }

    private void SetupStateMachine()
    {
      m_GameStateMachine = new StateMachine();
      m_GameStateMachine.AddState("Init",
        new State(
          onEnter: state => InitStateEnter()
        ));
      m_GameStateMachine.AddState("Playing",
        new State(
          onEnter: state => PlayingStateEnter(),
          onExit: state => PlayingStateExit()
            ));
      
     m_GameStateMachine.AddState("Won",
       new State(
         onEnter: state => WonStateEnter()
       ));
     m_GameStateMachine.AddState("Lost",
       new State(
         onEnter: state => LostStateEnter()
       ));
      
     m_GameStateMachine.AddTransition(new Transition(
       "Init", "Playing", transition => true));
     
     m_GameStateMachine.AddTransition(new Transition(
       "Playing", "Init", transition => m_ProjectileUsed));
     
     m_GameStateMachine.AddTransition(new Transition(
       "Playing", "Won", transition => m_NoMoreAttemptsLeft));
     
     m_GameStateMachine.AddTransition(new Transition(
       "Playing", "Lost", transition => m_BirdsDestroyed));
     
     m_GameStateMachine.SetStartState("Init");
     m_GameStateMachine.Init();
    }

    private void Update()
    {
      m_GameStateMachine.OnLogic();
    }

    #region Utility Methods
    /*
    private void ResolveDependencies(object sender, DependencyEventArgs args)
    {
      m_Projectile = args.Projectile;
      m_ProjectileLocation = args.ProjectileLocation;
      m_Bird = args.Bird;

    }
    private void SpawnNewProjectile(object sender, ValueChangedEventArgs<bool> e)
    {
      if (e.NewValue)
        StartCoroutine(Spawner.current.Spawn(
          m_Projectile.gameObject,
          m_ProjectileLocation));
    }
    */
    #endregion
  }

  // Setup state
  public partial class GameManager
  {
    private void InitStateEnter()
    {
      m_Projectile = _projectileHandler.GetCurrent();
    }
  }
  // Playing state
  public partial class GameManager
  {
    private void PlayingStateEnter()
    {
      m_ProjectileUsed = false;
      m_NoMoreAttemptsLeft = false;
      m_BirdsDestroyed = false;
      m_Projectile.OnProjectileUsed += OnProjectileUsed_Perform;
      _projectileHandler.OnEmpty += OnProjectileStackEmpty_Perform;
      _birdsHandler.OnListEmpty += OnBirdListEmpty_Perform;
    }
    private void OnProjectileUsed_Perform(object sender, EventArgs e)
    {
      m_ProjectileUsed = true;
    }
    private void OnProjectileStackEmpty_Perform(object sender, EventArgs e)
    {
      Debug.Log("Hello");
      m_NoMoreAttemptsLeft = true;
    }

    private void OnBirdListEmpty_Perform(object sender, EventArgs e)
    {
      m_BirdsDestroyed = true;
    }
    private void PlayingStateExit()
    {
      m_Projectile.OnProjectileUsed -= OnProjectileUsed_Perform;
      _projectileHandler.OnEmpty -= OnProjectileStackEmpty_Perform;
      _birdsHandler.OnListEmpty -= OnBirdListEmpty_Perform;
    }
  }
  
  // Won State
  public partial class GameManager
  {
    private void WonStateEnter() => Instantiate(WonUI);
  }
  
  // Lost State
  public partial class GameManager
  {
    private void LostStateEnter() => Instantiate(LostUI);
  }
}