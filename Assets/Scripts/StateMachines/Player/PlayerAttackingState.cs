using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack _attack;
    private bool _alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        Weapon weapon = stateMachine.CurrentWeapon;
        if (weapon)
        {
            _attack = weapon.Attacks[attackIndex];
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

        float normalizedTime = GetNormalizedTime();

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

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfop = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfop.IsTag("Attack"))
        {
            return currentInfop.normalizedTime;
        }
        else
        {
            return 0f;
        }
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