using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.States;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Transitions;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Slingshot
{
    public class SlingshotController : MonoBehaviour
    {
        private StateMachine m_SlingshotStateMachine;
        private Slingshot _slingshot;

        private void Awake()
        {
            _slingshot = GetComponent<Slingshot>();
        }

        private void Start()
        {
            m_SlingshotStateMachine = new StateMachine();

            m_SlingshotStateMachine.AddState("Empty",
                new State(
                    onEnter: state => _slingshot.EmptyStateEnter(),
                    onLogic: state => _slingshot.EmptyStateUpdate(),
                    onExit: state => _slingshot.EmptyStateExit()
                    )
                );
            m_SlingshotStateMachine.AddState("Loaded",
                new State(
                    onEnter: state => _slingshot.LoadedStateEnter(),
                    onLogic: state => _slingshot.LoadedStateUpdate(),
                    onExit: state => _slingshot.LoadedStateExit()
                    )
                );
            
            m_SlingshotStateMachine.SetStartState("Empty");
            
            m_SlingshotStateMachine.AddTransition(new Transition(
                "Empty",
                "Loaded",
                transition => _slingshot.IsOverlapping));
            
            m_SlingshotStateMachine.AddTransition(new Transition(
                "Loaded",
                "Empty",
                transition => _slingshot.ReleasedTriggered));
            m_SlingshotStateMachine.Init();
        }

        private void Update()
        {
            m_SlingshotStateMachine.OnLogic();
        }
    }
}