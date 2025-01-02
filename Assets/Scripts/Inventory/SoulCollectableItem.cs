using UnityEngine;

public class SoulCollectableItem : CollectableItem
{
    private int _souls;

    public void Init(int souls)
    {
        _souls = souls;
    }

    public override void Collect()
    {
        AudioManager.Instance.PlayCue("SoulRecovered");
        PlayerStateMachine.Instance.Inventory.Souls += _souls;
        Destroy(gameObject);
    }
}