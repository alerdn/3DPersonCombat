using System;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private float AnimatorDampTime = .1f;
    private bool _shouldFade;

    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        _shouldFade = shouldFade;
    }

    public override void Enter()
    {
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);

        if (_shouldFade)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, .1f);
        }
        else
        {
            stateMachine.Animator.Play(FreeLookBlendTreeHash);
        }

        stateMachine.InputReader.ToggleTargetEvent += OnToggleTarget;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.SwitchWeaponEvent += OnSwitchWeapon;
        stateMachine.InputReader.SwitchSecondaryWeaponEvent += OnSwitchSecondaryWeapon;
        stateMachine.InputReader.UseItemEvent += OnUseItem;
        stateMachine.InputReader.SwitchItemEvent += OnSwitchItem;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            if (CanAttack(0))
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingFreeState(stateMachine));
            return;
        }

        Vector3 movement = CalculateFreelookMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.ToggleTargetEvent -= OnToggleTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.SwitchWeaponEvent -= OnSwitchWeapon;
        stateMachine.InputReader.SwitchSecondaryWeaponEvent -= OnSwitchSecondaryWeapon;
        stateMachine.InputReader.UseItemEvent -= OnUseItem;
        stateMachine.InputReader.SwitchItemEvent -= OnSwitchItem;
    }

    private void OnToggleTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void OnSwitchWeapon()
    {
        stateMachine.SwitchPrimaryWeapon();
    }

    private void OnSwitchSecondaryWeapon()
    {
        stateMachine.SwitchSecondaryWeapon();
    }
}
