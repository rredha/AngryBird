using UnityEngine;
using UnityHFSM;

namespace Arcade.Project.Runtime.Games.AngryBird
{
    public class ProjectileController : MonoBehaviour
    {
        private StateMachine m_ProjectileStateMachine;
        private Projectile m_Projectile;

        private void Start()
        {
            m_Projectile = GetComponent<Projectile>();
            m_ProjectileStateMachine = new StateMachine();

            m_ProjectileStateMachine.AddState("Idle",
                new State(
                    onEnter: state => m_Projectile.IdleStateEnter(),
                    onLogic: state => m_Projectile.IdleStateUpdate(),
                    onExit: state => m_Projectile.IdleStateExit()
                )
            );
            m_ProjectileStateMachine.AddState("Selected",
                new State(
                    onEnter: state => m_Projectile.SelectedStateEnter(),
                    onLogic: state => m_Projectile.SelectedStateUpdate(),
                    onExit: state => m_Projectile.SelectedStateExit()
                )
            );
            m_ProjectileStateMachine.AddState("Used",
                new State(
                    onEnter: state => m_Projectile.UsedStateEnter(),
                    onLogic: state => m_Projectile.UsedStateUpdate(),
                    onExit: state => m_Projectile.UsedStateExit()
                )
            );
            
            m_ProjectileStateMachine.SetStartState("Idle");
            
            m_ProjectileStateMachine.AddTransition(new Transition(
                "Idle",
                "Selected",
                transition => m_Projectile.IsSelected));
            
            m_ProjectileStateMachine.AddTransition(new Transition(
                "Selected",
                "Used",
                transition => m_Projectile.IsThrown));
            m_ProjectileStateMachine.Init();
        }

        private void Update()
        {
            m_ProjectileStateMachine.OnLogic();
        }
    }
}