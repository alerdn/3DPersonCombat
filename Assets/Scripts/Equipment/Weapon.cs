using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int StaminaCost { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

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
}