using System;
using UnityEngine;

namespace Arcade.Project.Runtime.Games.AngryBird
{
  public class Pointer : MonoBehaviour
  {
    private PointerBehaviour _behaviour;
    private PointerVisual _visual;

    private void Awake()
    {
      _behaviour = GetComponent<PointerBehaviour>();
      _behaviour.OnProjectileOverlap += OnOnProjectileOverlap_SetToPointer;
      _visual = GetComponent<PointerVisual>();
    }

    private void OnDisable()
    {
      _behaviour.OnProjectileOverlap -= OnOnProjectileOverlap_SetToPointer;
    }

    private void OnOnProjectileOverlap_SetToPointer(object sender, Projectile e)
    {
      e.SetStatic();
      e.transform.SetParent(transform);
    }
  }
}
