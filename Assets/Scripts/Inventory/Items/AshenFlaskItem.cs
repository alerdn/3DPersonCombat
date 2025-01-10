using UnityEngine;

[CreateAssetMenu(fileName = "AshenFlaskItem", menuName = "Inventory/Ashen Flask", order = 0)]
public class AshenFlaskItem : Item
{
    public override void Use()
    {
        PlayerStateMachine _player = PlayerStateMachine.Instance;
        _player.SwitchState(new PlayerDrinkingState(_player, DrinkType.AshenFlask));
    }
}