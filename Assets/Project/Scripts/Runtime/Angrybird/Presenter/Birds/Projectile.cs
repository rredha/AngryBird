using System;
using Project.Scripts.Runtime.Angrybird.Managers;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
  public partial class Projectile : MonoBehaviour
  {
    private LayerMask _environmentLayer;
    private LayerMask _obstacleLayer; 
    private LayerMask _groundLayer;
    public Rigidbody2D Rb {get; private set;}
    public Collider2D Col {get; private set;}
    public bool IsSelected { get; set; }
    public bool IsThrown { get; set; }
    public bool IsOverlapped { get; set; }
    public bool IsTouchingGround => !Col.IsTouchingLayers(_environmentLayer) &&
                                    (Col.IsTouchingLayers(_obstacleLayer) || Col.IsTouchingLayers(_groundLayer)); 
    public event EventHandler OnProjectileUsed;
    private void Awake()
    {
      _environmentLayer = LayerMask.GetMask("Environment");
      _obstacleLayer = LayerMask.GetMask("Obstacles");
      _groundLayer = LayerMask.GetMask("Ground");

      Rb = GetComponent<Rigidbody2D>();
      Col = GetComponent<Collider2D>();
    }
    public void SetStatic()
    {
      Col.enabled = true;
      Rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void SetDynamic()
    {
      Col.enabled = true;
      Rb.bodyType = RigidbodyType2D.Dynamic;
    }
  }
  public partial class Projectile
  // Idle state
  {
    public DurationTracker SelectingTaskTracker;
    //public DurationMonitor SelectingTaskMonitor;
    public bool IsIdle;
    public void IdleStateEnter()
    {
      /* TO DO : Need to add either a time buffer or to change things up.
       *    Idle state should be when we enter playing state.
       */
      /*
        if (Col.IsTouchingLayers(_groundLayer))
        {
          Debug.Log("Im ready to be selected");
          GameManager.Instance.SelectingTaskTracker.StartRecording();
          GameManager.Instance.SelectingTaskMonitor.Subscribe();
      }
      */
      SelectingTaskTracker = new DurationTracker();
      //SelectingTaskMonitor = new DurationMonitor(SelectingTaskTracker, "Selecting");
      
      IsIdle = true;
      IsSelected = false;
      
      SelectingTaskTracker.StartRecording();
      //SelectingTaskMonitor.Subscribe();
    }

    public void IdleStateUpdate()
    {
    }
    public void IdleStateExit()
    {
      IsIdle = false;
    }
    
  }
  public partial class Projectile
    // Selected State
  {
    public void SelectedStateEnter()
    {
      SelectingTaskTracker.StopRecording();
      //SelectingTaskMonitor.Unsubscribe();
      
      //GameManager.Instance.DroppingTaskMonitor.Subscribe();
      GameManager.Instance.DroppingTaskTracker.StartRecording();
      IsOverlapped = false;
    }
    public void SelectedStateExit()
    {
    }
  }
  public partial class Projectile
    // Used State
  {
    public void UsedStateEnter()
    {
      IsSelected = false;
    }
    public void UsedStateUpdate()
    {
      var isLaunched = !Col.IsTouchingLayers(_environmentLayer) &&
                       (Col.IsTouchingLayers(_obstacleLayer) ||
                        Col.IsTouchingLayers(_groundLayer));
      var isMoving = Rb.velocity.sqrMagnitude <= 1f;
      if (!isMoving && isLaunched)
        OnProjectileUsed?.Invoke(this, EventArgs.Empty);
    }
    public void UsedStateExit()
    {
    }
  }
}