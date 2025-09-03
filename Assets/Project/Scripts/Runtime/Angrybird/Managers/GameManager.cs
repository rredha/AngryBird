using System;
using System.Collections.Generic;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.States;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Transitions;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Level;
using Project.Scripts.Runtime.Angrybird.Presenter.Slingshot;
using Project.Scripts.Runtime.Angrybird.Utils;
using Project.Scripts.Runtime.Core.SessionManager;
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
    
    private bool _replayTriggered; // should be reload level.
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
      
     _gameStateMachine.AddState("Finish",
       new State(
         onEnter: state => FinishStateEnter(),
         onExit: state => FinishStateExit()
       ));
     
     // TODO:
     // add condition for passing from init to playing.
     // for instance the projectile needs to be in the correct init position.
     
     _gameStateMachine.AddTransition(new Transition(
       "Init", "Playing", transition => true));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Init", transition => levelManager.Projectile.IsThrown && !levelManager.OutOfAttempts));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Finish", transition => levelManager.Projectile.IsTouchingGround && 
                                          (levelManager.LevelStatus is LevelStatusEnum.Completed) || (levelManager.OutOfAttempts)));
     
     _gameStateMachine.AddTransition(new Transition(
       "Finish", "Init", transition => _replayTriggered));
     
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
      // TODO :
      // Fix issue with Aiming Timer,
      // Also wrong switching from init -> playing on last attempt
      
      StopPlayingTaskTimer += playingTaskTimer.Disable;
      StartPlayingTaskTimer -= playingTaskTimer.Enable;
      
      StopPlayingTaskTimer?.Invoke(this,EventArgs.Empty);
      levelManager.ProjectileHandler.Unsubscribe();
      levelManager.ProjectileHandler.UnsubscribeBeginDroppingTimer();

      var sessionMetrics = new SessionMetrics(
        levelManager.CurrentLevel, levelManager.Attempt,
        playingTaskTimer.Total,
        droppingTaskTimer.Total,
        aimingTaskTimer.Total);
      
      SessionManager.Instance.AddMetric(sessionMetrics);
    }

  }
  
  
  // Lost State
  public partial class GameManager
  {
    /*
    private void LostStateEnter()
    {
      SessionManager.Instance.Export();
      _uiManager.Show("Lost");
    }

    private void LostStateExit()
    {
    }
    */
  }
  // Finish State
  public partial class GameManager
  {
    // TODO:
    // fix LostUI issue.
    private void FinishStateEnter()
    {
      SessionManager.Instance.Export();
      
      if (LevelManager.Instance.LevelStatus == LevelStatusEnum.Completed)
      {
        _uiManager.Show("Won");
        UIManager.Instance.WonUI.ReplayTriggered += OnReplayTriggered_Reset;
      }
      else if (LevelManager.Instance.LevelStatus == LevelStatusEnum.UnCompleted)
      {
        _uiManager.Show("Lost");
        //UIManager.Instance.LostUI.ReplayTriggered += OnReplayTriggered_Reset;
      }
    }

    private void FinishStateExit()
    {
      if (_replayTriggered)
      {
        _levelBuilder.Clean();
        _firstGame = true;
      }

      UIManager.Instance.WonUI.ReplayTriggered -= OnReplayTriggered_Reset;
      //UIManager.Instance.LostUI.ReplayTriggered -= OnReplayTriggered_Reset;
      
      _replayTriggered = false;
    }
    
    private void OnReplayTriggered_Reset(object sender, EventArgs e)
    {
      _replayTriggered = true;
    }
  }
}