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
        stateMachine.Health.SetBlocking(true);
        _remainingDodgeTime = stateMachine.DodgeDuration;

        stateMachine.Animator.SetFloat(DodgingForwardHash, _dodgingDirection.y);
        stateMachine.Animator.SetFloat(DodgingRightHash, _dodgingDirection.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgingBlendTreeHash, .1f);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = Vector3.zero;

        movement += stateMachine.transform.forward * _dodgingDirection.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += stateMachine.transform.right * _dodgingDirection.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;

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
    }
}