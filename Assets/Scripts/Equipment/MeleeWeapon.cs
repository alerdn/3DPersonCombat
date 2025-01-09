using System;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [field: SerializeField] public Attack[] Attacks { get; protected set; }

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

    public int GetAttackDamage(int attackIndex, int strength = 0)
    {
        int baseDamage = GetDamageBase(strength);
        float damageMultiplier = Attacks[attackIndex].DamageMultiplier;

        return Mathf.RoundToInt(baseDamage * damageMultiplier);
    }
}