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
    private ItemInventory CurrentItem
    {
        get
        {
            if (_currentItemIndex >= _items.Count) return null;
            return _items[_currentItemIndex];
        }
    }

    public int Souls { get => _souls; set => _souls = Mathf.Max(value, 0); }

    [SerializeField] private List<ItemInventory> _items = new List<ItemInventory>();

    private int _currentItemIndex;

    [Header("Debug")]
    [SerializeField] private int _souls;

    public void UseItem()
    {
        if (CurrentItem == null || CurrentItem.Quantity == 0) return;

        CurrentItem.Item.Use();
        CurrentItem.Quantity--;
    }

    public void SwitchItem()
    {
        _currentItemIndex = (_currentItemIndex + 1) % _items.Count;
        Debug.Log($"Current item: {CurrentItem}");
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
        GetHealItem().Quantity = 10;
    }
}
