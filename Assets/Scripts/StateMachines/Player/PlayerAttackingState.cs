using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack _attack;
    private Weapon _weapon;
    private bool _alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _weapon = stateMachine.CurrentWeapon;

        _attack = _weapon.Attacks[attackIndex];
        _weapon.SetAttack(_attack.Damage, _attack.Knockback, UnitType.Enemy);
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
        AudioManager.Instance.PlayCue("Swing");
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

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
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        stateMachine.CurrentWeapon.DisableHitBox();
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (_attack.ComboStateIndex == -1) return;

        if (normalizedTime < _attack.ComboAttackTime) return;

        if (CanAttack(_attack.ComboStateIndex))
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, _attack.ComboStateIndex));
        }
    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) return;

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * _attack.Force);
        _alreadyAppliedForce = true;
    }
}