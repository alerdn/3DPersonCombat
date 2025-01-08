using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeSlotUI : MonoBehaviour
{
    [SerializeField] private Image _spriteImage;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _damge;
    [SerializeField] private TMP_Text _upgradeCost;

    private Weapon _weapon;

    public void Init(Weapon weapon)
    {
        _weapon = weapon;
        _weapon.OnLevelChanged += UpdateUI;
    }

    public void UpdateUI()
    {
        _spriteImage.sprite = _weapon.Sprite;
        _name.text = $"{_weapon.Name} + {_weapon.Level}";
        _damge.text = _weapon.GetDamageBase(PlayerStateMachine.Instance.CharacterStat.Strength).ToString();
        _upgradeCost.text = _weapon.GetUpgradeCost().ToString();
    }

    public void OnClick_Upgrade()
    {
        _weapon.Upgrade();
    }
}
