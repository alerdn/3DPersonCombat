using System;
using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public event Action<float, float> OnStaminaChanged;
    public event Action<float> OnMaxStaminaChanged;

    public float CurrentMaxStamina
    {
        get => _currentMaxStamina; private set
        {
            _currentMaxStamina = value;
            OnMaxStaminaChanged?.Invoke(_currentMaxStamina);
        }
    }
    public float InitialMaxStamina => _initialMaxStamina;
    public float EnduranceMultiplier => _enduranceMultiplier;
    public float CurrentStamina
    {
        get => _stamina; private set
        {
            if (value == _stamina) return;

            _stamina = value;
            OnStaminaChanged?.Invoke(_stamina, CurrentMaxStamina);
        }
    }

    [SerializeField] private float _initialMaxStamina = 100f;
    [SerializeField] private float _enduranceMultiplier = 2f;
    [SerializeField] private float _recoveryTime = 1f;
    [SerializeField] private float _recoveryRate = 20;

    [Header("Debug")]
    [SerializeField] private float _stamina;
    [SerializeField] private float _currentMaxStamina;

    private float _remainingRecoveryTime;
    private bool _isBlocking;

    private void Start()
    {
        CurrentMaxStamina = InitialMaxStamina;
        CurrentStamina = CurrentMaxStamina;
    }

    private void Update()
    {
        _remainingRecoveryTime = Mathf.Max(_remainingRecoveryTime - Time.deltaTime, 0);

        if (_remainingRecoveryTime == 0)
        {
            float staminaPerFrame = CurrentMaxStamina * (_recoveryRate / 100f) * Time.deltaTime;
            if (_isBlocking)
            {
                staminaPerFrame *= .25f;
            }

            CurrentStamina = Mathf.Min(CurrentStamina + staminaPerFrame, CurrentMaxStamina);
        }
    }

    public void SetMaxStamina(float endurance, bool restoreStamina = false)
    {
        CurrentMaxStamina = GetMaxStaminaByEndurance(endurance);
        if (restoreStamina) CurrentStamina = CurrentMaxStamina;
    }

    public float GetMaxStaminaByEndurance(float endurance)
    {
        return InitialMaxStamina + endurance * EnduranceMultiplier;
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