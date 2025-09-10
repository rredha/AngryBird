namespace Presenter
{
    public class SurveyAnswer
    {
        public string Answer { get; set; }
        public string SurveyTitle { get; set; }

        public SurveyAnswer(string surveyTitle, string answer)
        {
            SurveyTitle = surveyTitle;
            Answer = answer;
        }
    }
}