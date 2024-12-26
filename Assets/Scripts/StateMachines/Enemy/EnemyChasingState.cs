using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("In Range");
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}