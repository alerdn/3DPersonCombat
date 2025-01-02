using UnityEngine;

[CreateAssetMenu(fileName = "Heal Item", menuName = "Inventory/Heal Item")]
public class HealItem : Item
{
    public override void Use()
    {
        PlayerStateMachine _player = PlayerStateMachine.Instance;
        _player.SwitchState(new PlayerHealingState(_player));
    }
}