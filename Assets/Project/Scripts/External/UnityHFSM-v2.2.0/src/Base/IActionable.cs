
namespace Project.Scripts.External.UnityHFSM_v2._2._0.src.Base
{
	/// <summary>
	/// Interface for states that support custom actions. Actions are like the
	/// builtin events <c>OnEnter</c> / <c>OnLogic</c> / ... but are defined by the user.
	/// </summary>
	public interface IActionable<TEvent>
	{
		void OnAction(TEvent trigger);
		void OnAction<TData>(TEvent trigger, TData data);
	}

	/// <inheritdoc />
	public interface IActionable : IActionable<string>
	{
	}
}
