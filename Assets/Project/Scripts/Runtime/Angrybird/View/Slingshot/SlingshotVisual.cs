using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Project.Scripts.Runtime.Angrybird.Model.Slingshot;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Runtime.Angrybird.View.Slingshot
{
    public class SlingshotVisual : MonoBehaviour
    {

        public SlingshotContext context { get; set; }
        public PlayerInputActions PlayerInputActions { get; private set; }

        private void Awake()
        {
            PlayerInputActions = new PlayerInputActions();
        }
        public void EnablePlayerActions() => PlayerInputActions.Player.Enable();
        public void InitializeRubber()
        {
            context.Rubber.Init();
            context.Rubber.Reset(context.Rubber.HolderInitialPosition);
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
            context.Rubber.Set(context.PointerWorldPosition);
        }
        private void Release_performed(InputAction.CallbackContext ctx)
        {
            context.Rubber.Animation();
        }

    }
}