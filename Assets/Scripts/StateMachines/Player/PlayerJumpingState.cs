using System;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private Vector3 _momentum;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        _momentum = stateMachine.CharacterController.velocity;
        _momentum.y = 0;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, .1f);

        stateMachine.LedgeDetector.OnLedgeDetected += HandleLedgeDetect;
    }

    public override void Tick(float deltaTime)
    {
        Move(_momentum, deltaTime);

        if (stateMachine.CharacterController.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        if (stateMachine.InputReader.IsAttacking)
        {
            if (CanAttack(3))
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 3));
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