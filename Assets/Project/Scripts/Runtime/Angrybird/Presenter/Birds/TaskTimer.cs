using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Debug = UnityEngine.Debug;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class TaskTimer
    {
        private readonly Stopwatch _timer = new();
        public TimeSpan TimeSpan { get; private set; }
        public List<TimeSpan> TimeSpanData => timeSpanData;
        private List<TimeSpan> timeSpanData = new();

        public void Enable(object sender, EventArgs e)
        {
            _timer.Start();
        }
        public  void Disable(object sender, EventArgs e)
        {
            _timer.Stop();
            Register();
            //TimeSpan = _timer.Elapsed;
            _timer.Reset();
        }

        public void Log(string taskName)
        {
            foreach (var timeSpan in TimeSpanData)
            {
                Debug.Log($"{taskName} took {timeSpan.Seconds}:{timeSpan.Milliseconds}" );
            }
        }

        private void Register()
        {
            timeSpanData.Add(_timer.Elapsed);
        }
    }
}