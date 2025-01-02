using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Item CurrentItem
    {
        get
        {
            if (_currentItemIndex >= _items.Count) return null;
            return _items[_currentItemIndex];
        }
    }

    [SerializeField] private List<Item> _items = new List<Item>();

    private int _currentItemIndex;

    public void UseItem()
    {
        CurrentItem?.Use();
    }

    public void SwitchItem()
    {
        _currentItemIndex = (_currentItemIndex + 1) % _items.Count;
        Debug.Log($"Current item: {CurrentItem}");
    }
}
