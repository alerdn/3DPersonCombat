using System;
using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public event Action<float, float> OnStaminaChanged;

    public float MaxStamina => _currentMaxStamina;
    public float CurrentStamina
    {
        get => _stamina; private set
        {
            if (value == _stamina) return;

            _stamina = value;
            OnStaminaChanged?.Invoke(_stamina, _currentMaxStamina);
        }
    }

    [SerializeField] private float _initialMaxStamina = 100f;
    [SerializeField] private float _recoveryTime = 1f;
    [SerializeField] private float _recoveryRate = 20;

    [Header("Debug")]
    [SerializeField] private float _stamina;
    [SerializeField] private float _currentMaxStamina;

    private float _remainingRecoveryTime;
    private bool _isBlocking;

    private void Start()
    {
        _currentMaxStamina = _initialMaxStamina;
        CurrentStamina = _currentMaxStamina;
    }

    private void Update()
    {
        _remainingRecoveryTime = Mathf.Max(_remainingRecoveryTime - Time.deltaTime, 0);

        if (_remainingRecoveryTime == 0)
        {
            float staminaPerFrame = _recoveryRate * Time.deltaTime;
            if (_isBlocking)
            {
                staminaPerFrame *= .25f;
            }

            CurrentStamina = Mathf.Min(CurrentStamina + staminaPerFrame, _currentMaxStamina);
        }
    }

    public void SetMaxStamina(float multiplier, bool restoreStamina = false)
    {
        _currentMaxStamina = _initialMaxStamina * multiplier;
        if (restoreStamina) CurrentStamina = _currentMaxStamina;
    }

    public bool TryUseStamina(float amount)
    {
        if (CurrentStamina < amount)
        {
            return false;
        }

        CurrentStamina = Mathf.Max(CurrentStamina - amount, 0);
        _remainingRecoveryTime = _recoveryTime;
        return true;
    }

    public float BlockAttack(float damage)
    {
        _remainingRecoveryTime = _recoveryTime;

        if (CurrentStamina < damage)
        {
            float remainingDamage = damage - CurrentStamina;
            CurrentStamina = 0;
            return remainingDamage;
        }

        CurrentStamina -= damage;
        return 0;
    }

    public void SetBlocking(bool isBlocking)
    {
        _isBlocking = isBlocking;
    }
}