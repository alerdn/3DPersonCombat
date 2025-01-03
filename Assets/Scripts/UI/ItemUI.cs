using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    None,
    Weapon,
    Shield,
    Consumable
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
                break;
            case ItemType.Shield:
                player.OnShieldSwitched += OnShieldSwitched;
                break;
            case ItemType.Consumable:
                player.OnItemSwitched += OnItemSwitched;
                player.Inventory.CurrentItem.OnQuantityChanged += UpdateQuantity;
                UpdateQuantity(player.Inventory.CurrentItem.Quantity);
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

    private void OnShieldSwitched(Weapon weapon)
    {
        _icon.color = Color.white;
        _icon.sprite = weapon.Sprite;
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
}