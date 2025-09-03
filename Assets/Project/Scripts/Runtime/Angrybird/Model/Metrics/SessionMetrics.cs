using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Managers
{
    public class SessionMetrics
    {
        public int Level { get; set; }
        public int Attempt { get; set; }
        public long PlayingTime { get; set; }
        public long DroppingTime { get; set; }
        public long AimingTime { get; set; }

        public SessionMetrics(int level, int attempt, long playingTime, long droppingTime, long aimingTime)
        {
            Level = level;
            Attempt = attempt;
            PlayingTime = playingTime;
            DroppingTime = droppingTime;
            AimingTime = aimingTime;
        }

        public void Log()
        {
            Debug.Log($"Level {Level}," +
                      $" Attempt {Attempt}," +
                      $" Playing Time {PlayingTime}," +
                      $" Dropping Time {DroppingTime}" +
                      $" Aiming Time {AimingTime}");
        }
    }
}