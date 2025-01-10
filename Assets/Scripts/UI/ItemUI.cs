using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    None,
    Weapon,
    Shield,
    Consumable,
    Spell
}

public class ItemUI : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _quantity;

    private void Start()
    {
        PlayerStateMachine player = PlayerStateMachine.Instance;

        switch (_type)
        {
            case ItemType.Weapon:
                player.OnWeaponSwitched += OnWeaponSwitched;
                OnWeaponSwitched(player.CurrentWeapon);
                break;
            case ItemType.Shield:
                player.OnShieldSwitched += OnShieldSwitched;
                OnShieldSwitched(player.CurrentShield);
                break;
            case ItemType.Consumable:
                player.OnItemSwitched += OnItemSwitched;
                player.Inventory.OnQuantityChanged += UpdateQuantity;

                OnItemSwitched(player.Inventory.CurrentItem);
                UpdateQuantity(player.Inventory.CurrentItem.Quantity);
                break;
            case ItemType.Spell:
                player.OnSpellSwitched += OnSpellSwitched;
                player.OnWeaponSwitched += ToggleSpellOnWeaponSwitched;

                OnSpellSwitched(player.Spellbook.CurrentSpell);
                ToggleSpellOnWeaponSwitched(player.CurrentWeapon);
                break;
            default:
                break;
        }

    }

    private void OnWeaponSwitched(Weapon weapon)
    {
        _icon.color = Color.white;
        _icon.sprite = weapon.Sprite;
    }

    private void OnShieldSwitched(Shield shield)
    {
        _icon.color = Color.white;
        _icon.sprite = shield.Sprite;
    }

    private void OnItemSwitched(ItemInventory item)
    {
        _icon.sprite = item.Item.Sprite;
        _quantity.text = item.Quantity.ToString();
    }

    private void UpdateQuantity(int quantity)
    {
        if (quantity == 0)
        {
            _quantity.text = "";
            _icon.color = Color.gray;
        }
        else
        {
            _quantity.text = quantity.ToString();
            _icon.color = Color.white;
        }
    }

    private void OnSpellSwitched(Spell spell)
    {
        _icon.color = Color.white;
        _icon.sprite = spell.Sprite;
    }

    private void ToggleSpellOnWeaponSwitched(Weapon weapon)
    {
        if (weapon is Staff)
        {
            _icon.color = Color.white;
        }
        else
        {
            _icon.color = Color.gray;
        }
    }
}