using System;
using Arcade.Project.Runtime.Games.AngryBird.Utils.InputSystem;
using Project.Scripts.Runtime.Angrybird.MovementProvider;
using Project.Scripts.Runtime.Angrybird.Presenter.Birds;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Runtime.Angrybird.Presenter.Pointer
{
    public abstract class ReleaseStrategyBase : ScriptableObject
    {
        public MousePointer MovementProvider;
        public PlayerInputActions PlayerInputActions;

        public event EventHandler<bool> TaskComplete;
        public abstract void Task(object sender, bool e);
        public abstract void Execute(object sender, bool e);
    }
    
    public class TimerReleaseStrategy : ReleaseStrategyBase
    {
        public override void Task(object sender, bool e)
        {
            PlayerInputActions.Player.Enable();
        }

        public override void Execute(object sender, bool e)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ClickReleaseStrategy : ReleaseStrategyBase
    {
        public override void Task(object sender, bool e)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(object sender, bool e)
        {
            throw new System.NotImplementedException();
        }
    }
}