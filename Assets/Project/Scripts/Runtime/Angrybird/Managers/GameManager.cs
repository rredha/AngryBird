using System;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.View.UI;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.States;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Transitions;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Level;
using Project.Scripts.Runtime.Angrybird.Presenter.Pigs;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Managers
{
  public partial class GameManager : MonoBehaviour
  {
    public static GameManager Instance;

    private GameConfigurationSO _configuration;
    
    [SerializeField] private GameObject wonUI;
    [SerializeField] private Transform lostUI;
    
    
    private Projectile _projectile;
    
    private StateMachine _gameStateMachine;

    private BirdsHandler _birdsHandler;
    private LevelBuilder _level;

    private bool _replayTriggered;
    private bool _noAttemptsLeft;
    private bool _birdsDestroyed;
    private bool _projectileUsed;

    private void Awake()
    {
      Instance = this;

      _level = GetComponent<LevelBuilder>();
      
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
         onEnter: state => LostStateEnter(),
         onExit: state => LostStateExit()
       ));
      
     _gameStateMachine.AddTransition(new Transition(
       "Init", "Playing", transition => true));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Init", transition => _level.Projectile.IsUsed));
     
     //m_GameStateMachine.AddTransition(new Transition(
     //  "Playing", "Won", transition => m_BirdsDestroyed ));
     
     _gameStateMachine.AddTransition(new Transition(
       "Playing", "Lost", transition => _level.ProjectileHandler.IsStackEmpty 
                                            &&  _level.ProjectileHandler.Current.IsThrown 
                                            &&  _level.ProjectileHandler.Current.IsTouchingGround));
     
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
      if (_replayTriggered || _firstGame)
      {
        Debug.Log("I’m ready to initialize everything");
        _level.Initialize();
      }
      else
      {
        _level.Proceed();
      }
      
      _projectileUsed = false;
      _birdsDestroyed = false;
    }

    private bool _firstGame = true;

    private void InitStateExit()
    {
      _firstGame = false;
      _replayTriggered = false;
    }

  }
  // Playing state
  public partial class GameManager
  {
    private void PlayingStateEnter()
    {
    }

    private void PlayingStateExit()
    {
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
    private Transform lostUIBehaviour;
    private void LostStateEnter()
    {
      lostUIBehaviour = Instantiate(lostUI);
      lostUIBehaviour.gameObject.GetComponent<LostUI>().ReplayTriggered += OnReplayTriggered_Set;
      Debug.Log("I’m in lost state enter");
      //_lostUIBehaviour.ReplayTriggered += OnReplayTriggered_Set;
    }

    private void LostStateExit()
    {
      Debug.Log("I’m in lost state exit : Triggered" + _replayTriggered);
      lostUIBehaviour.gameObject.GetComponent<LostUI>().ReplayTriggered -= OnReplayTriggered_Set;
      _level.Clean(); 
    }
    private void OnReplayTriggered_Set(object sender, EventArgs e)
    {
      _replayTriggered = true;
      
    }
  }
}