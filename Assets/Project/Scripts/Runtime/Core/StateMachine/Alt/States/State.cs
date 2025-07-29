using System;
using System.Collections;

namespace Project.Scripts.Runtime.Core.StateMachine.Alt.States
{
    public class State : BaseState
    {
        private readonly Action m_OnExecute;
        public State(Action OnExecute, string stateName = nameof(State), bool enableDebug = false)
        {
            m_OnExecute = OnExecute;
            Name = stateName;
            DebugEnable = enableDebug;

        }
        public override IEnumerator Execute()
        {
            yield return null;

            if (m_Debug)
            {
                base.LogCurrentState();
            }
            m_OnExecute?.Invoke();
        }
    }
}