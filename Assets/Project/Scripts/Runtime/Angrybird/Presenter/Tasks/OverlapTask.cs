using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Strategy/Overlap", fileName = "Overlap", order = 0)]
    public class OverlapTask : BaseTask
    {
        private Projectile _projectile;
        public OverlapTaskBehaviour taskBehaviour;
        public override void Enable(object sender, Projectile proj)
        {
            if (proj.IsSelected) return;
            _projectile = proj;
            
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
            Debug.Log("Select Strategy : Overlap");
        }
    }
}