using Project.Scripts.Runtime.Angrybird.Presenter.Birds;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public interface ISelectOnOverlapStrategy 
    {
        void Select(object sender, Projectile e);
    }
}