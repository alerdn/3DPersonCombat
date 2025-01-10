using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private float _duration = 1f;

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.HasNoticedPlayer = true;
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        _duration -= deltaTime;

        if (_duration <= 0)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}