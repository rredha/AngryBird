using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Strategy/Multiple Clicks", fileName = "Multiple Clicks", order = 0)]
    public class MultipleClicksTask : BaseTask
    // still has a small issue, need to trigger the event only if it's still on trigger stay !
    {
        private Projectile _projectile;
        public MultipleClickBehaviour taskBehaviour;
        public int threshold;

        public void OnEnable()
        {
            taskBehaviour.Configure(threshold);
            taskBehaviour.Initialize();
        }

        public void OnDisable()
        {
            taskBehaviour.Cleanup();
        }

        public override void Enable(object sender, Projectile projectile)
        {
            if (projectile.IsSelected) return;
            _projectile = projectile; 
            
            taskBehaviour.TaskComplete += OnTaskComplete_Execute;
            taskBehaviour.Execute();
        }
        private void OnTaskComplete_Execute(object sender, EventArgs eventArgs)
        {
            Outcome(_projectile, Pointer);   
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
            Debug.Log("MultipleClicksTask enabled");
        }
    }
}