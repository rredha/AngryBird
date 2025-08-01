namespace Project.Scripts.External.UnityHFSM_v2._2._0.src.Inspection
{
	public interface IVisitableState
	{
		/// <summary>
		/// Accepts a visitor that can perform operations on the current state.
		/// This method is part of the Visitor Pattern, which allows adding new behavior
		/// to state machine states without modifying their class structure. It is used to
		/// implement dynamic inspection tools for hierarchical state machines.
		/// </summary>
		void AcceptVisitor(IStateVisitor visitor);
	}
}
