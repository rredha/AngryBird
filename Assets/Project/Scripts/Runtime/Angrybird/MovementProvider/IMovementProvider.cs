namespace Project.Scripts.Runtime.Angrybird.MovementProvider
{
    public interface IMovementProvider
    {
        public void Initialize();
        public void Subscribe();
        public void Unsubscribe();
    }
}