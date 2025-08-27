using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Project.Scripts.Runtime.Angrybird.Managers;
using Project.Scripts.Runtime.Angrybird.Model.Slingshot;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.View.Slingshot;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Slingshot
{
  public partial class Slingshot : MonoBehaviour
  {
    [SerializeField] private GameObject _holder; // these two should be data
    [SerializeField] private DropZone _dropZone;
    [SerializeField] private Rubber _rubber;
    
    private SlingshotContext _context;
    private SlingshotBehaviour _behaviour;
    private SlingshotVisual _visual;

    private Projectile _projectile;


    public bool ReleasedTriggered => _context.ReleasedTriggered;
    public bool IsOverlapping => _dropZone.IsOverlapping;

    private void Awake()
    {
      _context = new SlingshotContext(_holder, _dropZone, _rubber);
      
      _behaviour = GetComponent<SlingshotBehaviour>();
      _visual = GetComponent<SlingshotVisual>();
      
      _behaviour.context = _context;
      _visual.context = _context;
    }
  }

  public partial class Slingshot
  {
    public void EmptyStateEnter()
    {
      _behaviour.Pointer.Initialize();
      _behaviour.Pointer.Subscribe();
    }
    public void EmptyStateUpdate()
    {
      _visual.InitializeRubber();
    }
    public void EmptyStateExit()
    {
      _behaviour.Pointer.Unsubscribe();
      StopDroppingStateTimer += GameManager.Instance.droppingTaskTimer.Disable; 
    }
  }
  
  public partial class Slingshot
  {
    private readonly List<float> _aimingTimerData = new();
    private float _aimingTimer;

    public event EventHandler StopDroppingStateTimer;
    public event EventHandler StartAimingStateTimer;
    public event EventHandler StopAimingStateTimer;
    /*
    public void ReportAimingTimer()
    {
      Debug.Log("Aiming :");
      for (var i = 0; i < _aimingTimerData.Count; i++)
      {
        Debug.Log($"Attempt {i+1} : {_aimingTimerData[i]} s.");
      }
    }
    */
    public void LoadedStateEnter()
    {
      StopDroppingStateTimer?.Invoke(this, EventArgs.Empty);
      StartAimingStateTimer?.Invoke(this, EventArgs.Empty);
      
      StartAimingStateTimer += GameManager.Instance.aimingTaskTimer.Enable;
      StartAimingStateTimer += OnStartAimingStateTimer_Notify;
      _behaviour.EnablePlayerActions();
      _visual.EnablePlayerActions();
      
      _behaviour.Subscribe();
      _visual.Subscribe();
    }

    private void OnStartAimingStateTimer_Notify(object sender, EventArgs e)
    {
      Debug.Log("Aiming Timer Start");
    }

    public void LoadedStateUpdate()
    {
      _behaviour.SetProjectileToHolder();
      
      /*
      _aimingTimer += Time.deltaTime;
      */
    }
    public void LoadedStateExit()
    {
      StopAimingStateTimer?.Invoke(this, EventArgs.Empty);
      StopAimingStateTimer += GameManager.Instance.aimingTaskTimer.Disable;
      StopAimingStateTimer += OnStopAimingStateTimer_Notify;  
      /*
      _aimingTimerData.Add(_aimingTimer);
      _aimingTimer = 0f;
      */
      
      _context.ReleasedTriggered = false;
      _dropZone.IsOverlapping = false;
      
      _behaviour.Unsubscribe();
      _visual.Unsubscribe();
      // unsubscribing from events
      StopDroppingStateTimer -= GameManager.Instance.droppingTaskTimer.Disable;
      StartAimingStateTimer -= GameManager.Instance.aimingTaskTimer.Enable;
      StopAimingStateTimer -= GameManager.Instance.aimingTaskTimer.Disable;
    }

    private void OnStopAimingStateTimer_Notify(object sender, EventArgs e)
    {
      Debug.Log("Aiming timer stopped");
    }
  }
}
