using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class ClickTaskVisual : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        private VisualElement _root;
        private Label _counterLabel;


        public void Initialize()
        {
            _root = uiDocument.rootVisualElement;
            _counterLabel = _root.Q<Label>();
        }
        public void Hide()
        {
           gameObject.SetActive(false); 
        }
        public void SetCounterLabel(int clicksLeft)
        {
            _counterLabel.text = clicksLeft.ToString();
        }
    }
}