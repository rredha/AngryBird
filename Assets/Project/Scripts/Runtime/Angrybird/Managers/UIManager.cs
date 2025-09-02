using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject lostUI;
        [SerializeField] private GameObject wonUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject surveyUI;

        private Dictionary<string, GameObject> _userInterfaces;

        private void Awake()
        {
            _userInterfaces = new Dictionary<string, GameObject>();
            _userInterfaces.Add("Lost", lostUI);
            _userInterfaces.Add("Won", wonUI);
            _userInterfaces.Add("Pause", pauseUI);
            _userInterfaces.Add("Survey", surveyUI);
            DontDestroyOnLoad(gameObject);
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
    }
}