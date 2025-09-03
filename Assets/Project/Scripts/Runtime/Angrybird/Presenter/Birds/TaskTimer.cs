using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Debug = UnityEngine.Debug;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class TaskTimerContainer
    {
        public double Total => CalculateTotal(Seconds, Milliseconds);

        public int Seconds { get; set; }
        public double Milliseconds { get; set; }

        public double CalculateTotal(int seconds, double milliseconds)
        {
            return seconds * 1000 + milliseconds;
        }
    }
    public class DroppingTaskTimerData : TaskTimerContainer
    {
        public string TaskName { get; set; } = "Dropping";
        public int Seconds { get; set; }
        public double Milliseconds { get; set; }
    }
    public class PlayingTaskTimerData : TaskTimerContainer
    {
        public string TaskName { get; set; } = "Playing";
        public int Seconds { get; set; }
        public double Milliseconds { get; set; }
    }
    public class AimingTaskTimerData : TaskTimerContainer
    {
        public string TaskName { get; set; } = "Aiming";
        public int Seconds { get; set; }
        public double Milliseconds { get; set; }
    }
    public class TaskTimerCSVData
    {
        public int Level { get; set; }
        public int Attempt { get; set; }
        public List<TaskTimerContainer> TaskTimerContainers { get; set; }
        public List<double> TimersTotal { get; set; }
        
        public double AimingTimer { get; set; }
        public double PlayingTimer { get; set; }
        public double DroppingTimer { get; set; }

        public void Initialize()
        {
            TaskTimerContainers = new List<TaskTimerContainer>
            {
                new DroppingTaskTimerData(),
                new AimingTaskTimerData(),
                new PlayingTaskTimerData()
            };
            TimersTotal = new List<double>
            {
                DroppingTimer,
                AimingTimer,
                PlayingTimer
            };
        }

        public void Serialize()
        {
            for (int i = 0; i < TimersTotal.Count; i++)
            {
                TimersTotal[i] = TaskTimerContainers[i].Total;
            }
        }
    }


    /*
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
    */
    public class TaskTimer
    {
        private readonly Stopwatch _timer = new();
        public long Total => _total;
        private long _total;

        public void Enable(object sender, EventArgs e)
        {
            _timer.Start();
        }
        public void Disable(object sender, EventArgs e)
        {
            _timer.Stop();
            _total = _timer.ElapsedMilliseconds;
            _timer.Reset();
        }
    }
    /*
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
                //var data = new TaskTimerCSVData(i+1, Seconds[i], Milliseconds[i]);
                var data = new TaskTimerCSVData();
                data.Initialize();
                data.Serialize();
                _csvDatas.Add(data);
            }
        }
    }
    */
}