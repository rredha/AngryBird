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
      Debug.Log("Hello from empty state enter");
    }
    public void EmptyStateUpdate()
    {
      _visual.InitializeRubber();
    }
    public void EmptyStateExit()
    {
      _behaviour.Pointer.Unsubscribe();
      Debug.Log("Hello from empty state exit");
    }
  }
  
  public partial class Slingshot
  {
    public void LoadedStateEnter()
    {
      _behaviour.EnablePlayerActions();
      _visual.EnablePlayerActions();
      
      _behaviour.Subscribe();
      _visual.Subscribe();
      Debug.Log("Hello from loaded state enter");
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
      Debug.Log("Hello from loaded state exit");
      
    }
  }
}
