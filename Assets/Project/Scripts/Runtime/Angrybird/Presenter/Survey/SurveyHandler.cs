using System;
using System.Collections.Generic;
using Model.Survey;
using UnityEngine;
using UnityEngine.Serialization;
using View.Survey;

namespace Presenter
{
    public class SurveyHandler : MonoBehaviour
    {
        [SerializeField] private SurveySO surveySO;
        [SerializeField] private SurveyUI surveyUI;

        private SurveyAnswer _surveyAnswer;
        public List<SurveyAnswer> SurveyAnswers;

        private void OnEnable()
        {
            surveyUI.AnswerPicked += SurveyUIOnAnswerPicked_Save;
            surveyUI.ChoiceConfirmed += SurveyUIOnChoiceConfirmed;
        }


        private void Awake()
        {
            surveyUI.Data = surveySO;
            surveyUI.SetQuestion(surveySO.Question);
            surveyUI.SetAnswers(surveySO.Answers);
            SurveyAnswers = new List<SurveyAnswer>();
        }
        private void OnDisable()
        {
            surveyUI.AnswerPicked -= SurveyUIOnAnswerPicked_Save;
            surveyUI.ChoiceConfirmed -= SurveyUIOnChoiceConfirmed;
        }
        private void SurveyUIOnAnswerPicked_Save(object sender, SurveyAnsweredEventArgs e)
        {
            Debug.Log($"{e.SurveyTitle} => {e.Answer}");
            _surveyAnswer = new SurveyAnswer(e.SurveyTitle, e.Answer);
            SurveyAnswers.Add(_surveyAnswer);
        }
        private void SurveyUIOnChoiceConfirmed(object sender, EventArgs e)
        {
            SurveyExport.Export("mySurveyDataSecond", SurveyAnswers);
        }
    }
}