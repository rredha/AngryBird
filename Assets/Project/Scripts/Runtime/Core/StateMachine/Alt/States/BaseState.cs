using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Runtime.Core.StateMachine.Alt.Interfaces;
using UnityEngine;

namespace Project.Scripts.Runtime.Core.StateMachine.Alt.States
{
    public abstract class BaseState : IState
    {
        public virtual string Name { get; set; }
        protected bool m_Debug = false;

        private readonly List<ITransition> m_Transitions = new();

        public bool DebugEnable
        {
            get => m_Debug;
            set => m_Debug = value;
        }

        public virtual void Enter()
        {
        }

        public abstract IEnumerator Execute();

        public virtual void Exit()
        {
        }

        public void AddTransition(ITransition transition)
        {
            if (!m_Transitions.Contains(transition)) // doesnt contain the transition, then add it.
            {
                m_Transitions.Add(transition);
            }
        }

        public void RemoveTransition(ITransition transition)
        {
            if (m_Transitions.Contains(transition)) // doesnt contain the transition, then add it.
            {
                m_Transitions.Remove(transition);
            }
        }

        public void RemoveAllTransitions()
        {
            m_Transitions.Clear();
        }

        public virtual bool ValidateTransition(out IState nextState)
        {
            if (m_Transitions != null && m_Transitions.Count > 0)
            {
                foreach (var transition in m_Transitions)
                {
                    bool result = transition.Validate(out nextState);
                    if (result)
                    {
                        return true;
                    }
                }
            }

            nextState = null;
            return false;
        }

        public void EnableTransitions()
        {
            /*
            if (m_Transitions != null && m_Transitions.Count > 0)
            {
                foreach (var transition in m_Transitions)
                {
                    transition.Enable();
                } 
            }
            It seems that transitions can be enabled/disabled only after they've been validated.
            */

            foreach (var transition in m_Transitions)
            {
                transition.Enable();
            }
        }

        public void DisableTransitions()
        {
            foreach (var transition in m_Transitions)
            {
                transition.Disable();
            }
        }

        public virtual void LogCurrentState()
        {
            if (m_Debug)
                Debug.Log("Current State =" + Name + "(" + this.GetType().Name + ")");
        }
    }
}