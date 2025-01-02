using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnDie;
    public event Action<int, int> OnHealthChanged;

    public bool IsDead => _health == 0;
    public int CurrentHealth
    {
        get => _health; private set
        {
            if (value == _health) return;

            _health = value;
            OnHealthChanged?.Invoke(_health, _maxHealth);
        }
    }

    [SerializeField] private int _maxHealth = 100;

    private int _health;
    private bool _isBlocking;
    private Stamina _stamina;
    private bool _isInvulnerable;

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void SetBlocking(bool isBlocking)
    {
        _isBlocking = isBlocking;
        _stamina.SetBlocking(isBlocking);
    }

    public void SetStaminaComponent(Stamina stamina)
    {
        _stamina = stamina;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth == 0 || _isInvulnerable) return;

        if (_isBlocking && _stamina)
        {
            damage = Mathf.RoundToInt(_stamina.BlockAttack(damage));
            if (damage == 0) return;
        }

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        OnTakeDamage?.Invoke();

        if (CurrentHealth == 0)
        {
            OnDie?.Invoke();
        }
    }

    public void RestoreHealth(int heal)
    {
        if (CurrentHealth == 0) return;

        CurrentHealth = Mathf.Min(CurrentHealth + heal, _maxHealth);
    }
}