using Project.Scripts.External.UnityHFSM_v2._2._0.src.Base;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;

namespace Project.Scripts.External.UnityHFSM_v2._2._0.src.Inspection
{
	/// <summary>
	/// Interface for objects that recursively traverse the states of a state machine
	/// via a <see cref="StateMachineWalker"/>.
	/// </summary>
	public interface IStateMachineHierarchyVisitor
	{
		void VisitStateMachine<TOwnId, TStateId, TEvent>(
			StateMachinePath fsmPath,
			StateMachine<TOwnId, TStateId, TEvent> fsm);

		void VisitRegularState<TStateId>(
			StateMachinePath statePath,
			StateBase<TStateId> state);

		/// <summary>
		/// Called after the current state machine and all its child states (and child state machines) have
		/// been visited.
		/// </summary>
		void ExitStateMachine<TOwnId, TStateId, TEvent>(
			StateMachinePath fsmPath,
			StateMachine<TOwnId, TStateId, TEvent> fsm);
	}
}
