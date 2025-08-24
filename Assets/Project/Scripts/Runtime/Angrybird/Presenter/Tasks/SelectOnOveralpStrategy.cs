using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class SelectOnOverlapStrategy : BaseTask
    {
        // still a little problem with this one, bug, projectile follows mouse.
        public override void Enable(object sender, Projectile e)
        {
            e.IsSelected = true;
            e.SetStatic();
            //e.transform.SetParent(FindFirstObjectByType<Pointer>().transform);
            //_movementProvider.SelectEventRaised = false;
        }

        protected override void Create()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetActive()
        {
            throw new System.NotImplementedException();
        }

        public override void Notify(object sender, Projectile e)
        {
            throw new System.NotImplementedException();
        }
    }
}