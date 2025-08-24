using System;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Strategy/Pop Bubbles", fileName = "Hexagon Bubbles Layout", order = 0)]
    public class BubblePopTask : BaseTask
    {
        public BubblePopBehaviour taskBehaviour;
        
        private Projectile _projectile;

        public override void Enable(object sender, Projectile proj)
        {
            if (proj.IsSelected) return;
            _projectile = proj;
            
            taskBehaviour.TaskComplete += OnTaskComplete_Execute;
            taskBehaviour.Execute();
        }

        private void OnTaskComplete_Execute(object sender, EventArgs e)
        {
            Outcome(_projectile, Pointer);
        }

        protected override void Create()
        {
        }

        protected override void SetActive()
        {
        }

        public override void Notify(object sender, Projectile e)
        {
        }
    }
}
