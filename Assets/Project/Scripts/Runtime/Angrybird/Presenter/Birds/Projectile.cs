using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Debug = UnityEngine.Debug;


namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
  // check if a event based or simple script will get better result that fsm.
  // projectile is better suited to be event based.

  public partial class Projectile
  // Idle state
  {
    public bool IsIdle;
    public void IdleStateEnter()
    {
      IsIdle = true;
      IsSelected = false;
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
    public event EventHandler DroppingStageBegin;
    private readonly List<float> _droppingTimerData = new();
    private float _droppingTimer;

    public void ReportDroppingTimer()
    {
      Debug.Log("Dropping State Timers :");
      for (var i = 0; i < _droppingTimerData.Count; i++)
      {
        Debug.Log($"Attempt {i+1} : {_droppingTimerData[i]} s.");
      }
    }
    public void SelectedStateEnter()
    {
      IsOverlapped = false;
      DroppingStageBegin?.Invoke(this, EventArgs.Empty);
    }
    public void SelectedStateUpdate()
    {
      _droppingTimer += Time.deltaTime;
    }
    public void SelectedStateExit()
    {
      _droppingTimerData.Add(_droppingTimer);
      _droppingTimer = 0f;
    }
    
  }
  public partial class Projectile
    // Used State
  {
    private void OnProjectileUsed_Notify(object sender, EventArgs e)
    {
      Debug.Log(gameObject.name + " Used");
    }
    public void UsedStateEnter()
    {
      IsSelected = false;
    }
    public void UsedStateUpdate()
    {
      var isLaunched = !Col.IsTouchingLayers(m_EnvironmentLayer) &&
                       (Col.IsTouchingLayers(m_ObstacleLayer) ||
                        Col.IsTouchingLayers(m_GroundLayer));
      var isMoving = Rb.velocity.sqrMagnitude <= 1f;
      if (!isMoving && isLaunched)
        OnProjectileUsed?.Invoke(this, EventArgs.Empty);
    }
    public void UsedStateExit()
    {
      
    }
    
  }
    public partial class Projectile : MonoBehaviour
    {
      private LayerMask m_EnvironmentLayer;
      private LayerMask m_ObstacleLayer; 
      private LayerMask m_GroundLayer;
      public Rigidbody2D Rb {get; private set;}
      public Collider2D Col {get; private set;}
      public bool IsSelected { get; set; }
      public bool IsThrown { get; set; }
      public bool IsOverlapped { get; set; }


      public bool IsTouchingGround => !Col.IsTouchingLayers(m_EnvironmentLayer) &&
                                      (Col.IsTouchingLayers(m_ObstacleLayer) || Col.IsTouchingLayers(m_GroundLayer)); 
      
      public event EventHandler OnProjectileUsed;

      private void OnEnable()
      {
      }

      private void OnDisable()
      {
      }
      private void Awake()
      {
        m_EnvironmentLayer = LayerMask.GetMask("Environment");
        m_ObstacleLayer = LayerMask.GetMask("Obstacles");
        m_GroundLayer = LayerMask.GetMask("Ground");

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
}