using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Model.Survey
{
    [CreateAssetMenu(fileName = "Survey", menuName = "Survey")]
    public class SurveySO : ScriptableObject
    {
        [SerializeField, DontCreateProperty] private string _title;
        [CreateProperty] public string Title => _title;
        
        [SerializeField, DontCreateProperty]
        private string m_Question;
        
        [SerializeField, DontCreateProperty]
        private List<string> m_Answers;

        [CreateProperty]
        public string Question => m_Question;

        [CreateProperty]
        public List<string> Answers => m_Answers;

    }
}