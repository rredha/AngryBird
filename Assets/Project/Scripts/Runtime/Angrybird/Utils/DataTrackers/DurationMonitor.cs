using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class DurationMonitor
    {
        private DurationTracker _durationTracker;
        private string _description;

        public DurationMonitor(DurationTracker durationTracker, string description)
        {
            _durationTracker = durationTracker;
            _description = description;
        }

        public void Subscribe()
        {
            if (_durationTracker.IsRunning) return;
            _durationTracker.RecordingStarted += OnRecordingStarted;
            _durationTracker.RecordingStopped += OnRecordingStopped;
        }
        public void Unsubscribe()
        {
            _durationTracker.RecordingStarted -= OnRecordingStarted;
            _durationTracker.RecordingStopped -= OnRecordingStopped;
        }
        
        private void OnRecordingStarted(object sender, EventArgs e)
        {
            Debug.Log($"Recording {_description} started");
        }
        private void OnRecordingStopped(object sender, RecordedDataEventArgs e)
        {
            Debug.Log($"Recording {_description} stopped, Total elapsed time {_durationTracker.Data.Total} ms");
        }
    }
}