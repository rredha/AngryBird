using Project.Scripts.External.UnityHFSM_v2._2._0.src.StateMachine;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.States;
using Project.Scripts.External.UnityHFSM_v2._2._0.src.Transitions;
using UnityEngine;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Birds
{
    public class ProjectileController : MonoBehaviour
    {
        private StateMachine _projectileStateMachine;
        private Projectile _projectile;

        private void Start()
        {
            _projectile = GetComponent<Projectile>();
            _projectileStateMachine = new StateMachine();

            _projectileStateMachine.AddState("Idle",
                new State(
                    onEnter: state => _projectile.IdleStateEnter(),
                    onLogic: state => _projectile.IdleStateUpdate(),
                    onExit: state => _projectile.IdleStateExit()
                )
            );
            _projectileStateMachine.AddState("Selected",
                new State(
                    onEnter: state => _projectile.SelectedStateEnter(),
                    onExit: state => _projectile.SelectedStateExit()
                )
            );
            _projectileStateMachine.AddState("Used",
                new State(
                    onEnter: state => _projectile.UsedStateEnter(),
                    onLogic: state => _projectile.UsedStateUpdate(),
                    onExit: state => _projectile.UsedStateExit()
                )
            );
            
            _projectileStateMachine.SetStartState("Idle");
            
            _projectileStateMachine.AddTransition(new Transition(
                "Idle",
                "Selected",
                transition => _projectile.IsSelected));
            
            _projectileStateMachine.AddTransition(new Transition(
                "Selected",
                "Used",
                transition => _projectile.IsThrown));
            _projectileStateMachine.Init();
        }

        private void Update()
        {
            _projectileStateMachine.OnLogic();
        }
    }
}