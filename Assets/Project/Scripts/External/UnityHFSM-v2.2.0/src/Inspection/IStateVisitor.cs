using Project.Scripts.External.UnityHFSM_v2._2._0.src.Base;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;

namespace Project.Scripts.External.UnityHFSM_v2._2._0.src.Inspection
{
	/// <summary>
	/// Defines the interface for a visitor that can perform operations on different states
	/// of the state machine. This is part of the Visitor Pattern, which allows new behavior
	/// to be added to existing state classes without modifying their code. It is used to
	/// implement dynamic inspection tools for hierarchical state machines.
	/// </summary>
	public interface IStateVisitor
	{
		void VisitStateMachine<TOwnId, TStateId, TEvent>(StateMachine<TOwnId, TStateId, TEvent> fsm);

		void VisitRegularState<TStateId>(StateBase<TStateId> state);
	}
}
