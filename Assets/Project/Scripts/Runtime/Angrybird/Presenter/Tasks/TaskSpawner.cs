using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    public class TaskSpawner : MonoBehaviour
    {
        public TaskSpawner Instance { get; private set; }
        public static GameObject SpawnedRef { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance == this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public static void InstantiateTask(GameObject taskPrefab)
        {
            Instantiate(taskPrefab);
        }
        public static void InstantiateTaskWithRef(GameObject taskPrefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            SpawnedRef = Instantiate(taskPrefab, position, rotation, parent);
        }

    }
}