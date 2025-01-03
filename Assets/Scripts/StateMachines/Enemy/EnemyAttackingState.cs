using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private float _transitionDelay;
    private bool _isTransitioning;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        FacePlayer();

        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback, UnitType.Player);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, .1f);

        _transitionDelay = Random.Range(.2f, 1f);

        AudioManager.Instance.PlayCue("Swing");
    }

    public override void Tick(float deltaTime)
    {

        if (!_isTransitioning && GetNormalizedTime(stateMachine.Animator, "Attack") >= 1f)
        {
            _isTransitioning = true;
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);
            stateMachine.Animator.SetFloat(SpeedHash, 0f, .1f, deltaTime);
        }

        if (_isTransitioning)
        {
            _transitionDelay = Mathf.Max(_transitionDelay - Time.deltaTime, 0);

            if (_transitionDelay == 0)
            {
                stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            }
        }
    }

    public override void Exit()
    {
    }
}