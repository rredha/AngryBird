using System;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using Project.Scripts.Runtime.Angrybird.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public class TaskHandler : MonoBehaviour
    {
        // TODO : add task data output (time span)
        [SerializeField] private BaseTaskData taskData;
        [SerializeField] private Pointer pointer;
        
        private ITaskBehaviour _taskBehaviour;
        private Projectile _projectile;
        public void Enable(object sender, Projectile projectile)
        {
            _projectile = projectile;
            if (taskData.CreateGameObjectInstance)
            {
                var obj = Instantiate(taskData.Prefab);
                _taskBehaviour = obj.GetComponent<ITaskBehaviour>();
            }
            else
            {
                _taskBehaviour = taskData.TaskBehaviour;
            }
            
            if (_projectile.IsIdle && !_projectile.IsSelected && _projectile.IsOverlapped)
            {
                _taskBehaviour.IsTaskStarted = true;
                _taskBehaviour.Initialize();
                Subscribe();
            }
        }
        
        private void Disable()
        {
            _taskBehaviour.Cleanup();
            Unsubscribe();
        }
        private void Outcome(object sender, EventArgs eventArgs)
        {
            _projectile.IsSelected = true;
            _projectile.SetStatic();
            _projectile.transform.SetParent(pointer.transform);
            _projectile.transform.localPosition = Vector3.zero;
            Disable();
        }
        private void Subscribe()
        {
            _taskBehaviour.TaskComplete += Notify;
            _taskBehaviour.TaskComplete += Outcome;
            //_taskBehaviour.TaskComplete += Disable;
        }

        private void Notify(object sender, EventArgs e)
        {
            Debug.Log("Task Completed");
        }

        private void Unsubscribe()
        {
            _taskBehaviour.TaskComplete -= Notify;
            _taskBehaviour.TaskComplete -= Outcome;
            //_taskBehaviour.TaskComplete -= Disable;
        }
    }
}