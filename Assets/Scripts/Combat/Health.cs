using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnTakeDamage;

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
            Debug.Log("I'm dead!");
        }

        Debug.Log($"{name} Health: {_health}");
    }
}