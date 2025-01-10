using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeSlotUI : MonoBehaviour
{
    [SerializeField] private Image _spriteImage;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _damge;
    [SerializeField] private TMP_Text _upgradeCost;

    private PlayerStateMachine _player;
    private Weapon _weapon;

    public void Init(Weapon weapon)
    {
        _player = PlayerStateMachine.Instance;
        _weapon = weapon;

        _weapon.OnLevelChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        _weapon.OnLevelChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        _spriteImage.sprite = _weapon.Sprite;
        _name.text = $"{_weapon.Name} + {_weapon.Level}";

        var attribute = _weapon.DamageAttribute switch
        {
            DamageAttribute.Strength => _player.CharacterStat.Strength,
            DamageAttribute.Intelligence => _player.CharacterStat.Intelligence,
            _ => 0,
        };

        _damge.text = _weapon.GetDamageBase(attribute).ToString();
        _upgradeCost.text = _weapon.GetUpgradeCost().ToString();
    }

    public void OnClick_Upgrade()
    {
        _weapon.Upgrade();
    }
}
