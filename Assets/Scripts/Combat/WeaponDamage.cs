using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum UnitType
{
    None,
    Player,
    Enemy
}

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider _myCollider;
    [SerializeField] private UnitType _targets;

    private List<Collider> _alreadyCollidedWith = new List<Collider>();
    private int _damage;
    private float _knockback;

    private void OnEnable()
    {
        _alreadyCollidedWith.Clear();
    }

    public void SetAttack(int damage, float knockback, UnitType targets)
    {
        _damage = damage;
        _knockback = knockback;
        _targets = targets;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _myCollider) return;

        if (!other.tag.TryToEnum(out UnitType unitType) || !_targets.HasFlag(unitType))
        {
            return;
        }

        if (_alreadyCollidedWith.Contains(other)) return;
        _alreadyCollidedWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            AudioManager.Instance.PlayCue("Attack");
        }

        if (other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            if (!forceReceiver.Pusheable) return;
            
            Vector3 direction = (other.transform.position - _myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * _knockback);
        }

    }
}
