using System;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils.GameState
{
  /*
  public class Level
  {
    public int NumberOfBirds { get; set; }
    public int NumberOfProjectiles { get; set; }
    public Transform[] BirdsLocations { get; set; }
  }
  */
  public partial class GameManager : MonoBehaviour
  {
    public static GameManager current;

    [SerializeField] private GameConfigurationSO m_Configuration;
    
    [SerializeField] private GameObject WonUI;
    [SerializeField] private GameObject LostUI;
    
    [SerializeField] private Spawner m_Spawner;
    
    private GameObject m_ActiveProjectile;
    private Projectile m_Projectile;
    [SerializeField] private Transform m_ProjectileLocation;
    private Birds m_Bird;
    [SerializeField] private List<Transform> m_BirdLocations;
    
    private StateMachine m_GameStateMachine;

    private ProjectileHandler _projectileHandler;
    private BirdsHandler _birdsHandler;
    //private Level _levelOne;
    
    private bool m_NoMoreAttemptsLeft;
    private bool m_BirdsDestroyed;
    private bool m_ProjectileUsed;

    private void Awake()
    {
      /*
      _levelOne = new Level();
      _levelOne.NumberOfBirds = 3;
      _levelOne.NumberOfProjectiles = 3;
      */
      
      current = this; // singleton
      
      _projectileHandler = new ProjectileHandler(m_Spawner);
      _projectileHandler.CacheProjectiles(3);
      _birdsHandler = new BirdsHandler(3, m_BirdLocations, m_Spawner);
      
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
       "Playing", "Init", transition => m_Projectile.IsUsed));
     
     /*
     m_GameStateMachine.AddTransition(new Transition(
       "Init", "Won", transition => m_BirdsDestroyed));
       */
     
     m_GameStateMachine.AddTransition(new Transition(
       "Playing", "Lost", transition => m_NoMoreAttemptsLeft));
     
     m_GameStateMachine.SetStartState("Init");
     m_GameStateMachine.Init();
    }

    private void Update()
    {
      m_GameStateMachine.OnLogic();
    }
  }

  // Setup state
  public partial class GameManager
  {
    private void InitStateEnter()
    {
      _projectileHandler.GetProjectile();
      m_Projectile = _projectileHandler.Current;
      
      m_ProjectileUsed = false;
      m_BirdsDestroyed = false;
    }
  }
  // Playing state
  public partial class GameManager
  {
    private void PlayingStateEnter()
    {
      _projectileHandler.OnEmpty += OnProjectileStackEmpty_Perform;
      _birdsHandler.OnListEmpty += OnBirdListEmpty_Perform;
    }
    private void OnProjectileStackEmpty_Perform(object sender, EventArgs e)
    {
      m_NoMoreAttemptsLeft = true;
    }

    private void OnBirdListEmpty_Perform(object sender, EventArgs e)
    {
      m_BirdsDestroyed = true;
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
    private void LostStateEnter() => Debug.Log("You lost");
  }
}