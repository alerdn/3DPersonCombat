using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingFreeState : PlayerBaseState
{
    private readonly int BlockingFreeBlendTreeHash = Animator.StringToHash("BlockingFreeBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    public PlayerBlockingFreeState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Health.SetBlocking(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockingFreeBlendTreeHash, .1f);

        stateMachine.InputReader.ToggleTargetEvent += OnToggleTarget;
        stateMachine.InputReader.UseItemEvent += OnUseItem;
        stateMachine.InputReader.SwitchItemEvent += stateMachine.SwitchItem;
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.InputReader.IsBlocking)
        {
            ReturnToLocomotion();
            return;
        }

        Vector3 movement = CalculateFreelookMovement();
        Move(movement * stateMachine.BlockingMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, .1f, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, .1f, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Health.SetBlocking(false);
        stateMachine.InputReader.ToggleTargetEvent -= OnToggleTarget;
        stateMachine.InputReader.UseItemEvent -= OnUseItem;
        stateMachine.InputReader.SwitchItemEvent -= stateMachine.SwitchItem;
    }

    private void OnToggleTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;
        stateMachine.SwitchState(new PlayerBlockingTargetState(stateMachine));
    }
}
