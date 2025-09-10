using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class DurationTracker
    {
        public event EventHandler RecordingStarted;
        public event EventHandler<RecordedDataEventArgs> RecordingStopped;
        public DurationData Data => _data;
        private readonly DurationData _data = new();
        private Stopwatch _timer = new();
        public bool IsRunning => _timer.IsRunning;
        public long Total => _total;
        private long _total;

        public void StartRecording()
        {
            if (_timer.IsRunning)
            {
                Debug.Log("Error occured, timer already running");
                return;
            }

            _data.Clear(); // clear data
            _timer.Start(); // start timer
            OnRecordingStarted(EventArgs.Empty); // invoke event
        }
        public void StopRecording()
        {
            if (!_timer.IsRunning)
                Debug.Log("No timer to stop.");
            _timer.Stop();
            
            OnRecordingStopped(new RecordedDataEventArgs
            {
                Seconds = _timer.Elapsed.Seconds,
                Milliseconds = _timer.Elapsed.Milliseconds
            });
            _timer.Reset();
        }

        public virtual void OnRecordingStarted(EventArgs e)
        {
            RecordingStarted?.Invoke(this, e); 
        }
        public virtual void OnRecordingStopped(RecordedDataEventArgs recordedDataEventArgs)
        {
            _data.Milliseconds = recordedDataEventArgs.Milliseconds;
            _data.Seconds = recordedDataEventArgs.Seconds;
            RecordingStopped?.Invoke(this, recordedDataEventArgs);
        }

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
}