using UnityEngine;

public abstract class CollectableItem : MonoBehaviour
{
    [SerializeField] protected Item item;

    protected abstract void OnCollect();

    public void Collect()
    {
        OnCollect();
        Destroy(gameObject);
    }
}