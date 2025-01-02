using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    private float _respawnDelay = 4f;

    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        AudioManager.Instance.PlayCue("GameOver");

        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.CurrentWeapon.gameObject.SetActive(false);

        stateMachine.DropSouls();
    }

    public override void Tick(float deltaTime)
    {
        _respawnDelay -= deltaTime;

        if (_respawnDelay <= 0)
        {
            stateMachine.Respawn();
        }
    }

    public override void Exit()
    {

    }
}