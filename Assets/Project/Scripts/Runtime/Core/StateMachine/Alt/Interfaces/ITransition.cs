namespace Project.Scripts.Runtime.Core.StateMachine.Alt.Interfaces
{
    public interface ITransition
    {
        bool Validate(out IState nextState);

        void Enable()
        {
            
        }

        void Disable()
        {
            
        }
    }
}