using System;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public class TaskController
    {
        public GameObject TaskPrefab { get; set; }
        public ITaskBehaviour TaskBehaviour { get; set; }

        protected void SetActive()
        {
            
        }

        public void Notify(object sender, Projectile e)
        {
            
        }

        public void Enable(object sender, EventArgs e)
        {
            //TaskBehaviour.Initialize();
            //TaskBehaviour.Execute();
        }
        public void Disable(object sender, EventArgs e)
        {
        }
    }
}