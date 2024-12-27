using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private string _myTag = "Player";
    [SerializeField] private bool _destroyOnHit;

    private List<Collider> _alreadyCollidedWith = new List<Collider>();
    private int _damage;

    private void OnEnable()
    {
        _alreadyCollidedWith.Clear();
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_myTag)) return;

        if (_alreadyCollidedWith.Contains(other)) return;
        _alreadyCollidedWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }

        if (_destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
