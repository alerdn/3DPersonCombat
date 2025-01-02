using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private float _attackDelay;
    private bool _isAttacking = false;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        FacePlayer();
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);

        _attackDelay = Random.Range(.2f, 1f);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(SpeedHash, 0f, .1f, deltaTime);

        _attackDelay = Mathf.Max(_attackDelay - Time.deltaTime, 0);

        if (_attackDelay == 0 && !_isAttacking)
        {
            _isAttacking = true;
            stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback, UnitType.Player);
            stateMachine.Animator.CrossFadeInFixedTime(AttackHash, .1f);
        }

        if (_isAttacking && GetNormalizedTime(stateMachine.Animator, "Attack") >= 1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

    }

    public override void Exit()
    {
    }
}