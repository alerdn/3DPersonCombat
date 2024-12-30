using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnDie;

    [SerializeField] private int _maxHealth = 100;

    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_health == 0) return;

        _health = Mathf.Max(_health - damage, 0);
        OnTakeDamage?.Invoke();

        if (_health == 0)
        {
            OnDie?.Invoke();
        }

        Debug.Log($"{name} Health: {_health}");
    }
}