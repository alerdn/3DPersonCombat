using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack _attack;
    private Weapon _weapon;
    private bool _alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _weapon = stateMachine.CurrentWeapon;
        if (_weapon)
        {
            _attack = _weapon.Attacks[attackIndex];
            _weapon.SetAttack(_attack.Damage, _attack.Knockback);
        }
    }

    public override void Enter()
    {
        if (_attack == null) return;

        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (_attack == null)
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }

            return;
        }

        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if (normalizedTime < 1f)
        {
            if (normalizedTime >= _attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
    }

    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (_attack.ComboStateIndex == -1) return;

        if (normalizedTime < _attack.ComboAttackTime) return;

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, _attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) return;

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * _attack.Force);
        _alreadyAppliedForce = true;
    }
}