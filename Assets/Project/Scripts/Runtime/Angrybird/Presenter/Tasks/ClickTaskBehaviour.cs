using System;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = System.Object;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class ClickTaskBehaviour : ITaskBehaviour
    {
        private int _clickCount;
        public int Threshold { get; set; }
        
        private PlayerInputActions _playerInputActions; 
        public event EventHandler TaskComplete;

        public ClickTaskBehaviour(int threshold)
        {
            Threshold = threshold;
            
            _playerInputActions = new PlayerInputActions();
        }
        public void Initialize()
        {
            _playerInputActions.Enable();
            _playerInputActions.Player.Select.performed += SelectAction_Performed;
            // TODO 
            //  expected : execute once only 
            //  bug : keeps executing at every frame.
            //Debug.Log($"Task initialized.");
        }

        void SelectAction_Performed(InputAction.CallbackContext obj)
        {
            _clickCount++;
            Debug.Log(_clickCount);
            if (_clickCount >= Threshold)
                TaskComplete?.Invoke(this,EventArgs.Empty);
        }
        void SelectAction_Canceled(InputAction.CallbackContext obj)
        {
        }
        public void Cleanup()
        {
            _playerInputActions.Player.Select.performed -= SelectAction_Performed;
            _playerInputActions.Disable(); // added execution on OnDisable
            
            _clickCount = 0;
        }
    }
}