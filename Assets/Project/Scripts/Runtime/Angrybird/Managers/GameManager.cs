using System;
using System.Collections.Generic;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.States;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Transitions;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Level;
using Project.Scripts.Runtime.Angrybird.Presenter.Slingshot;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Managers
{
  public partial class GameManager : MonoBehaviour
  {
    public static GameManager Instance;
    private GameConfigurationSO _configuration;
    [SerializeField] private Slingshot slingshot;
    
    [SerializeField] private GameObject wonUI;
    [SerializeField] private Transform lostUI;

    public TaskTimer droppingTaskTimer { get; private set; }
    public TaskTimer aimingTaskTimer { get; private set; }
    private StateMachine _gameStateMachine;
    private LevelBuilder _levelBuilder;
    public LevelManager levelManager{ get; private set; }

    private void Awake()
    {
      Instance = this;

      droppingTaskTimer = new TaskTimer();
      aimingTaskTimer = new TaskTimer();
      _levelBuilder = GetComponent<LevelBuilder>();
      levelManager = GetComponent<LevelManager>();
      
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
          onLogic: state => PlayingStateUpdate(),
          onExit: state => PlayingStateExit()
            ));
      
     _gameStateMachine.AddState("Won",
       new State(
         onEnter: state => WonStateEnter()
       ));
     _gameStateMachine.AddState("Lost",
       new State(
         onEnter: state => LostStateEnter(),
         onExit: state => LostStateExit()
       ));
      
     _gameStateMachine.AddTransition(new Transition(
       "Init", "Playing", transition => true));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Init", transition => levelManager.Projectile.IsThrown && !levelManager.OutOfAttempts));
     
     //m_GameStateMachine.AddTransition(new Transition(
     //  "Playing", "Won", transition => m_BirdsDestroyed ));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Lost", transition => levelManager.OutOfAttempts));
     /*
     _gameStateMachine.AddTransition(new Transition(
       "Init", "Lost", transition => _levelManager.ProjectileHandler.IsStackEmpty
                                            &&  _levelManager.ProjectileHandler.Current.IsThrown
                                            &&  _levelManager.ProjectileHandler.Current.IsTouchingGround));
      */
     
     /*
     _gameStateMachine.AddTransition(new Transition(
       "Lost", "Init", transition => _replayTriggered));
       */
     
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
      if (_firstGame)
      {
        _levelBuilder.Init();
        _levelBuilder.PopFirstProjectile();
      }
      else
      {
        levelManager.Proceed();
      }
    }

    private bool _firstGame = true;

    private void InitStateExit()
    {
      _firstGame = false;
    }

  }
  // Playing state
  public partial class GameManager
  {
    private readonly List<float> _playingTimerData = new();
    private float _playingTimer;

    private void ReportPlayingTimer()
    {
      Debug.Log("Playing State Timers :");
      for (var i = 0; i < _playingTimerData.Count - 1; i++)
      {
        Debug.Log($"Attempt {i+1} : {_playingTimerData[i]} s.");
      }
    }
    private void PlayingStateEnter()
    {
      levelManager.ProjectileHandler.Subscribe();
      levelManager.ProjectileHandler.SubscribeBeginDroppingTimer();
    }

    private void PlayingStateUpdate()
    {
      _playingTimer += Time.deltaTime;
    }

    private void PlayingStateExit()
    {
      _playingTimerData.Add(_playingTimer);
      _playingTimer = 0f;
      
      levelManager.ProjectileHandler.Unsubscribe();
      levelManager.ProjectileHandler.UnsubscribeBeginDroppingTimer();
    }
  }
  
  
  // Lost State
  public partial class GameManager
  {
    private Transform lostUIBehaviour;
    private void LostStateEnter()
    {
      //levelManager.Projectile.ReportDroppingTimer();
      droppingTaskTimer.Log("Dropping");
      aimingTaskTimer.Log("Aiming");
      ReportPlayingTimer();
      lostUIBehaviour = Instantiate(lostUI);
      //lostUIBehaviour.gameObject.GetComponent<LostUI>().ReplayTriggered += OnReplayTriggered_Set;
      //_lostUIBehaviour.ReplayTriggered += OnReplayTriggered_Set;
    }
    private void LostStateExit()
    {
      //lostUIBehaviour.gameObject.GetComponent<LostUI>().ReplayTriggered -= OnReplayTriggered_Set;
      //_levelBuilder.Clean(); 
    }
  }
  // Won State
  public partial class GameManager
  {
    private void WonStateEnter() => Debug.Log("Congratulations");
  }
}