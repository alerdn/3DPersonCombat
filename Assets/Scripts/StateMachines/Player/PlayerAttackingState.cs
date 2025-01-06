using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack _attack;
    private bool _alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        Weapon weapon = stateMachine.CurrentWeapon;
        _attack = weapon.Attacks[attackIndex];

        int damage = weapon.GetAttackDamage(attackIndex, stateMachine.CharacterStat.Strength);

        weapon.SetAttack(damage, _attack.Knockback, UnitType.Enemy);
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