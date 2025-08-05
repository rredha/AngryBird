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
    [SerializeField] private SelectStrategyBase selectStrategy;
    private PointerBehaviour _behaviour;
    private PointerVisual _visual;
    private Action<Projectile> _selectStrategy;




    private void Awake()
    {
      _behaviour = GetComponent<PointerBehaviour>();
      selectStrategy.MovementProvider = _behaviour.MouseMovement;
      _behaviour.OnProjectileOverlap += selectStrategy.Select;
      _visual = GetComponent<PointerVisual>();
    }

    private void OnDisable()
    {
      _behaviour.OnProjectileOverlap -= selectStrategy.Select;
    }
  }
}
