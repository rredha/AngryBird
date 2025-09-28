using System;
using System.Collections.Generic;
using Model.Survey;
using UnityEngine;
using View.Survey;

namespace Arcade
{
    public class SurveySUS : MonoBehaviour
    {
        /* TODO:
         * need major refactoring later.
         */
        [SerializeField] private List<SurveySO> _surveys;
        [SerializeField] private SurveySUSUI ui;

        [SerializeField] private bool logEnabled;

        private List<SurveyData> _surveyStatus = new();
        private Dictionary<string, string> _results = new();
        Dictionary<string, int> _scoreTable;
        private float _finalScore;

        private int _currentSurveyIndex;

        private void ConstructSurveysStatus(List<SurveySO> surveys)
        {
            for (int i = 0; i < surveys.Count; i++)
            {
                var status = new SurveyData()
                {
                    Id = i + 1,
                    IsAnswered = false
                };
                _surveyStatus.Add(status);
            }
        }
        private void Awake()
        {
            ConstructSurveysStatus(_surveys);
            ui.Data = _surveys[_currentSurveyIndex];
            DisplaySurvey(_currentSurveyIndex);
        }
        private void DisplaySurvey(int index)
        {
            ui.ChoiceConfirmed += UiOnChoiceConfirmed;
            ui.AnswerPicked += UiOnAnswerPicked;
            
            ui.Data = _surveys[index];

            ui.SetQuestion(ui.Data.Question);
            ui.SetAnswers(ui.Data.Answers);
        }
        private void UiOnChoiceConfirmed(object sender, EventArgs e)
        {
            ui.CloseCommand -= ui.CloseWindow;
            ui.AnswerPicked -= UiOnAnswerPicked;
            ui.ChoiceConfirmed -= UiOnChoiceConfirmed;
            ui.Clean();
            _surveyStatus[_currentSurveyIndex].IsAnswered = true;
            _currentSurveyIndex++;
            if (_currentSurveyIndex == _surveys.Count)
            {
                ui.CloseCommand += ui.CloseWindow;
                ui.CloseCommandInvoke();
                Report();
                _currentSurveyIndex = 0;
                _results.Clear();
                
            }
            else
            {
                if (_surveyStatus[_currentSurveyIndex].IsAnswered == false)
                {
                    DisplaySurvey(_currentSurveyIndex);
                }
            }
        }
        private void Report()
        {
            PopulateScoreTable(_results);
            CalculateScore();
            Debug.Log(_finalScore);
        }
        private void PopulateScoreTable(Dictionary<string, string> results)
        {
            _scoreTable = new Dictionary<string, int>();
            foreach (var kvp in results)
            {
               _scoreTable.Add(kvp.Key, ConvertAnswerToScore(kvp.Value)); 
            }
        }
        private void UiOnAnswerPicked(object sender, SurveyAnsweredEventArgs e)
        {
            _results.Add(e.SurveyTitle, e.Answer);
        }
        private int ConvertAnswerToScore(string answer)
        {
            return answer switch
            {
                "Strongly Disagree" => 1,
                "Disagree" => 2,
                "Neutral" => 3,
                "Agree" => 4,
                "Strongly Agree" => 5,
                _ => 0
            };
        }
        private void CalculateScore()
        {
            float positiveScore = 0;
            float negativeScore = 0;
            for (var i = 1; i <= 9; i+=2)
            {
                positiveScore += _scoreTable[$"Q{i}"];
            }
            for (var i = 2; i <= 10; i+=2)
            {
                negativeScore += _scoreTable[$"Q{i}"];
            }

            _finalScore =  2.5f * (20 + positiveScore - negativeScore);
        }
    }
}
