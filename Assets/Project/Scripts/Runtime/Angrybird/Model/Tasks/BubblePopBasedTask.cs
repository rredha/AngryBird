using Arcade;
using Model.Shape;
using Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks.Shape;
using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Runtime.AngryBird.Project.Scripts.Runtime.Angrybird.Presenter.Tasks
{
    [CreateAssetMenu(menuName = "Select Strategy/BubblePop Based Task", fileName = "Pop Shape", order = 0)]
    public class BubblePopBasedTask : BaseTaskData
    {
        public ShapeSO Shape;
        private void OnEnable()
        {
            CreateGameObjectInstance = true;
        }
    }
}