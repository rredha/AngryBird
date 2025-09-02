using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Runtime.Angrybird.MovementProvider
{
    public class MousePointer : IMovementProvider
    {
        private PlayerInputActions m_playerInputActions;
        public Vector3 PointerWorldPosition { get; private set; }
        public bool SelectEventRaised { get; set; }
        public bool MoveEventRaised { get; private set; }

        public void Initialize()
        {
            m_playerInputActions = new PlayerInputActions();
            m_playerInputActions.Player.Enable();
        }
        ~MousePointer()
        {
            m_playerInputActions.Player.Disable();
        }
        public void Subscribe()
        {
            m_playerInputActions.Player.Move.performed += Move_performed;
            m_playerInputActions.Player.Select.performed += Select_performed;
        }
        public void Unsubscribe()
        {
            m_playerInputActions.Player.Move.performed -= Move_performed;
            m_playerInputActions.Player.Select.performed -= Select_performed;
            m_playerInputActions.Player.Disable();
        }
        private void Select_performed(InputAction.CallbackContext ctx)
        {
            SelectEventRaised = true;
        }
        private void Move_performed(InputAction.CallbackContext ctx)
        {
            MoveEventRaised = true;
            // convert form screen to world position.
            PointerWorldPosition = ScreenToWorldPosition(ctx.ReadValue<Vector2>());
        }
        private Vector3 ScreenToWorldPosition(Vector2 screenPosition)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            return new Vector3(worldPosition.x, worldPosition.y, 5);
        }
    }
}