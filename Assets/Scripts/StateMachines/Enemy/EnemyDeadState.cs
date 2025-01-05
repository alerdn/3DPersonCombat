using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
        stateMachine.Target.enabled = false;

        PlayerStateMachine.Instance.Inventory.Souls += stateMachine.SoulsValue;
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}