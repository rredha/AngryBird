using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Survey
{
    [CreateAssetMenu(fileName = "Survey", menuName = "Survey")]
    public class SurveySO : ScriptableObject
    {
        [SerializeField, DontCreateProperty] private string _title;
        [CreateProperty] public string Title => _title;
        
        [SerializeField, DontCreateProperty]
        private string _question;
        
        [SerializeField, DontCreateProperty]
        private List<string> _answers;

        [CreateProperty]
        public string Question => _question;

        [CreateProperty]
        public List<string> Answers => _answers;

    }
}