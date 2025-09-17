using System;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public class TaskHandler : MonoBehaviour
    {
        // TODO : add task data output (time span)
        [SerializeField] private BaseTaskData taskData;
        [SerializeField] private Pointer pointer;
        
        private ITaskBehaviour _taskBehaviour;
        private Projectile _projectile;
        private SelectAction _selectAction;

        public void Awake()
        {
            _selectAction = gameObject.GetComponent<SelectAction>();
        }

        public void Enable(object sender, Projectile projectile)
        {
            _selectAction.Unsubscribe(); // unsubscribe before executing anything else.
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
                Subscribe();
                _taskBehaviour.Initialize();
            }
        }
        
        private void Disable()
        {
            _taskBehaviour.Cleanup();
            Unsubscribe();
            _selectAction.Subscribe(); // subscribe after every thing has been cleaned for next projectile ?
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
        }

        private void Notify(object sender, EventArgs e)
        {
            Debug.Log("Task Completed");
        }

        private void Unsubscribe()
        {
            _taskBehaviour.TaskComplete -= Notify;
            _taskBehaviour.TaskComplete -= Outcome;
        }
    }
}