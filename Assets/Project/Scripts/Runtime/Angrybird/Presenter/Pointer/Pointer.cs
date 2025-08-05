using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.View.Pointer;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{

  public abstract class SelectStrategy
  {
    public abstract void Execute();

    public void SetStrategy(SelectStrategy selectStrategy)
    {
      
    }
  }
  
  public class HoverStrategy : SelectStrategy
  {
    private Projectile projectile;
    private Pointer pointer;
    public override void Execute()
    {
      projectile.IsSelected = true;
      projectile.SetStatic();
      projectile.transform.SetParent(pointer.transform);
    }
  }
  public class Pointer : MonoBehaviour
  {
    private PointerBehaviour _behaviour;
    private PointerVisual _visual;
    private Action<Projectile> _selectStrategy;



    private void Awake()
    {
      _behaviour = GetComponent<PointerBehaviour>();
      _visual = GetComponent<PointerVisual>();
      
      _behaviour.OnProjectileOverlap += OnOnProjectileOverlap_SetToPointer;
    }

    private void OnDisable()
    {
      _behaviour.OnProjectileOverlap -= OnOnProjectileOverlap_SetToPointer;
    }

    private void OnOnProjectileOverlap_SetToPointer(object sender, Projectile e)
    {
      e.IsSelected = true;
      e.SetStatic();
      e.transform.SetParent(transform);
    }
  }
}
