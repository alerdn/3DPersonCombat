using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private Attack _attack;
    private float _transitionDelay;
    private bool _isTransitioning;

    public EnemyAttackingState(EnemyStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        MeleeWeapon weapon = stateMachine.Weapon;
        _attack = weapon.Attacks[attackIndex];

        int damage = weapon.GetAttackDamage(attackIndex);
        weapon.SetAttack(damage, _attack.Knockback, UnitType.Player);

        _transitionDelay = Random.Range(.2f, 1f);
    }

    public override void Enter()
    {
        FacePlayer();
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, .1f);
        AudioManager.Instance.PlayCue("Swing");
    }

    public override void Tick(float deltaTime)
    {

        if (!_isTransitioning && GetNormalizedTime(stateMachine.Animator, "Attack") >= 1f)
        {
            _isTransitioning = true;
            stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, .1f);
        }

        if (_isTransitioning)
        {
            stateMachine.Animator.SetFloat(SpeedHash, 0f, .1f, deltaTime);
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