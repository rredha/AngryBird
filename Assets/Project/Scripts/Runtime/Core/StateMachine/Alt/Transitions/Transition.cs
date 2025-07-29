using Project.Scripts.Runtime.Core.StateMachine.Alt.Interfaces;

namespace Project.Scripts.Runtime.Core.StateMachine.Alt.Transitions
{
    public class Transition : ITransition
    {
        readonly IState m_NextState;

        public Transition(IState nextState)
        {
            m_NextState = nextState;
        }
        public bool Validate(out IState nextState)
        {
            nextState = m_NextState;
            return true;
        }
    }
}