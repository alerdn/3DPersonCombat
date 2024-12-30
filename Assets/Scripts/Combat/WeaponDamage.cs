using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider _myCollider;
    [SerializeField] private bool _destroyOnHit;

    private List<Collider> _alreadyCollidedWith = new List<Collider>();
    private int _damage;
    private float _knockback;

    private void OnEnable()
    {
        _alreadyCollidedWith.Clear();
    }

    public void SetAttack(int damage, float knockback)
    {
        _damage = damage;
        _knockback = knockback;
    }

    public void SetMyCollider(Collider myCollider)
    {
        _myCollider = myCollider;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _myCollider) return;

        if (_alreadyCollidedWith.Contains(other)) return;
        _alreadyCollidedWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }

        if (other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - _myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * _knockback);
        }

        if (_destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
