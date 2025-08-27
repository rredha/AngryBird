using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Arcade
{
    public class MainMenu : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private VisualElement _root;
        private Button _playButton;
        void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            _playButton = _root.Q<Button>("Play");
            _playButton.clicked += OnPlayButtonClicked_LoadLevelScene;
        }

        private void OnPlayButtonClicked_LoadLevelScene()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        
        void Update()
        {
        
        }

        private void OnDisable()
        {
            _playButton.clicked -= OnPlayButtonClicked_LoadLevelScene;
        }
    }
}
