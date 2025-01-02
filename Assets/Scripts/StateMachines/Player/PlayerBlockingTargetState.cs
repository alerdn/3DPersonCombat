using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingTargetState : PlayerBaseState
{
    private readonly int BlockingTargetBlendTreeHash = Animator.StringToHash("BlockingTargetBlendTree");
    private readonly int BlockingForwardHash = Animator.StringToHash("BlockingForward");
    private readonly int BlockingRightHash = Animator.StringToHash("BlockingRight");

    public PlayerBlockingTargetState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Health.SetBlocking(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockingTargetBlendTreeHash, .1f);

        stateMachine.InputReader.ToggleTargetEvent += OnToggleTarget;
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.InputReader.IsBlocking)
        {
            ReturnToLocomotion();
            return;
        }

        if (!stateMachine.Targeter.CurrentTarget)
        {
            stateMachine.SwitchState(new PlayerBlockingFreeState(stateMachine));
            return;
        }

        Vector3 movement = CalculateTargetingMovement();
        Move(movement * stateMachine.BlockingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.Health.SetBlocking(false);
        stateMachine.InputReader.ToggleTargetEvent -= OnToggleTarget;
    }

    private void OnToggleTarget()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerBlockingFreeState(stateMachine));
    }

    private void UpdateAnimator(float deltaTime)
    {
        Vector3 movement = stateMachine.InputReader.MovementValue;
        if (movement.y != 0)
        {
            movement.y = movement.y > 0 ? 1 : -1;
        }
        if (movement.x != 0)
        {
            movement.x = movement.x > 0 ? 1 : -1;
        }

        stateMachine.Animator.SetFloat(BlockingForwardHash, stateMachine.InputReader.MovementValue.y, .1f, deltaTime);
        stateMachine.Animator.SetFloat(BlockingRightHash, stateMachine.InputReader.MovementValue.x, .1f, deltaTime);
    }
}
