using System;
using System.Collections.Generic;
using Project.Scripts.Runtime.Angrybird.Managers;
using Project.Scripts.Runtime.Angrybird.Model.Slingshot;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.View.Slingshot;
using UnityEngine;

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
      StopDroppingStateTimer?.Invoke(this, EventArgs.Empty);
    }
  }
  
  public partial class Slingshot
  {
    private readonly List<float> _aimingTimerData = new();

    public event EventHandler StopDroppingStateTimer;
    public event EventHandler StartAimingStateTimer;
    public event EventHandler StopAimingStateTimer;
    public void LoadedStateEnter()
    {
      _behaviour.EnablePlayerActions();
      _visual.EnablePlayerActions();
      
      _behaviour.Subscribe();
      _visual.Subscribe();
      StartAimingStateTimer += GameManager.Instance.aimingTaskTimer.Enable;
      StopAimingStateTimer -= GameManager.Instance.aimingTaskTimer.Disable;
      
      StartAimingStateTimer?.Invoke(this, EventArgs.Empty);
    }

    public void LoadedStateUpdate()
    {
      _behaviour.SetProjectileToHolder();
    }
    public void LoadedStateExit()
    {
      _context.ReleasedTriggered = false;
      _dropZone.IsOverlapping = false;
      
      _behaviour.Unsubscribe();
      _visual.Unsubscribe();
      
      // unsubscribing from events
      StopAimingStateTimer += GameManager.Instance.aimingTaskTimer.Disable;
      StartAimingStateTimer -= GameManager.Instance.aimingTaskTimer.Enable;
      
      StopAimingStateTimer?.Invoke(this, EventArgs.Empty);
      StopDroppingStateTimer -= GameManager.Instance.droppingTaskTimer.Disable;
    }
  }
}
