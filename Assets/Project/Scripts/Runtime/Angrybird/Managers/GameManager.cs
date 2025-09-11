using System;
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
    //[SerializeField] private UIManager _uiManager;
    private bool _firstGame = true;

    private bool _replayTriggered; // should be reload level.
    private StateMachine _gameStateMachine;
    private LevelBuilder _levelBuilder;
    public LevelManager levelManager{ get; private set; }

    private void Awake()
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);

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
       "Playing", "Finish", transition => levelManager.ProjectileHandler.Current.IsTouchingGround && 
         (levelManager.BirdsHandler.AllBirdsDestroyed || levelManager.OutOfAttempts)));
     
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
    private void InitStateExit()
    {
      _firstGame = false;
    }

  }
  // Playing state
  public partial class GameManager
  {
    public DurationTracker PlayingDurationTracker;
    //public DurationMonitor PlayingDurationMonitor;
    
    public DurationTracker SelectingTaskTracker;
    //public DurationMonitor SelectingTaskMonitor;
    
    public DurationTracker DroppingTaskTracker;
    //public DurationMonitor DroppingTaskMonitor;

    public DurationTracker AimingTaskTracker;
    //public DurationMonitor AimingTaskMonitor;
    
    private void PlayingStateEnter()
    {
      PlayingDurationTracker = new DurationTracker();
      //PlayingDurationMonitor = new DurationMonitor(PlayingDurationTracker, "Total Playing");
      
      /*
      SelectingTaskTracker = new DurationTracker();
      SelectingTaskMonitor = new DurationMonitor(SelectingTaskTracker, "Selecting");
      */
      SelectingTaskTracker = levelManager.ProjectileHandler.Current.SelectingTaskTracker;
      //SelectingTaskMonitor = levelManager.ProjectileHandler.Current.SelectingTaskMonitor;
      
      DroppingTaskTracker = new DurationTracker();
      //DroppingTaskMonitor = new DurationMonitor(DroppingTaskTracker, "Dropping");
      
      AimingTaskTracker = new DurationTracker();
      //AimingTaskMonitor = new DurationMonitor(AimingTaskTracker, "Aiming");
      
      //PlayingDurationMonitor.Subscribe();
      PlayingDurationTracker.StartRecording();
      
      levelManager.ProjectileHandler.Subscribe();
    }
    private void PlayingStateUpdate()
    {
    }
    private void PlayingStateExit()
    {
      PlayingDurationTracker.StopRecording();
      //PlayingDurationMonitor.Unsubscribe();

      levelManager.ProjectileHandler.Unsubscribe();

      var sessionMetrics = new SessionMetrics(
        levelManager.CurrentLevel, levelManager.Attempt,
        PlayingDurationTracker.Data.Total,
        SelectingTaskTracker.Data.Total,
        DroppingTaskTracker.Data.Total,
        AimingTaskTracker.Data.Total);
      
      SessionManager.Instance.AddMetric(sessionMetrics);
    }
  }
  
  // Finish State
  public partial class GameManager
  {
    // TODO:
    // fix LostUI issue, remake prefab and prefab variants.
    private void FinishStateEnter()
    {
      SessionManager.Instance.Export();
      //SessionManager.Instance.Log(SessionManager.Instance.SessionMetrics);
      
      if (LevelManager.Instance.LevelStatus == LevelStatusEnum.Completed)
      {
        UIManager.Instance.Show("Won");
        UIManager.Instance.WonUI.ReplayTriggered += OnReplayTriggered_Reset;
      }
      else if (LevelManager.Instance.LevelStatus == LevelStatusEnum.UnCompleted)
      {
        UIManager.Instance.Show("Lost");
        UIManager.Instance.LostUI.ReplayTriggered += OnReplayTriggered_Reset;
      }
    }

    private void FinishStateExit()
    {
      _levelBuilder.Clean();
      UIManager.Instance.WonUI.ReplayTriggered -= OnReplayTriggered_Reset;
      UIManager.Instance.LostUI.ReplayTriggered -= OnReplayTriggered_Reset;
      
      _replayTriggered = false;
    }
    
    private void OnReplayTriggered_Reset(object sender, EventArgs e)
    {
      levelManager.OutOfAttempts = false; // rework the cleaning and creation of a level
      _firstGame = true;
      _replayTriggered = true;
    }
  }
}