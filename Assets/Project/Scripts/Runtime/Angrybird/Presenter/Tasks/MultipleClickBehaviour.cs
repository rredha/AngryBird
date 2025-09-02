using System;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class MultipleClickBehaviour : MonoBehaviour
    {
        private int _clickCount;
        private int _threshold;

        private PlayerInputActions _playerInputActions; 
        public event EventHandler TaskComplete;
        public void Initialize()
        {
            _clickCount = 0;
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();
            _playerInputActions.Player.Select.performed += SelectAction_Performed;
        }
        public void Configure(int threshold)
        {
            _threshold = threshold;
        }
        public void Cleanup()
        {
            _playerInputActions.Player.Select.performed -= SelectAction_Performed;
            _playerInputActions.Disable();
        }

        public void Execute()
        {
        }
        void SelectAction_Performed(InputAction.CallbackContext obj)
        {
            _clickCount++;
            Debug.Log(_clickCount);
            if (_clickCount == _threshold)
            {
                TaskComplete?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Disable();
        }
    }
}