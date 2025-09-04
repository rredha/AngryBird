using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Actions", fileName = "SelectActionSelector", order = 0)]
    public class SelectActionSelector : ScriptableObject
    {
        public TaskEnum TaskEnum;
        public BaseTask Task;
        public ITaskBehaviour TaskBehaviour;

        public void OnEnable()
        {
            /*
            switch (TaskEnum)
            {
                case TaskEnum.Overlap:
                    Task = CreateInstance<OverlapTask>();
                    break;
                case TaskEnum.SingleClick:
                    Task = CreateInstance<SingleClickTask>();
                    break;
            }
            */
        }
    }
    public enum TaskEnum
    {
        Overlap,
        SingleClick,
        MultipleClick,
        PopBubbles
    }
}