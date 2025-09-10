using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Managers
{
    public class SessionMetrics
    {
        public int Level { get; set; }
        public int Attempt { get; set; }
        public double PlayingTime { get; set; }
        public double SelectingTime { get; set; }
        public double DroppingTime { get; set; }
        public double AimingTime { get; set; }

        public SessionMetrics(int level, int attempt,
            double playingTime,
            double selectingTime, double droppingTime, double aimingTime)
        {
            Level = level;
            Attempt = attempt;
            PlayingTime = playingTime;
            SelectingTime = selectingTime;
            DroppingTime = droppingTime;
            AimingTime = aimingTime;
        }

        public void Log()
        {
            Debug.Log($"Level {Level}," +
                      $" Attempt {Attempt}," +
                      $" Playing Time {PlayingTime}," +
                      $" Selecting Time {SelectingTime}," +
                      $" Dropping Time {DroppingTime}" +
                      $" Aiming Time {AimingTime}");
        }
    }
}