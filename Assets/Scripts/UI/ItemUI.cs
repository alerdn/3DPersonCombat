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
                break;
            default:
                break;
        }
    }

    private void OnWeaponSwitched(Weapon weapon)
    {
        _icon.color = Color.white;
        _icon.sprite = weapon.Sprite;
        _quantity.text = "";
    }

    private void OnShieldSwitched(Weapon weapon)
    {
        _icon.color = Color.white;
        _icon.sprite = weapon.Sprite;
        _quantity.text = "";
    }

    private void OnItemSwitched(ItemInventory item)
    {
        _icon.sprite = item.Item.Sprite;
        _quantity.text = item.Quantity.ToString();
        if (item.Quantity == 0)
        {
            _icon.color = Color.gray;
        }
        else
        {
            _icon.color = Color.white;
        }
    }
}