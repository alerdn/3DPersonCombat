using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record ItemInventory
{
    public Item Item;
    public int Quantity;
}

public class Inventory : MonoBehaviour
{
    public event Action<int> OnQuantityChanged;

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

    [Header("Potions")]
    [SerializeField] private int _potionQuantity = 10;

    private int _currentItemIndex;

    public void UseItem()
    {
        if (CurrentItem == null || CurrentItem.Quantity == 0) return;

        CurrentItem.Item.Use();
        CurrentItem.Quantity--;

        OnQuantityChanged?.Invoke(CurrentItem.Quantity);
    }

    public void SwitchItem()
    {
        _currentItemIndex = (_currentItemIndex + 1) % _items.Count;
    }

    public ItemInventory GetEstus()
    {
        foreach (ItemInventory item in _items)
        {
            if (item.Item is EstusFlaskItem)
            {
                return item;
            }
        }

        return null;
    }

    public ItemInventory GetAshen()
    {
        foreach (ItemInventory item in _items)
        {
            if (item.Item is AshenFlaskItem)
            {
                return item;
            }
        }

        return null;
    }

    public void ReplanishPotions()
    {
        GetEstus().Quantity = _potionQuantity;
        GetAshen().Quantity = _potionQuantity;

        OnQuantityChanged?.Invoke(CurrentItem.Quantity);
    }
}
