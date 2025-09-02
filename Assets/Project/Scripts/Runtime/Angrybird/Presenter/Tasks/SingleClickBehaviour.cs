using System;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class SingleClickBehaviour : MonoBehaviour, ITaskBehaviour
    {
        private bool _clicked;

        private PlayerInputActions _playerInputActions; 
        public event EventHandler TaskComplete;
        public void Initialize()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Select.performed += SelectAction_Performed;
            _playerInputActions.Player.Select.canceled += SelectAction_Canceled;
        }

        public void Execute()
        {
        }
        void SelectAction_Performed(InputAction.CallbackContext obj)
        {
            _clicked = true;
            TaskComplete?.Invoke(this, EventArgs.Empty);
        }
        void SelectAction_Canceled(InputAction.CallbackContext obj)
        {
            _clicked = false;
        }
        public void Cleanup()
        {
            _playerInputActions.Player.Select.performed -= SelectAction_Performed;
            _playerInputActions.Player.Select.canceled -= SelectAction_Canceled;
            _playerInputActions.Disable(); // added execution on OnDisable
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Disable();
        }
    }
}