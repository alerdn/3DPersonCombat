using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record ItemInventory
{
    public event Action<int> OnQuantityChanged;

    public Item Item;
    public int Quantity
    {
        get => _quantity; set
        {
            _quantity = value;
            OnQuantityChanged?.Invoke(_quantity);
        }
    }

    private int _quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemInventory CurrentItem
    {
        get
        {
            if (_currentItemIndex >= _items.Count) return null;
            return _items[_currentItemIndex];
        }
    }

    public int Souls { get => _souls.Value; set => _souls.Value = Mathf.Max(value, 0); }

    [SerializeField] private List<ItemInventory> _items = new List<ItemInventory>();
    [SerializeField] private SOInt _souls;

    [Header("Heal")]
    [SerializeField] private int _healItemQuantity = 10;

    private int _currentItemIndex;

    private void Start()
    {
        _souls.Value = 0;
    }

    public void UseItem()
    {
        if (CurrentItem == null || CurrentItem.Quantity == 0) return;

        CurrentItem.Item.Use();
        CurrentItem.Quantity--;
    }

    public void SwitchItem()
    {
        _currentItemIndex = (_currentItemIndex + 1) % _items.Count;
    }

    public ItemInventory GetHealItem()
    {
        foreach (ItemInventory item in _items)
        {
            if (item.Item is HealItem)
            {
                return item;
            }
        }

        return null;
    }

    public void ReplanishHealItem()
    {
        GetHealItem().Quantity = _healItemQuantity;
    }
}
