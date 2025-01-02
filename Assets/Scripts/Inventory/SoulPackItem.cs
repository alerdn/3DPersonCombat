using UnityEngine;

[CreateAssetMenu(fileName = "Soul Pack Item", menuName = "Inventory/Soul Pack Item")]
public class SoulPackItem : Item
{
    public override void Use()
    {
        Debug.Log("Using Soul Pack");
    }
}