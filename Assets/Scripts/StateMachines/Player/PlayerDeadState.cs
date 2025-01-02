using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.CurrentWeapon.gameObject.SetActive(false);

        //TODO: dropar almas do player
        //TODO: reiniciar o player, mas não a fase toda
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}