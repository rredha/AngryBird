using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Model.Survey;
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
        private Label _questionText;
        private VisualElement _buttonContainer;
        private static List<Button> _buttons = new();
        
        public SurveySO Data { get; set; }
        public event EventHandler<SurveyAnsweredEventArgs> AnswerPicked;
        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _buttonContainer = _root.Query<VisualElement>("ButtonContainer");
            _questionText = _root.Query<Label>("QuestionText");
        }
        private void OnEnable()
        {
            SetQuestion(Data.Question);
            Populate(Data.Answers);
        }

        private void SetQuestion(string question)
        {
            _questionText.text = question;
        }
        
        private void Populate(List<string> answers)
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
            foreach (var button in _buttons)
            {
                button.clickable.clickedWithEventInfo -= OnClickedWithEventInfo;
            }
        }
    }
}