using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnDie;

    public bool IsDead => _health == 0;

    [SerializeField] private int _maxHealth = 100;

    private int _health;
    private bool _isInvulnerable;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;
    }

    public void TakeDamage(int damage)
    {
        if (_health == 0) return;

        if (_isInvulnerable) return;

        _health = Mathf.Max(_health - damage, 0);
        OnTakeDamage?.Invoke();

        if (_health == 0)
        {
            OnDie?.Invoke();
        }
    }
}