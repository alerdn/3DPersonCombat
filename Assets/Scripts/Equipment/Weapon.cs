using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action OnLevelChanged;

    public int Level
    {
        get => _level; private set
        {
            _level = value;
            OnLevelChanged?.Invoke();
        }
    }

    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; } = 20;
    [field: SerializeField] public int StaminaCost { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    [SerializeField] private int _rawDamage;
    [SerializeField] private int _level;
    [SerializeField] private WeaponDamage _hitBox;

    public void EnableHitBox()
    {
        _hitBox.gameObject.SetActive(true);
    }

    public void DisableHitBox()
    {
        _hitBox.gameObject.SetActive(false);
    }

    public void SetAttack(int damage, float knockback, UnitType targets)
    {
        _hitBox.SetAttack(damage, knockback, targets);
    }

    public int GetDamageBase(int strength)
    {
        return Mathf.RoundToInt(_rawDamage + (Level * strength) + ((strength - 10) * (Level + 1)));
    }

    public int GetAttackDamage(int attackIndex, int strength = 0)
    {
        int baseDamage = GetDamageBase(strength);
        float damageMultiplier = Attacks[attackIndex].DamageMultiplier;

        return Mathf.RoundToInt(baseDamage * damageMultiplier);
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