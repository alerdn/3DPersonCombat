using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetDamage(stateMachine.AttackDamage);

        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, .1f);

    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime() >= 1f)
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
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
}