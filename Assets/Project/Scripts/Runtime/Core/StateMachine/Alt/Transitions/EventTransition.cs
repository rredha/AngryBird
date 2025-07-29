using System;
using Project.Scripts.Runtime.Core.StateMachine.Alt.Interfaces;

namespace Project.Scripts.Runtime.Core.StateMachine.Alt.Transitions
{
    public class EventTransition : ITransition
    {
        bool m_EventRaised;
        private ActionWrapper m_ActionWrapper;
        private readonly IState m_NextState;

        public EventTransition(IState nextState, ActionWrapper actionWrapper)
        {
            m_NextState = nextState;
            m_ActionWrapper = actionWrapper;
        }
        public bool Validate(out IState nextState)
        {
            nextState = m_EventRaised ? m_NextState : null;
            return m_EventRaised;
        }

        public void OnEventRaised()
        {
            m_EventRaised = true;
        }

        public void Enable()
        {
            m_ActionWrapper.Subscribe(OnEventRaised);
            m_EventRaised = false;
        }

        public void Disable()
        {
            m_ActionWrapper.Unsubscribe(OnEventRaised);
            m_EventRaised = false;
        }

    }

    public class ActionWrapper
    {
        public Action<Action> Subscribe { get; set; }
        public Action<Action> Unsubscribe { get; set; }
    }
}