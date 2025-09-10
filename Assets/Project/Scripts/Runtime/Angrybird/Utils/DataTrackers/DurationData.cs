namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class DurationData
    {
        public double Total => CalculateTotal(Seconds, Milliseconds);
        public int Seconds { get; set; }
        public double Milliseconds { get; set; }

        private double CalculateTotal(int seconds, double milliseconds)
        {
            return seconds * 1000 + milliseconds;
        }
        public void Clear()
        {
            Seconds = 0;
            Milliseconds = 0;
        }
    }
}