using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Model.Survey;
using UnityEngine;
using View.Survey;

namespace Presenter
{
    public class SurveyCSV
    {
        public string Answer { get; set; }
        public string SurveyTitle { get; set; }

        public SurveyCSV(string surveyTitle, string answer)
        {
            SurveyTitle = surveyTitle;
            Answer = answer;
        }
    }
    public static class SurveyDataCSV
    {
        private const string pathRoot = "/home/redha/";
        public static void Export(string outputName, Survey survey)
        {
            survey.SerializeData();
            using (var writer = new StreamWriter(pathRoot+outputName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(survey.SurveyCSVData);
            }
        }
    }
    public class Survey : MonoBehaviour
    {
        [SerializeField] private SurveySO _surveySO;
        [SerializeField] private SurveyUI _surveyUI;

        private SurveyCSV _surveyCsv;
        public List<SurveyCSV> SurveyCSVData;
        public SurveyCSV SurveyCsv => _surveyCsv;
        private void OnEnable()
        {
            _surveyUI.AnswerPicked += SurveyUIOnAnswerPicked_Save;
        }
        private void OnDisable()
        {
            _surveyUI.AnswerPicked -= SurveyUIOnAnswerPicked_Save;
        }

        private void SurveyUIOnAnswerPicked_Save(object sender, SurveyAnsweredEventArgs e)
        {
            Debug.Log($"{e.SurveyTitle} => {e.Answer}");
            _surveyCsv = new SurveyCSV(e.SurveyTitle, e.Answer);
            SurveyCSVData.Add(_surveyCsv);
            SurveyDataCSV.Export("surveyData", this);
        }

        private void Awake()
        {
            _surveyUI.Data = _surveySO;
            SurveyCSVData = new();
        }

        public void SerializeData()
        {
            
        }
    }
}