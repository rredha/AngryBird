using System;
using System.Reflection;
using Project.Scripts.Runtime.Angrybird.Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.View.UI
{
    public class LostUI : MonoBehaviour
    {
        private Button _replayBtn;
        private Button _quitBtn;
        public event EventHandler ReplayTriggered;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            _replayBtn = uiDocument.rootVisualElement.Q("ReplayBtn") as Button;
            _quitBtn = uiDocument.rootVisualElement.Q("QuitBtn") as Button;

            _replayBtn?.RegisterCallback<ClickEvent>(OnReplayClicked_TriggerReplay);
            _replayBtn?.RegisterCallback<ClickEvent>(OnReplayClicked_DisableUI);
            
            _quitBtn?.RegisterCallback<ClickEvent>(OnQuitClicked_AppQuit);
        }
        private void OnDisable()
        {
            _replayBtn.UnregisterCallback<ClickEvent>(OnReplayClicked_TriggerReplay);
            _replayBtn?.UnregisterCallback<ClickEvent>(OnReplayClicked_DisableUI);
            _quitBtn?.UnregisterCallback<ClickEvent>(OnQuitClicked_AppQuit);
        }

        #region Events
        private void OnQuitClicked_AppQuit(ClickEvent evt) => QuitGame();
        private void OnReplayClicked_DisableUI(ClickEvent evt) => UIManager.Instance.Hide("Lost");
        private void OnReplayClicked_TriggerReplay(ClickEvent evt) => ReplayTriggered?.Invoke(this, EventArgs.Empty);

        #endregion

        #region Methods

        private void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
        }
        

        #endregion
    }
}