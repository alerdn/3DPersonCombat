using UnityEngine;

[CreateAssetMenu(fileName = "Soul Pack Item", menuName = "Inventory/Soul Pack Item")]
public class SoulPackItem : Item
{
    [SerializeField] private int _souls;

    public override void Use()
    {
        PlayerStateMachine.Instance.Inventory.Souls += _souls;
    }
}