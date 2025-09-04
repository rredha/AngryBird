using System;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Strategy/SingleClick Task", fileName = "Single Click", order = 0)]
    public class SingleClickTask : BaseTask
    {
        private Projectile _projectile;
        public SingleClickBehaviour taskBehaviour;

        public void OnEnable()
        {
            taskBehaviour.Initialize();
        }

        public void OnDisable()
        {
            taskBehaviour.Cleanup();
        }
        public override void Enable(object sender, Projectile proj)
        {
            if (proj.IsSelected) return;
            _projectile = proj; 
            
            taskBehaviour.TaskComplete += OnTaskComplete_Execute;
            taskBehaviour.Execute();
        }

        private void OnTaskComplete_Execute(object sender, EventArgs eventArgs)
        {
            //Outcome(_projectile,Pointer);   
        }
        public override void Notify(object sender, Projectile e)
        {
        }

        protected override void Create()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetActive()
        {
            throw new System.NotImplementedException();
        }

    }
}