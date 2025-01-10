using UnityEngine;

public class SoulCollectableItem : CollectableItem
{
    private int _souls;

    public void Init(int souls)
    {
        _souls = souls;
    }

    protected override void OnCollect()
    {
        AudioManager.Instance.PlayCue("SoulRecovered");
        PlayerStateMachine.Instance.Inventory.RecoverSouls(_souls);
    }
}