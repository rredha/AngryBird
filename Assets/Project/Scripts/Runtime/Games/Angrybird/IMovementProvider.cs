namespace Arcade.Project.Runtime.Games.AngryBird
{
    public interface IMovementProvider
    {
        public void Initialize();
        public void Subscribe();
        public void Unsubscribe();
    }
}