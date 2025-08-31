using System.Globalization;
using System.IO;
using CsvHelper;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;

namespace Project.Scripts.Runtime.Angrybird.Managers
{
    public static class TaskTimerCSV
    {
        private const string pathRoot = "/home/redha/";
        public static void Export(string outputName, TaskTimer taskTimer)
        {
            taskTimer.SerializeData();
            using (var writer = new StreamWriter(pathRoot+outputName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(taskTimer.CsvData);
            }
        }
    
    }
}