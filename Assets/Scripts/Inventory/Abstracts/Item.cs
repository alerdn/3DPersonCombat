using UnityEngine;

public abstract class Item : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }

    public abstract void Use();
}