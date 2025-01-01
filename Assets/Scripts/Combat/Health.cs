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
    private bool _isInvulnerable;
    private Stamina _stamina;

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;
    }

    public void SetStaminaComponent(Stamina stamina)
    {
        _stamina = stamina;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth == 0) return;

        if (_isInvulnerable && _stamina)
        {
            // Tem stamina suficiente para bloquear o dano
            if (_stamina.TryUseStamina(damage))
            {
                return;
            }
        }

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        OnTakeDamage?.Invoke();

        if (CurrentHealth == 0)
        {
            OnDie?.Invoke();
        }
    }
}