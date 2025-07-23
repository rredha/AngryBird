using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Arcade.Project.Runtime.Games.AngryBird.Hints;
using Arcade.Project.Runtime.Games.AngryBird.Interfaces;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Arcade.Project.Runtime.Games.AngryBird.Configurations;

namespace Arcade.Project.Runtime.Games.AngryBird
{
  public class Pointer : MonoBehaviour
  {
    public List<IVisualHint> ColorChangeHints { get; private set; }

    private Camera _camera;
    private Color _defaultColor;
    private LayerMask _layerMask;
    public bool PointerOverlapProjectile { get; set; }

    public void SetPointerOverlapProjectile(bool value)
    {
      PointerOverlapProjectile = value;
    }

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private Vector3 _pointerWorldPosition;
    private Vector2 _pointerScreenPosition;

    private HapticPointer m_MovementProvider;
    //private MousePointer m_MovementProvider;
    public Projectile proj;

    private void Awake()
    {
      _camera = Camera.main;
      _collider = GetComponent<Collider2D>();
      _collider.isTrigger = true;
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _layerMask = LayerMask.GetMask("Selectables");

      // it work but it need to be implemented using IEnumerator
      ColorChangeHints = new List<IVisualHint>();
      ColorChangeHints.Add(new ColorChangeHintFactory().CreateVisualHint());
      ColorChangeHints.Add(new ColorChangeWithDelayFactory().CreateVisualHint());
      ColorChangeHints[0].Initialize(_spriteRenderer, Color.blue);
      ColorChangeHints[1].Initialize(_spriteRenderer, Color.red);
      
      //m_MovementProvider = GetComponent<MousePointer>();
      m_MovementProvider = GetComponent<HapticPointer>();
      m_MovementProvider.Initialize();
    }

    private void FixedUpdate()
    {
      transform.position = m_MovementProvider.GetPointerPosition();
    }
  }
}
