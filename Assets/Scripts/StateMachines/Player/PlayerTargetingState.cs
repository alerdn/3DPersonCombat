using System;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    private Vector2 _dodgingDirection;
    private float _remainingDodgeTime;

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
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        if (!stateMachine.Targeter.CurrentTarget)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement(deltaTime);
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
    }

    private void OnToggleTarget()
    {
        stateMachine.Targeter.CancelTarget();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        if (_remainingDodgeTime > 0f)
        {
            movement += stateMachine.transform.forward * _dodgingDirection.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            movement += stateMachine.transform.right * _dodgingDirection.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;

            _remainingDodgeTime = Mathf.Max(_remainingDodgeTime - deltaTime, 0);
            if (_remainingDodgeTime == 0)
            {
                stateMachine.Health.SetInvulnerable(false);
            }
        }
        else
        {
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        }

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

    private void OnDodge()
    {
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown) return;

        stateMachine.Health.SetInvulnerable(true);
        stateMachine.SetDodgeTime(Time.time);
        _dodgingDirection = stateMachine.InputReader.MovementValue;
        _remainingDodgeTime = stateMachine.DodgeDuration;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
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