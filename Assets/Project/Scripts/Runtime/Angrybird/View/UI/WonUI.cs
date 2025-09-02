using System;
using Project.Scripts.Runtime.Angrybird.Managers;
using Project.Scripts.Runtime.Angrybird.Presenter.Level;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.View.UI
{
    public class WonUI : MonoBehaviour
    {
        private Button _replayButton;
        private Button _nextLevelButton;
        private Button _quitButton;
        public event EventHandler ReplayTriggered;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            _replayButton = uiDocument.rootVisualElement.Q("ReplayButton") as Button;
            _nextLevelButton = uiDocument.rootVisualElement.Q("NextLevelButton") as Button;
            _quitButton = uiDocument.rootVisualElement.Q("QuitButton") as Button;

            _replayButton?.RegisterCallback<ClickEvent>(OnReplayClicked_TriggerReplay);
            _nextLevelButton?.RegisterCallback<ClickEvent>(OnNextLevelClicked_LoadLevel);
            _quitButton?.RegisterCallback<ClickEvent>(OnQuitClicked_AppQuit);
        }
        private void OnDisable()
        {
            _replayButton.UnregisterCallback<ClickEvent>(OnReplayClicked_TriggerReplay);
            _nextLevelButton?.UnregisterCallback<ClickEvent>(OnNextLevelClicked_LoadLevel);
            _quitButton?.UnregisterCallback<ClickEvent>(OnQuitClicked_AppQuit);
        }

        #region Events
        private void OnQuitClicked_AppQuit(ClickEvent evt) => QuitGame();
        private void OnNextLevelClicked_LoadLevel(ClickEvent evt)
        {
            UIManager.Instance.Hide("Won");
            var nextLevel = LevelManager.Instance.CurrentLevel + 2;
            SceneManager.LoadScene(sceneBuildIndex:nextLevel, LoadSceneMode.Single);
        }

        private void OnReplayClicked_TriggerReplay(ClickEvent evt)
        {
            UIManager.Instance.Hide("Won");
            ReplayTriggered?.Invoke(this, EventArgs.Empty);
        }

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