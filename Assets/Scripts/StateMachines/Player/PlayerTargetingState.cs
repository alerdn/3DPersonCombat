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
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.SwitchWeaponEvent += OnSwitchWeapon;
        stateMachine.InputReader.SwitchSecondaryWeaponEvent += OnSwitchSecondaryWeapon;
        stateMachine.InputReader.UseItemEvent += OnUseItem;
        stateMachine.InputReader.SwitchItemEvent += stateMachine.SwitchItem;
        stateMachine.InputReader.SwitchSpellEvent += stateMachine.SwitchSpell;
    }

    public override void Tick(float deltaTime)
    {
        //TODO: Trocar o alvo quando mexer na câmera

        if (stateMachine.InputReader.IsAttacking)
        {
            if (CanAttack(0))
            {
                EnterAttackingState(0);
                return;
            }
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingTargetState(stateMachine));
            return;
        }

        if (!stateMachine.Targeter.CurrentTarget)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateTargetingMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.ToggleTargetEvent -= OnToggleTarget;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.SwitchWeaponEvent -= OnSwitchWeapon;
        stateMachine.InputReader.SwitchSecondaryWeaponEvent -= OnSwitchSecondaryWeapon;
        stateMachine.InputReader.UseItemEvent -= OnUseItem;
        stateMachine.InputReader.SwitchItemEvent -= stateMachine.SwitchItem;
        stateMachine.InputReader.SwitchSpellEvent -= stateMachine.SwitchSpell;
    }

    private void OnToggleTarget()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
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

    private void OnSwitchWeapon()
    {
        stateMachine.SwitchWeapon();
    }

    private void OnSwitchSecondaryWeapon()
    {
        stateMachine.SwitchShield();
    }
}