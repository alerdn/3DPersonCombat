using System;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private Vector3 _momentum;

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _momentum = stateMachine.CharacterController.velocity;
        _momentum.y = 0;

        stateMachine.LedgeDetector.OnLedgeDetected += HandleLedgeDetect;

        stateMachine.Animator.CrossFadeInFixedTime(FallHash, .1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(_momentum, deltaTime);

        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
            return;
        }

        if (stateMachine.InputReader.IsAttacking)
        {
            if (CanAttack(3, true))
            {
                EnterAttackingState(3, true);
                return;
            }
        }

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetected -= HandleLedgeDetect;
    }
}