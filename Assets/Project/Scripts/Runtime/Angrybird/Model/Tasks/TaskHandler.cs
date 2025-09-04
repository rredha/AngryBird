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
        private Spawner _spawner;
        
        private ITaskBehaviour _taskBehaviour;
        private Projectile _projectile;

        private void Awake()
        {
            _spawner = GetComponent<Spawner>();
        }

        private void Start()
        {
            _taskBehaviour = taskData.TaskBehaviour;
        }

        public void Enable(object sender, Projectile projectile)
        {
            _projectile = projectile;
            if (taskData.CreateGameObjectInstance)
                Create(taskData.Prefab);
            if (_projectile.IsIdle && !_projectile.IsSelected && _projectile.IsOverlapped)
            {
                _taskBehaviour.Initialize();
                Subscribe();
            }
        }
        
        private void Disable(object sender, EventArgs eventArgs)
        {
            _taskBehaviour.Cleanup();
            Unsubscribe();
        }
        private void Outcome(object sender, EventArgs eventArgs)
        {
            _projectile.SetStatic();
            _projectile.transform.SetParent(pointer.transform);
            _projectile.transform.localPosition = Vector3.zero;
        }
        private void Subscribe()
        {
            _taskBehaviour.TaskComplete += Notify;
            _taskBehaviour.TaskComplete += Outcome;
            _taskBehaviour.TaskComplete += Disable;
        }

        private void Notify(object sender, EventArgs e)
        {
            //Debug.Log("Task Completed");
        }

        private void Unsubscribe()
        {
            _taskBehaviour.TaskComplete -= Notify;
            _taskBehaviour.TaskComplete -= Outcome;
            _taskBehaviour.TaskComplete -= Disable;
        }

        private void Create(GameObject prefab) => _spawner.Spawn(prefab);

    }
}