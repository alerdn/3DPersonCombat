using UnityEngine;

public enum DrinkType
{
    EstusFlask,
    AshenFlask
}

public class PlayerDrinkingState : PlayerBaseState
{
    private readonly int DrinkHash = Animator.StringToHash("Drink");

    private DrinkType _drinkType;

    public PlayerDrinkingState(PlayerStateMachine stateMachine, DrinkType drinkType) : base(stateMachine)
    {
        _drinkType = drinkType;
    }

    public override void Enter()
    {
        AudioManager.Instance.PlayCue("Drink");

        switch (_drinkType)
        {
            case DrinkType.EstusFlask:
                stateMachine.EstusFlask.SetActive(true);
                break;
            case DrinkType.AshenFlask:
                stateMachine.AshenFlask.SetActive(true);
                break;
        }
        stateMachine.Animator.CrossFadeInFixedTime(DrinkHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Drink") < 1f)
        {
            return;
        }

        switch (_drinkType)
        {
            case DrinkType.EstusFlask:
                stateMachine.Health.RestoreHealth(300);
                break;
            case DrinkType.AshenFlask:
                stateMachine.Mana.RestoreMana(300);
                break;
        }

        ReturnToLocomotion();
    }

    public override void Exit()
    {
        switch (_drinkType)
        {
            case DrinkType.EstusFlask:
                stateMachine.EstusFlask.SetActive(false);
                break;
            case DrinkType.AshenFlask:
                stateMachine.AshenFlask.SetActive(false);
                break;
        }
    }
}