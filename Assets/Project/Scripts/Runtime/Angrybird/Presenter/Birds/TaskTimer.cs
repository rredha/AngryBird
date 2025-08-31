using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Debug = UnityEngine.Debug;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class TaskTimerCSVData
    {
        public string TaskName;

        public int Attempt { get; set; }
        public int Seconds { get; set; }
        public double Milliseconds { get; set; }

        public TaskTimerCSVData(int attempt, int seconds, double milliseconds)
        {
            Attempt = attempt;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }
    }
    public class TaskTimer
    {
        private readonly Stopwatch _timer = new();
        public List<TimeSpan> TimeSpanData => timeSpanData;
        private List<TimeSpan> timeSpanData = new();

        public List<int> Seconds = new();
        public List<double> Milliseconds = new();

        public void Enable(object sender, EventArgs e)
        {
            _timer.Start();
        }
        public void Disable(object sender, EventArgs e)
        {
            _timer.Stop();
            Register();
            _timer.Reset();
        }

        public void Log(string taskName)
        {
            foreach (var timeSpan in TimeSpanData)
            {
                Debug.Log($"{taskName} took {timeSpan.Seconds}:{timeSpan.Milliseconds}" );
            }
        }
        public void Log(string taskName, bool ignoreLastEntry)
        {
            if (!ignoreLastEntry) return;
            for (var i = 0; i < TimeSpanData.Count - 1; i++)
            {
                Debug.Log($"{taskName} took {TimeSpanData[i].Seconds}:{TimeSpanData[i].Milliseconds}" );
            }
        }

        private void Register()
        {
            timeSpanData.Add(_timer.Elapsed);
        }

        private void GetSeconds()
        {
            foreach (var timeSpan in TimeSpanData)
            {
                Seconds.Add(timeSpan.Seconds);
            }
        }
        private void GetMilliseconds()
        {
            foreach (var timeSpan in TimeSpanData)
            {
                Milliseconds.Add(timeSpan.Milliseconds);
            }
        }

        public List<TaskTimerCSVData> CsvData => _csvDatas;
        private List<TaskTimerCSVData> _csvDatas = new();
        public void SerializeData()
        {
            GetSeconds();
            GetMilliseconds();

            for (int i = 0; i < TimeSpanData.Count; i++)
            {
                var data = new TaskTimerCSVData(i+1, Seconds[i], Milliseconds[i]);
                _csvDatas.Add(data);
            }
        }
    }
}