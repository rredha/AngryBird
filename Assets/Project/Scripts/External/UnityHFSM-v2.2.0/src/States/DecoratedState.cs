using System;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Base;

namespace Project.Scripts.External.UnityHFSM_v2._2._0.src.States
{
	/// <summary>
	/// A class that allows you to run additional functions (companion code)
	/// before and after the wrapped state's code.
	/// It does not interfere with the wrapped state's timing / needsExitTime / ... behaviour.
	/// </summary>
	public class DecoratedState<TStateId, TEvent> : StateBase<TStateId>, ITriggerable<TEvent>, IActionable<TEvent>
	{
		private readonly Action<StateBase<TStateId>>
        	beforeOnEnter,
        	afterOnEnter,

        	beforeOnLogic,
        	afterOnLogic,

        	beforeOnExit,
        	afterOnExit;

        public readonly StateBase<TStateId> state;

        public DecoratedState(
        	StateBase<TStateId> state,

        	Action<StateBase<TStateId>> beforeOnEnter = null,
        	Action<StateBase<TStateId>> afterOnEnter = null,

        	Action<StateBase<TStateId>> beforeOnLogic = null,
        	Action<StateBase<TStateId>> afterOnLogic = null,

        	Action<StateBase<TStateId>> beforeOnExit = null,
        	Action<StateBase<TStateId>> afterOnExit = null) : base(state.needsExitTime, state.isGhostState)
        {
        	this.state = state;

        	this.beforeOnEnter = beforeOnEnter;
        	this.afterOnEnter = afterOnEnter;

        	this.beforeOnLogic = beforeOnLogic;
        	this.afterOnLogic = afterOnLogic;

        	this.beforeOnExit = beforeOnExit;
        	this.afterOnExit = afterOnExit;
        }

        public override void Init()
        {
        	state.name = name;
        	state.fsm = fsm;

        	state.Init();
        }

        public override void OnEnter()
        {
        	beforeOnEnter?.Invoke(this);
        	state.OnEnter();
        	afterOnEnter?.Invoke(this);
        }

        public override void OnLogic()
        {
        	beforeOnLogic?.Invoke(this);
        	state.OnLogic();
        	afterOnLogic?.Invoke(this);
        }

        public override void OnExit()
        {
        	beforeOnExit?.Invoke(this);
        	state.OnExit();
        	afterOnExit?.Invoke(this);
        }

        public override void OnExitRequest()
        {
        	state.OnExitRequest();
        }

        public void Trigger(TEvent trigger)
        {
        	(state as ITriggerable<TEvent>)?.Trigger(trigger);
        }

        public void OnAction(TEvent trigger)
        {
        	(state as IActionable<TEvent>)?.OnAction(trigger);
        }

        public void OnAction<TData>(TEvent trigger, TData data)
        {
        	(state as IActionable<TEvent>)?.OnAction<TData>(trigger, data);
        }

        public override string GetActiveHierarchyPath()
        {
        	return state.GetActiveHierarchyPath();
        }
	}

	/// <inheritdoc />
	public class DecoratedState : DecoratedState<string, string>
	{
		public DecoratedState(
			StateBase<string> state,
			Action<StateBase<string>> beforeOnEnter = null,
			Action<StateBase<string>> afterOnEnter = null,
			Action<StateBase<string>> beforeOnLogic = null,
			Action<StateBase<string>> afterOnLogic = null,
			Action<StateBase<string>> beforeOnExit = null,
			Action<StateBase<string>> afterOnExit = null)
		: base(
			state,
			beforeOnEnter,
			afterOnEnter,
			beforeOnLogic,
			afterOnLogic,
			beforeOnExit,
			afterOnExit) { }
	}
}
