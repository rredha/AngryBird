using System;
using UnityEngine;


namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
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
      /*
      IsIdle = true;
      IsSelected = false;
      */
    }
    
  }
  public partial class Projectile
  // Selected State
  {
    public void SelectedStateEnter()
    {
      
    }
    public void SelectedStateUpdate()
    {
      
    }
    public void SelectedStateExit()
    {
      
    }
    
  }
  public partial class Projectile
    // Used State
  {
    private void OnProjectileUsed_Perform(object sender, EventArgs e)
    {
      IsUsed = true;
    }
    private void OnProjectileUsed_Notify(object sender, EventArgs e)
    {
      Debug.Log(gameObject.name + " Used");
    }
    private void OnProjectileUsed_Set(object sender, EventArgs e)
    {
      IsUsed = true;
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
      public bool IsUsed { get; set; }
      public bool IsSelected { get; set; }
      public bool IsThrown { get; set; }
      
      public bool IsSelectable { get; set; }

      public bool IsTouchingGround => !Col.IsTouchingLayers(m_EnvironmentLayer) &&
                                      (Col.IsTouchingLayers(m_ObstacleLayer) || Col.IsTouchingLayers(m_GroundLayer)); 
      
      public event EventHandler OnProjectileUsed;
      public bool IsFlying { get; set; }

      private void OnEnable()
      {
        OnProjectileUsed += OnProjectileUsed_Perform;
        OnProjectileUsed += OnProjectileUsed_Set;
      }

      private void OnDisable()
      {
        OnProjectileUsed -= OnProjectileUsed_Perform;
        OnProjectileUsed -= OnProjectileUsed_Set;
      }
      private void Awake()
      {
        m_EnvironmentLayer = LayerMask.GetMask("Environment");
        m_ObstacleLayer = LayerMask.GetMask("Obstacles");
        m_GroundLayer = LayerMask.GetMask("Ground");

        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        

        IsFlying = false;
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
      private void Update()
      {
      }
    }
}