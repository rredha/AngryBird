using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.States;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Transitions;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Slingshot
{
    public class SlingshotController : MonoBehaviour
    {
        private StateMachine _slingshotStateMachine;
        private Slingshot _slingshot;

        private void Awake()
        {
            _slingshot = GetComponent<Slingshot>();
        }

        private void Start()
        {
            _slingshotStateMachine = new StateMachine();

            _slingshotStateMachine.AddState("Empty",
                new State(
                    onEnter: state => _slingshot.EmptyStateEnter(),
                    onLogic: state => _slingshot.EmptyStateUpdate(),
                    onExit: state => _slingshot.EmptyStateExit()
                    )
                );
            _slingshotStateMachine.AddState("Loaded",
                new State(
                    onEnter: state => _slingshot.LoadedStateEnter(),
                    onLogic: state => _slingshot.LoadedStateUpdate(),
                    onExit: state => _slingshot.LoadedStateExit()
                    )
                );
            
            _slingshotStateMachine.SetStartState("Empty");
            
            _slingshotStateMachine.AddTransition(new Transition(
                "Empty",
                "Loaded",
                transition => _slingshot.IsOverlapping));
            
            _slingshotStateMachine.AddTransition(new Transition(
                "Loaded",
                "Empty",
                transition => _slingshot.ReleasedTriggered));
            _slingshotStateMachine.Init();
        }

        private void Update()
        {
            _slingshotStateMachine.OnLogic();
        }
    }
}