using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public bool IsTwoHanded { get; private set; }
    [field: SerializeField] public bool IsRanged { get; private set; }
    [field: SerializeField] public Vector3 EquipOffset { get; private set; }
    [field: SerializeField] public Vector3 EquipRotation { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    [SerializeField] private WeaponDamage _hitBox;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _firePoint;

    public void EnableHitBox()
    {
        _hitBox?.gameObject.SetActive(true);
    }

    public void DisableHitBox()
    {
        _hitBox?.gameObject.SetActive(false);
    }

    public void SetAttack(int damage, float knockback)
    {
        _hitBox?.SetAttack(damage, knockback);
    }

    public void Fire(Collider myCollider, Vector3 targetPosition, int damage, float knockback)
    {
        Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity)
            .Init(myCollider, targetPosition, damage, knockback);
    }
}