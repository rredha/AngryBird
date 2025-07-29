using UnityEngine;

namespace Project.Scripts.External.UnityHFSM_v2._2._0.src.Util
{
	/// <summary>
	/// Default timer that calculates the elapsed time based on <c>Time.time</c>.
	/// </summary>
	public class Timer : ITimer
	{
		public float startTime;
		public float Elapsed => Time.time - startTime;

		public void Reset()
		{
			startTime = Time.time;
		}

		public static bool operator >(Timer timer, float duration)
			=> timer.Elapsed > duration;

		public static bool operator <(Timer timer, float duration)
			=> timer.Elapsed < duration;

		public static bool operator >=(Timer timer, float duration)
			=> timer.Elapsed >= duration;

		public static bool operator <=(Timer timer, float duration)
			=> timer.Elapsed <= duration;
	}
}
