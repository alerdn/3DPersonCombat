using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private MeleeWeapon _weapon;
    private Attack _attack;
    private bool _alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex, bool isJumping = false) : base(stateMachine)
    {
        _weapon = stateMachine.CurrentWeapon as MeleeWeapon;
        _attack = _weapon.Attacks[attackIndex];

        int damage = _weapon.GetAttackDamage(attackIndex, stateMachine.CharacterStat.Strength);

        _weapon.SetAttack(damage, _attack.Knockback, UnitType.Enemy);

        if (isJumping)
        {
            stateMachine.ForceReceiver.Pusheable = false;
        }
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
        AudioManager.Instance.PlayCue("Swing");

        stateMachine.InputReader.DodgeEvent += OnDodge;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        // Ajuste na direção do ataque
        if (!stateMachine.Targeter.CurrentTarget && normalizedTime < .5f)
        {
            Vector3 movement = CalculateFreelookMovement();
            if (movement != Vector3.zero)
            {
                FaceMovementDirection(movement, deltaTime);
            }
        }

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
        _weapon.DisableHitBox();
        stateMachine.ForceReceiver.Pusheable = true;

        stateMachine.InputReader.DodgeEvent -= OnDodge;
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