using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace Presenter
{
    public static class SurveyExport
    {
        // TODO : Needs to use csv data by session ID. 
        
        private const string pathRoot = "/home/redha/";
        public static void Export(string outputName, List<SurveyAnswer> answers)
        {
            using (var writer = new StreamWriter(pathRoot+outputName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(answers);
            }
        }
    }
}