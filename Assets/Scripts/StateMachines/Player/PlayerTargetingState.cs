using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, .1f);

        stateMachine.InputReader.ToggleTargetEvent += OnToggleTarget;
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.Targeter.CurrentTarget)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.ToggleTargetEvent -= OnToggleTarget;
    }

    private void OnToggleTarget()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}