using System;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

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

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
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

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;

        return movement;
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

        stateMachine.Animator.SetFloat(TargetingForwardHash, stateMachine.InputReader.MovementValue.y, .1f, deltaTime);
        stateMachine.Animator.SetFloat(TargetingRightHash, stateMachine.InputReader.MovementValue.x, .1f, deltaTime);
    }

    private void FaceTarget()
    {
        if (!stateMachine.Targeter.CurrentTarget) return;

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}