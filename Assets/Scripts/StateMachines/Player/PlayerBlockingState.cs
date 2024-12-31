using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockingBlendTreeHash = Animator.StringToHash("BlockingBlendTree");
    private readonly int BlockingForwardHash = Animator.StringToHash("BlockingForward");
    private readonly int BlockingRightHash = Animator.StringToHash("BlockingRight");

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Health.SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockingBlendTreeHash, .1f);

        stateMachine.InputReader.ToggleTargetEvent += OnToggleTarget;
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.InputReader.IsBlocking)
        {
            ReturnToLocomotion();
            return;
        }

        Vector3 movement = CalculateMovement(out bool isTargeting);
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime, isTargeting);

        if (isTargeting)
        {
            FaceTarget();
        }
        else
        {
            if (movement != Vector3.zero)
            {
                FaceMovementDirection(movement, deltaTime);
            }
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);

        stateMachine.InputReader.ToggleTargetEvent -= OnToggleTarget;
    }

    private void OnToggleTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private Vector3 CalculateMovement(out bool isTargeting)
    {
        Vector3 movement;

        if (stateMachine.Targeter.CurrentTarget != null)
        {
            movement = CalculateTargetingMovement();
            isTargeting = true;
        }
        else
        {
            movement = CalculateFreelookMovement();
            isTargeting = false;
        }

        return movement;
    }

    private void UpdateAnimator(float deltaTime, bool isTargeting)
    {
        float forward = stateMachine.InputReader.MovementValue.y;
        float right = stateMachine.InputReader.MovementValue.x;
        Vector3 movement = stateMachine.InputReader.MovementValue;

        if (isTargeting)
        {
            if (movement.y != 0)
            {
                forward = movement.y > 0 ? 1 : -1;
            }
            if (movement.x != 0)
            {
                right = movement.x > 0 ? 1 : -1;
            }
        }
        else
        {
            forward = movement.magnitude != 0 ? 1 : 0;
            right = 0;
        }

        stateMachine.Animator.SetFloat(BlockingForwardHash, forward, .1f, deltaTime);
        stateMachine.Animator.SetFloat(BlockingRightHash, right, .1f, deltaTime);
    }
}
