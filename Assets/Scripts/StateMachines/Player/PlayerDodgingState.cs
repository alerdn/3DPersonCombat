using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private readonly int DodgingBlendTreeHash = Animator.StringToHash("DodgingBlendTree");
    private readonly int DodgingForwardHash = Animator.StringToHash("DodgingForward");
    private readonly int DodgingRightHash = Animator.StringToHash("DodgingRight");

    private Vector3 _dodgingDirection;
    private float _remainingDodgeTime;

    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirection) : base(stateMachine)
    {
        _dodgingDirection = dodgingDirection;
    }

    public override void Enter()
    {
        stateMachine.Health.SetInvulnerable(true);
        _remainingDodgeTime = stateMachine.DodgeDuration;

        stateMachine.Animator.SetFloat(DodgingForwardHash, _dodgingDirection.y);
        stateMachine.Animator.SetFloat(DodgingRightHash, _dodgingDirection.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgingBlendTreeHash, .1f);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement, deltaTime);

        FaceTarget();

        _remainingDodgeTime -= deltaTime;
        if (_remainingDodgeTime <= 0)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        movement += forward * _dodgingDirection.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += right * _dodgingDirection.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        return movement;
    }
}