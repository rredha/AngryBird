using System.Collections;
using UnityEngine;
using Arcade.Project.Runtime.Games.AngryBird.Utils;

namespace Arcade.Project.Runtime.Games.AngryBird.Utils.PlayerState
{
  public class PickState : PlayerState
  {
    public PickState(PlayerContext context, PlayerStateMachine.EPlayerState key) : base(context, key)
    {
        PlayerContext Context = context;
    }

    public override void EnterState()
    {
      Debug.Log("Please pick the ball");

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
      return PlayerStateMachine.EPlayerState.Drop;
    }
  }
}
