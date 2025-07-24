using System;
using UnityEngine;
using System.Collections;
//using Arcade.Project.Runtime.Games.AngryBird.Cues;
using Arcade.Project.Runtime.Games.AngryBird.Interfaces;
using Arcade.Project.Runtime.Games.AngryBird.Utils.Events;
using Project.Scripts.Runtime.Games;

namespace Arcade.Project.Runtime.Games.AngryBird
{
  // projectile is better suited to be event based.
    public class Projectile : MonoBehaviour
    {
      private LayerMask m_EnvironmentLayer;
      private LayerMask m_GroundLayer;
      public Rigidbody2D Rb {get; private set;}
      public Collider2D Col {get; private set;}
      public bool IsSelected {get; private set;}
      public ObservableValue<bool> IsMoving = new ObservableValue<bool>(true);
      public ObservableValue<bool> IsTouchingGround = new ObservableValue<bool>(false);
      public ObservableValue<bool> IsUsed = new ObservableValue<bool>(false);

      public event EventHandler OnProjectileUsed;
      public bool IsFlying { get; set; }

      private void Awake()
      {
        m_EnvironmentLayer = LayerMask.GetMask("Environment");
        m_GroundLayer = LayerMask.GetMask("Ground");

        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();

        IsSelected = false;
        IsFlying = false;
        IsUsed.ValueChanged += DisableBehaviour;
      }

      private void OnDisable()
      {
        IsUsed.ValueChanged -= DisableBehaviour;
      }

      private void DisableBehaviour(object sender, ValueChangedEventArgs<bool> e) => enabled = false;
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

      public void SetProjectileSelected()
      {
        IsSelected = true;
      }

      private void Update()
      {
        IsTouchingGround.Value = Col.IsTouchingLayers(m_GroundLayer) || Col.IsTouchingLayers(m_EnvironmentLayer);
        IsMoving.Value = !(Rb.linearVelocity.magnitude <= 0.5f);
        
        IsUsed.Value = (IsTouchingGround.Value && IsMoving.Value);
        
        if (IsUsed.Value)
          OnProjectileUsed?.Invoke(this, EventArgs.Empty);
      }
    }
}