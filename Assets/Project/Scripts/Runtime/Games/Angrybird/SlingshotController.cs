using System;
using Arcade.Project.Runtime.Games.AngryBird;
using UnityEngine;
using UnityHFSM;

namespace Project.Runtime.Game.AngryBird.Project.Scripts.Runtime.Games.Angrybird
{
    public class SlingshotController : MonoBehaviour
    {
        private StateMachine m_SlingshotStateMachine;
        [SerializeField] private Slingshot m_Slingshot;

        private void Start()
        {
            m_SlingshotStateMachine = new StateMachine();

            m_SlingshotStateMachine.AddState("Empty",
                new State(
                    onEnter: state => m_Slingshot.EmptyStateEnter(),
                    onLogic: state => m_Slingshot.EmptyStateUpdate()
                    )
                );
            m_SlingshotStateMachine.AddState("Loaded",
                new State(
                    onEnter: state => m_Slingshot.LoadedStateEnter(),
                    onLogic: state => m_Slingshot.LoadedStateUpdate(),
                    onExit: state => m_Slingshot.LoadedStateExit()
                    )
                );
            
            m_SlingshotStateMachine.SetStartState("Empty");
            
            m_SlingshotStateMachine.AddTransition(new Transition(
                "Empty",
                "Loaded",
                transition => m_Slingshot.IsOverlapping));
            
            m_SlingshotStateMachine.AddTransition(new Transition(
                "Loaded",
                "Empty",
                transition => m_Slingshot.ReleasedTriggered && m_Slingshot.ProjectileIsFlying));
            m_SlingshotStateMachine.Init();
        }

        private void Update()
        {
            m_SlingshotStateMachine.OnLogic();
        }
    }
}