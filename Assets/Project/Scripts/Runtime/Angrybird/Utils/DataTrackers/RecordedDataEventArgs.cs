using System;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class RecordedDataEventArgs : EventArgs
    {
        public int Seconds { get; set; }
        public double Milliseconds { get; set; }
    }
}