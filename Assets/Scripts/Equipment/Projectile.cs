using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private WeaponDamage _weaponDamage;
    private Vector3 _target;
    private float _time;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weaponDamage = GetComponent<WeaponDamage>();
    }

    public void Init(Vector3 targetPosition, int damage)
    {
        _target = targetPosition;
        _weaponDamage.SetDamage(damage);
        transform.LookAt(_target);
    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * 20f;

        _time += Time.deltaTime;
        if (_time > 5f)
        {
            Destroy(gameObject);
        }
    }
}