using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int StaminaCost { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    [SerializeField] private int _rawDamage;
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
        return Mathf.RoundToInt(_rawDamage + strength * 10);
    }

    public int GetAttackDamage(int attackIndex, int strength)
    {
        int baseDamage = GetDamageBase(strength);
        float damageMultiplier = Attacks[attackIndex].DamageMultiplier;

        return Mathf.RoundToInt(baseDamage * damageMultiplier);
    }
}