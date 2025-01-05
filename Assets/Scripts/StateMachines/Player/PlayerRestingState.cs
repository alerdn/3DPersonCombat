using UnityEngine;

public class PlayerRestingState : PlayerBaseState
{
    private static readonly int RestHash = Animator.StringToHash("Rest");

    public PlayerRestingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(RestHash, .1f);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}