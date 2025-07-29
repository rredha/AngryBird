using System.Collections;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Project.Scripts.Runtime.Angrybird.Model.Slingshot;
using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Slingshot
{
    public class SlingshotBehaviour : MonoBehaviour
    {
        public SlingshotContext context { get; set; }
        public MousePointer Pointer { get; private set; }
        private Vector2 _pointerScreenPosition;
        private Projectile _projectile;
        private PlayerInputActions PlayerInputActions { get; set; }

        private void Awake()
        {
            Pointer = new MousePointer(Camera.main);
            PlayerInputActions = new PlayerInputActions();
        }

        public void EnablePlayerActions() => PlayerInputActions.Player.Enable();
        public void SetProjectileToHolder()
        {
            _projectile = context.DropZone._projectile;
            _projectile.transform.SetParent(context.Holder.transform);
            _projectile.transform.position = context.Holder.transform.position;
        }

        private void Shoot()
        {
            _projectile.transform.SetParent(null);
            _projectile.SetDynamic();
            _projectile.IsThrown = true;
            _projectile.Rb.velocity = (context.PointerWorldPosition - context.Rubber.Center.position) * context.Rubber.Config.force * -1;
        }
        public void Subscribe()
        {
            PlayerInputActions.Player.Move.performed += Move_performed;
            PlayerInputActions.Player.Release.performed += Release_performed;
        }
        public void Unsubscribe()
        {
            PlayerInputActions.Player.Move.performed -= Move_performed;
            PlayerInputActions.Player.Release.performed -= Release_performed;
        }
        private void Move_performed(InputAction.CallbackContext ctx)
        {
            context.PointerWorldPosition = ScreenToWorldPosition(ctx.ReadValue<Vector2>());
            context.Holder.transform.position = context.PointerWorldPosition;
        }

        private void Release_performed(InputAction.CallbackContext ctx)
        {
            Shoot();
            StartCoroutine(SwitchStateAfter(2f));
        }

        private IEnumerator SwitchStateAfter(float ms)
        {
            context.ReleasedTriggered = true;
            yield return new WaitForSeconds(ms);
        }
        private Vector3 ScreenToWorldPosition(Vector2 screenPosition)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            return new Vector3(worldPosition.x, worldPosition.y, 0);
        } 
    }
}