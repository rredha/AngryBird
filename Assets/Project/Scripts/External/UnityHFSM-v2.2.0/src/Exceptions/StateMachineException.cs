using System;

namespace Project.Scripts.External.UnityHFSM_v2._2._0.src.Exceptions
{
	[Serializable]
	public class StateMachineException : Exception
	{
		public StateMachineException(string message) : base(message) { }
	}
}
