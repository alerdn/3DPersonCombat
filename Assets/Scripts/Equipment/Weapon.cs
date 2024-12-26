using System;
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

    public void SetDamage(int damage)
    {
        _hitBox?.SetDamage(damage);
    }

    public void Fire(Vector3 targetPosition, int damage)
    {
        Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity)
            .Init(targetPosition, damage);
    }
}