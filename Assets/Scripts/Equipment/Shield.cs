using UnityEngine;

public class Shield : MonoBehaviour
{
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public Sprite Sprite { get; protected set; }
}