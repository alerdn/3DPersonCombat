using UnityEngine;

public class PlayerCastingState : PlayerBaseState
{
    private string _attackAnimationName;

    public PlayerCastingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        Staff staff = stateMachine.CurrentWeapon as Staff;
        Target target = stateMachine.Targeter.CurrentTarget;
        Health targetHealth = null;

        if (target)
        {
            targetHealth = stateMachine.Targeter.CurrentTarget.GetComponent<Health>();
        }

        Spell spell = stateMachine.Spellbook.CurrentSpell;
        _attackAnimationName = spell.AnimationName;

        staff.SetAttack(spell, targetHealth, stateMachine.CharacterStat.Intelligence);
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_attackAnimationName, .1f);
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

        if (normalizedTime >= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
    }
}