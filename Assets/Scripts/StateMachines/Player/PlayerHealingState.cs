using UnityEngine;

public class PlayerHealingState : PlayerBaseState
{
    private readonly int HealHash = Animator.StringToHash("Heal");

    public PlayerHealingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        AudioManager.Instance.PlayCue("Heal");

        stateMachine.EstusFlask.SetActive(true);
        stateMachine.Animator.CrossFadeInFixedTime(HealHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Heal") < 1f)
        {
            return;
        }

        stateMachine.Health.RestoreHealth(50);
        ReturnToLocomotion();
    }

    public override void Exit()
    {
        stateMachine.EstusFlask.SetActive(false);
    }
}