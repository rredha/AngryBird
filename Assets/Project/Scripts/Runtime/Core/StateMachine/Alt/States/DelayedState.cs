using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Runtime.Core.StateMachine.Alt.States
{
    public class DelayedState : BaseState
    {
        // should rewrite this state, as it may be interesting, to use it for other things than ui.
        readonly float m_DelayInSeconds;

        readonly Action<float> m_ProgressUpdated;
        readonly Action m_OnExit;

        public DelayedState(float delayInSeconds, Action<float> onUpdate = null, Action onExit = null, string stateName = nameof(DelayedState))
        {
            m_DelayInSeconds = delayInSeconds;
            m_ProgressUpdated = onUpdate;
            m_OnExit = onExit;
            Name = stateName;
        }
        public override IEnumerator Execute()
        {
            var startTime = Time.time;

            if (m_Debug)
            {
                base.LogCurrentState();
            }

            while (Time.time - startTime < m_DelayInSeconds)
            {
                yield return null;
                float progressValue = (Time.time - startTime) / m_DelayInSeconds;
                m_ProgressUpdated?.Invoke(progressValue * 100);
            }
        }

        public override void Exit()
        {
            m_OnExit?.Invoke();
        }
    }
}