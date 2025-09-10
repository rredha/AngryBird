using System;
using System.Collections.Generic;
using System.Linq;
using Model.Survey;
using Presenter;
using UnityEngine;
using UnityEngine.UIElements;

namespace View.Survey
{
    public class SurveyAnsweredEventArgs : EventArgs
    {
        public string Answer { get; private set; }
        public string SurveyTitle { get; private set; }

        public SurveyAnsweredEventArgs(string surveyTitle, string answer)
        {
            SurveyTitle = surveyTitle;
            Answer = answer;
        }
    }
    public class SurveyUI : MonoBehaviour
    {
        private VisualElement _root;
        private VisualElement _buttonContainer;
        private Label _questionText;
        private Button _exitButton;
        private Button _confirmButton;
        
        private static List<Button> _buttons = new();
        public SurveySO Data { get; set; }
        public event EventHandler<SurveyAnsweredEventArgs> AnswerPicked;
        public event EventHandler ChoiceConfirmed;
        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _buttonContainer = _root.Query<VisualElement>("ButtonContainer");
            _questionText = _root.Query<Label>("QuestionText");
            _exitButton = _root.Query<Button>("ExitButton");
            _confirmButton = _root.Query<Button>("ConfirmButton");
        }
        private void OnEnable()
        {
            _exitButton.clickable.clicked += OnExitClicked_Hide;
            _confirmButton.clickable.clicked += OnChoiceConfirmed;
        }

        public void SetQuestion(string question)
        {
            _questionText.text = question;
        }
        
        public void SetAnswers(List<string> answers)
        {
            foreach (var button in answers.Select((value, index) => new Button()
                     {
                         name = "button" + index,
                         text = value
                     }))
            {
                _buttonContainer.Add(button);
                _buttons.Add(button);
                
                button.clickable.clickedWithEventInfo += OnClickedWithEventInfo;
            }
        }
        private void OnClickedWithEventInfo(EventBase obj)
        {
            var clickedButton = (Button)obj.target;
            var userAnswer = clickedButton.text;
            AnswerPicked?.Invoke(this, new SurveyAnsweredEventArgs(Data.Title, userAnswer));
        }

        private void OnDisable()
        {
            _exitButton.clickable.clicked -= OnExitClicked_Hide;
            _confirmButton.clickable.clicked -= OnChoiceConfirmed;
            foreach (var button in _buttons)
            {
                button.clickable.clickedWithEventInfo -= OnClickedWithEventInfo;
            }
        }
        private void OnExitClicked_Hide()
        {
            gameObject.SetActive(false);
        }
        protected virtual void OnChoiceConfirmed()
        {
            ChoiceConfirmed?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }
}