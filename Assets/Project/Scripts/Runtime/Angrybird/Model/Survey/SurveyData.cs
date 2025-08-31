using System.Collections.Generic;
using UnityEngine;

namespace Model.Survey
{
    public class SurveyData
    {
        public string Question => _question;
        public List<string> Answers => _answers;
        
        private string _question;
        private List<string> _answers;

        public SurveyData(string question, List<string> answers)
        {
            _question = question;
            _answers = answers;
        }

        public void Log()
        {
           Debug.Log($"{Question}");
           foreach (var answer in Answers)
           {
               Debug.Log($"{answer}"); 
           }
        }
    }
}