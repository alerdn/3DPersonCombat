using UnityEngine;

[CreateAssetMenu(fileName = "Estus Flask Item", menuName = "Inventory/Estus Flask")]
public class EstusFlaskItem : Item
{
    public override void Use()
    {
        PlayerStateMachine _player = PlayerStateMachine.Instance;
        _player.SwitchState(new PlayerDrinkingState(_player, DrinkType.EstusFlask));
    }
}