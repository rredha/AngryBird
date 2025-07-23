using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Arcade.Project.Runtime.Games.AngryBird.Utils;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;

namespace Arcade.Project.Runtime.Games.AngryBird
{
  public partial class Slingshot : MonoBehaviour
  {
    [SerializeField] private GameObject m_Holder;
    [SerializeField] private DropZone m_DropZone;
    [SerializeField] private MousePointer m_Pointer;

    private Rubber m_Rubber;
    private Projectile m_Projectile;

    private PlayerInputActions _playerInputActions;
    
    private Camera _camera;
    private Vector3 _pointerWorldPosition;
    private Vector2 _pointerScreenPosition;
    public bool ReleasedTriggered { get; private set; }
    public bool IsOverlapping => m_DropZone.IsOverlapping;
    public bool ProjectileIsFlying => m_Projectile.IsFlying;

    private void Awake()
    {
      _camera = Camera.main;
      m_Rubber = GetComponent<Rubber>();
      _playerInputActions = new PlayerInputActions();
    }

    #region Utility Methods
    private Vector3 ScreenToWorldPosition(Vector2 screenPosition)
    {
      var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
      return new Vector3(worldPosition.x, worldPosition.y, 0);
    }
    #endregion
  }

  public partial class Slingshot
  {
    public void EmptyStateEnter()
    {
      m_Pointer.Initialize();
      m_Pointer.Subscribe();
    }
    public void EmptyStateUpdate()
    {
      InitializeRubber();
    }
    private void InitializeRubber()
    {
      m_Rubber.Init();
      m_Rubber.Reset(m_Rubber.HolderInitialPosition);
    }
  }
  
  public partial class Slingshot
  {
    public void LoadedStateEnter()
    {
      _playerInputActions.Player.Enable();
      m_Pointer.Unsubscribe();

      _playerInputActions.Player.Release.performed += Release_performed;
      _playerInputActions.Player.Move.performed += Move_performed;
    }
    public void LoadedStateUpdate()
    {
      SetProjectileToHolder();
    }
    public void LoadedStateExit()
    {
      _playerInputActions.Player.Release.performed -= Release_performed;
      _playerInputActions.Player.Move.performed -= Move_performed;
      ReleasedTriggered = false;
    }
    
    private void SetProjectileToHolder()
    {
      m_Projectile = m_DropZone.Projectile;
      m_Projectile.transform.SetParent(m_Holder.transform);
      m_Projectile.transform.position = m_Holder.transform.position;
    }
    private void Move_performed(InputAction.CallbackContext ctx)
    {
      _pointerWorldPosition = ScreenToWorldPosition(ctx.ReadValue<Vector2>());
      m_Rubber.Set(_pointerWorldPosition);
    }
    private void Release_performed(InputAction.CallbackContext ctx)
    {
      Shoot();
      StartCoroutine(SwitchStateAfter(2f));
    }
    private void Shoot()
    {
      m_Projectile.transform.SetParent(null);
      m_Projectile.SetDynamic();
      m_Projectile.Rb.velocity = (_pointerWorldPosition - m_Rubber.Center.position) * m_Rubber.Config.force * -1;
      m_Rubber.Animation();
    }

    private IEnumerator SwitchStateAfter(float ms)
    {
      m_Projectile.IsFlying = true;
      ReleasedTriggered = true;
      
      yield return new WaitForSeconds(ms);
    }
  }
}
