using Project.Scripts.Runtime.Angrybird.Presenter.Level;
using Project.Scripts.Runtime.Core.SessionManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Arcade
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private LevelController levelController;
        private UIDocument _uiDocument;
        private VisualElement _root;
        private Button _playButton;
        private int _level;
        void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            _playButton = _root.Q<Button>("Play");
            _playButton.clicked += OnPlayButtonClicked_LoadLevelScene;
        }

        private void OnPlayButtonClicked_LoadLevelScene()
        {
            _level = SessionManager.Instance.Session.UserGameData.LastLevel + 1;
            levelController.LoadLevel();
            SceneManager.LoadSceneAsync(sceneBuildIndex: _level, LoadSceneMode.Single);
        }

        private void OnDisable()
        {
            _playButton.clicked -= OnPlayButtonClicked_LoadLevelScene;
        }
    }
}
