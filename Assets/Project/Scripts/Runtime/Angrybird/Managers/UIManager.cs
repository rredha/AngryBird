using System.Collections.Generic;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.View.UI;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] private GameObject lostUI;
        [SerializeField] private GameObject wonUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject surveyUI;

        public WonUI WonUI;
        // TODO:
        // fix issues with lostui, in prefab.
        // create parent prefab and generate prefab variants.
        public LostUI LostUI;

        private static Dictionary<string, GameObject> _userInterfaces;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            
            WonUI = wonUI.GetComponent<WonUI>();
            LostUI = lostUI.GetComponent<LostUI>();
            
            _userInterfaces = new Dictionary<string, GameObject>();
            _userInterfaces.Add("Lost", lostUI);
            _userInterfaces.Add("Won", wonUI);
            _userInterfaces.Add("Pause", pauseUI);
            _userInterfaces.Add("Survey", surveyUI);
            Initialize();
      
        }

        private void Initialize()
        {
            foreach (var userInterface in _userInterfaces)
            {
                userInterface.Value.SetActive(false);
            }
        }

        public void Show(string key)
        {
            _userInterfaces[key].SetActive(true);
        }
        public void Hide(string key)
        {
            _userInterfaces[key].SetActive(false);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Show("Pause");
                PauseGame();
                Show("Survey");
            }
        }

        private void PauseGame()
        {
            Time.timeScale= 0f;
        }
    }
}