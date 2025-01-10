using System;
using UnityEngine;

public enum DamageAttribute
{
    Strength,
    Intelligence
}

public class Weapon : MonoBehaviour
{
    public event Action OnLevelChanged;

    public int Level
    {
        get => _level; set
        {
            _level = value;
            OnLevelChanged?.Invoke();
        }
    }

    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public Sprite Sprite { get; protected set; }
    [field: SerializeField] public DamageAttribute DamageAttribute { get; protected set; }
    [field: SerializeField] public int MaxLevel { get; protected set; } = 20;
    [field: SerializeField] public int StaminaCost { get; protected set; }

    [SerializeField] private int _rawDamage;
    [SerializeField] private int _level;

    public int GetDamageBase(int attribute)
    {
        return Mathf.RoundToInt(_rawDamage + (Level * attribute) + ((attribute - 10) * (Level + 1)));
    }

    public int GetUpgradeCost()
    {
        return (Level + 1) * 100;
    }

    public void Upgrade()
    {
        if (PlayerStateMachine.Instance.Inventory.Souls >= GetUpgradeCost() && Level < MaxLevel)
        {
            PlayerStateMachine.Instance.Inventory.Souls -= GetUpgradeCost();
            Level++;
        }
    }
}