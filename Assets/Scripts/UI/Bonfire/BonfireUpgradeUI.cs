using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonfireUpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject _frame;
    [SerializeField] private List<WeaponUpgradeSlotUI> _weaponUpgradeSlots;

    private BonfireUI _bonfireUI;
    private PlayerStateMachine _player;

    public void Init(PlayerStateMachine player, BonfireUI bonfireUI)
    {
        _bonfireUI = bonfireUI;
        _player = player;

        for (int i = 0; i < _player.PrimaryWeapons.Count(); i++)
        {
            _weaponUpgradeSlots[i].Init(_player.PrimaryWeapons[i]);
        }
    }

    public void ShowUpgradeFrame()
    {
        foreach (var weaponUpgradeSlot in _weaponUpgradeSlots)
        {
            weaponUpgradeSlot.UpdateUI();
        }

        _frame.SetActive(true);
    }

    public void OnClick_CancelUpgrade()
    {
        _bonfireUI.ShowMainFrame();
        HideFrame();
    }

    public void HideFrame()
    {
        _frame.SetActive(false);
    }
}