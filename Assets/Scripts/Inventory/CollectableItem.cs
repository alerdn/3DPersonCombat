using UnityEngine;

public abstract class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item _item;

    public abstract void Collect();
}