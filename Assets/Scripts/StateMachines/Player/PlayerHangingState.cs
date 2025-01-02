using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private readonly int HangingHash = Animator.StringToHash("Hanging");
    private Vector3 _ledgeForward;

    public PlayerHangingState(PlayerStateMachine playerStateMachine, Vector3 ledgeForward) : base(playerStateMachine)
    {
        _ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(_ledgeForward, Vector3.up);

        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y > 0f)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
        else if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}