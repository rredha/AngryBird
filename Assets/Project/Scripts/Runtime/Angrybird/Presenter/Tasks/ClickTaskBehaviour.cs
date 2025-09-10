using System;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = System.Object;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class ClickTaskBehaviour : MonoBehaviour, ITaskBehaviour
    {
        private int _clickCountLeft;
        private ClickTaskVisual _visual;
        public int Threshold { get; set; }
        
        private PlayerInputActions _playerInputActions;
        public bool IsTaskStarted { get; set; }
        private bool _isActive;
        public event EventHandler TaskComplete;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _visual = GetComponent<ClickTaskVisual>();
            _clickCountLeft = 3;
        }
        public void Initialize()
        {
            _playerInputActions.Enable();
            // TODO 
            //  expected : execute once only 
            //  bug : keeps executing at every frame.
            //Debug.Log($"Task initialized.");
            
            // kind of a fix
            if (IsTaskStarted)
            {
                _visual.Initialize();
                _visual.SetCounterLabel(_clickCountLeft);
                _playerInputActions.Player.Select.performed += SelectAction_Performed;
                IsTaskStarted = false;
            }
                
        }

        void SelectAction_Performed(InputAction.CallbackContext obj)
        {
            _clickCountLeft--;
            _visual.SetCounterLabel(_clickCountLeft);
            if (_clickCountLeft <= 0)
            {
                _visual.Hide();
                TaskComplete?.Invoke(this,EventArgs.Empty);
            }
        }
        void SelectAction_Canceled(InputAction.CallbackContext obj)
        {
        }
        public void Cleanup()
        {
            _isActive = false;
            _playerInputActions.Player.Select.performed -= SelectAction_Performed;
            _playerInputActions.Disable(); // added execution on OnDisable

            _clickCountLeft = Threshold;
        }
    }
}