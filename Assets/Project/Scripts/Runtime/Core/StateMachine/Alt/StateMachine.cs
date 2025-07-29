using System;
using System.Collections;
using Project.Scripts.Runtime.Core.StateMachine.Alt.Interfaces;
using UnityEngine;

namespace Project.Scripts.Runtime.Core.StateMachine.Alt
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        public virtual void SetCurrentState(IState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            if (CurrentState != null && m_CurrentPlayCoroutine != null)
            {
                Skip();
            }

            CurrentState = state;
            Coroutines.StartCoroutine(Play());
        }

        private Coroutine m_CurrentPlayCoroutine;
        private bool m_PlayLock;

        private IEnumerator Play()
        {
            if (!m_PlayLock)
            {
                m_PlayLock = true;
                CurrentState.Enter();
                m_CurrentPlayCoroutine = Coroutines.StartCoroutine(CurrentState.Execute());

                yield return m_CurrentPlayCoroutine;

                m_CurrentPlayCoroutine = null;
            }


        }
        
        void Skip()
        {
            if (CurrentState == null)
                throw new Exception($"{nameof(CurrentState)} is null");
            if (m_CurrentPlayCoroutine != null)
            {
                Coroutines.StopCoroutine(ref m_CurrentPlayCoroutine);
                // go through the state completion.
                CurrentState.Exit();
                m_CurrentPlayCoroutine = null;
                m_PlayLock = false;
            }
        }

        public virtual void Run(IState state)
        {
            SetCurrentState(state);
            Run();
        }

        Coroutine m_LoopCoroutine;

        public virtual void Run()
        {
            if (m_LoopCoroutine != null)
                return;
            m_LoopCoroutine = Coroutines.StartCoroutine(Loop());
        }

        public void Stop()
        {
            if (m_LoopCoroutine == null)
                return;
            if (CurrentState != null && m_CurrentPlayCoroutine != null)
            {
                Skip();
            }
            
            Coroutines.StopCoroutine(ref m_LoopCoroutine);
            CurrentState = null;
        }

        protected virtual IEnumerator Loop()
        {
            while (true)
            {
                if (CurrentState != null && m_CurrentPlayCoroutine != null)
                {
                    if (CurrentState.ValidateTransition(out var nextState))
                    {
                        if (m_PlayLock)
                        {
                            CurrentState.Exit();
                            m_PlayLock = false;
                        }
                        
                        CurrentState.DisableTransitions();
                        SetCurrentState(nextState);
                        CurrentState.EnableTransitions();
                    }
                }

                yield return null;
            }
        }

        public bool IsRunning => m_LoopCoroutine != null;
    }

    public static class Coroutines
    {
        public static MonoBehaviour s_CoroutineRunner;
        public static bool IsInitialized => s_CoroutineRunner != null;
        
        public static void Initialize(MonoBehaviour runner)
        {
            s_CoroutineRunner = runner;
        }

        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            if (s_CoroutineRunner != null)
            {
                throw new InvalidOperationException("Coroutine Runner is not initialized.");
            }

            return s_CoroutineRunner.StartCoroutine(coroutine);
        }

        public static void StopCoroutine(IEnumerator coroutine)
        {
            if (s_CoroutineRunner != null)
                s_CoroutineRunner.StopCoroutine(coroutine);
        }

        public static void StopCoroutine(ref Coroutine coroutine)
        {
            if (s_CoroutineRunner != null && coroutine != null)
            {
                s_CoroutineRunner.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}