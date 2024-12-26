using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public bool IsTwoHanded { get; private set; }
    [field: SerializeField] public Vector3 EquipOffset { get; private set; }
    [field: SerializeField] public Vector3 EquipRotation { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
}