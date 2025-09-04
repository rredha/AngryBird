using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public abstract class BaseTaskData : ScriptableObject
    {
        [FormerlySerializedAs("CreateInstance")] public bool CreateGameObjectInstance;
        public GameObject Prefab;
        public ITaskBehaviour TaskBehaviour;
    }
}