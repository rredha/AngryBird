using System.Collections;

namespace Project.Scripts.Runtime.Core.StateMachine.Alt.Interfaces
{
    public interface IState
    {
        void Enter();
        IEnumerator Execute();
        void Exit();

        void AddTransition(ITransition transition);
        void RemoveTransition(ITransition transition);
        void RemoveAllTransitions();
        
        bool ValidateTransition(out IState nextState);
        void EnableTransitions();
        void DisableTransitions();
    }
}