using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnDie;
    public event Action<int, int> OnHealthChanged;
    public event Action<int> OnMaxHealthChanged;

    public bool IsDead => _health == 0;
    public int CurrentMaxHealth
    {
        get => _currentMaxHealth;
        private set
        {
            _currentMaxHealth = value;
            OnMaxHealthChanged?.Invoke(_currentMaxHealth);
        }
    }
    public int InitialMaxHealth => _initialMaxHealth;
    public int VigorMultiplier => _vigorMultiplier;
    public int CurrentHealth
    {
        get => _health; private set
        {
            if (value == _health) return;

            _health = value;
            OnHealthChanged?.Invoke(_health, CurrentMaxHealth);
        }
    }

    [SerializeField] private int _initialMaxHealth = 100;
    [SerializeField] private int _vigorMultiplier = 5;

    [Header("Debug")]
    [SerializeField] private int _health;
    [SerializeField] private int _currentMaxHealth;

    private Stamina _stamina;
    private bool _isBlocking;
    private bool _isInvulnerable;

    private void Start()
    {
        CurrentMaxHealth = InitialMaxHealth;
        CurrentHealth = CurrentMaxHealth;
    }

    public void SetMaxHealth(int vigor, bool restoreHealth = false)
    {
        CurrentMaxHealth = GetMaxHealthByVigor(vigor);
        if (restoreHealth) CurrentHealth = CurrentMaxHealth;
    }

    public int GetMaxHealthByVigor(int vigor)
    {
        return InitialMaxHealth + vigor * VigorMultiplier;
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

    public void RestoreHealth(int heal = 0)
    {
        if (heal == 0) CurrentHealth = CurrentMaxHealth;
        else CurrentHealth = Mathf.Min(CurrentHealth + heal, CurrentMaxHealth);
    }
}