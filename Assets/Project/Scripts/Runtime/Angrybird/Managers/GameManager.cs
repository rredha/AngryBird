using System;
using System.Collections.Generic;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.States;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Transitions;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Level;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
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
    
    [SerializeField] private UIManager _uiManager;

    public event EventHandler StartPlayingTaskTimer;
    public event EventHandler StopPlayingTaskTimer;
    public TaskTimer droppingTaskTimer { get; private set; }
    public TaskTimer aimingTaskTimer { get; private set; }
    public TaskTimer playingTaskTimer { get; private set; }

    private List<TaskTimer> record;
    
    private StateMachine _gameStateMachine;
    private LevelBuilder _levelBuilder;
    public LevelManager levelManager{ get; private set; }

    private void Awake()
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);

      droppingTaskTimer = new TaskTimer();
      aimingTaskTimer = new TaskTimer();
      playingTaskTimer = new TaskTimer();

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
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Won", transition => levelManager.Projectile.IsTouchingGround && (levelManager.LevelStatus is LevelStatusEnum.Completed) ));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Lost", transition => levelManager.OutOfAttempts && levelManager.Projectile.IsTouchingGround));
     
     _gameStateMachine.AddTransition(new Transition(
       "Won", "Init", transition => _replayTriggered));
     
     _gameStateMachine.AddTransition(new Transition(
       "Lost", "Init", transition => _replayTriggered));
     
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
    private void PlayingStateEnter()
    {
      StartPlayingTaskTimer += playingTaskTimer.Enable;
      StopPlayingTaskTimer -= playingTaskTimer.Disable;
      
      StartPlayingTaskTimer?.Invoke(this, EventArgs.Empty);
      levelManager.ProjectileHandler.Subscribe();
      levelManager.ProjectileHandler.SubscribeBeginDroppingTimer();
    }

    private void PlayingStateUpdate()
    {
    }

    private void PlayingStateExit()
    {
      StopPlayingTaskTimer += playingTaskTimer.Disable;
      StartPlayingTaskTimer -= playingTaskTimer.Enable;
      
      StopPlayingTaskTimer?.Invoke(this,EventArgs.Empty);
      levelManager.ProjectileHandler.Unsubscribe();
      levelManager.ProjectileHandler.UnsubscribeBeginDroppingTimer();
    }
  }
  
  
  // Lost State
  public partial class GameManager
  {
    private void LostStateEnter()
    {
      TaskTimerCSV.Export("aiming.csv", aimingTaskTimer);
      TaskTimerCSV.Export("playing.csv", playingTaskTimer);
      TaskTimerCSV.Export("dropping.csv", droppingTaskTimer);
      
      _uiManager.Show("Lost");
    }

    private void LostStateExit()
    {
    }
  }
  // Won State
  public partial class GameManager
  {
    private void WonStateEnter()
    {
      _uiManager.Show("Won");
      UIManager.Instance.WonUI.ReplayTriggered += OnReplayTriggered_Reset;
    }

    private bool _replayTriggered;
    private void OnReplayTriggered_Reset(object sender, EventArgs e)
    {
      _levelBuilder.Clean();
      _firstGame = true;
      _replayTriggered = true;
    }
  }
}