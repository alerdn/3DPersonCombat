using System;
using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public event Action<float, float> OnStaminaChanged;

    public float CurrentStamina
    {
        get => _stamina; private set
        {
            if (value == _stamina) return;

            _stamina = value;
            OnStaminaChanged?.Invoke(_stamina, _maxStamina);
        }
    }

    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _recoveryTime = 1f;
    [SerializeField] private float _recoveryRate = 20;

    private float _stamina;
    private float _remainingRecoveryTime;
    private bool _isBlocking;

    private void Start()
    {
        CurrentStamina = _maxStamina;
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

            CurrentStamina = Mathf.Min(CurrentStamina + staminaPerFrame, _maxStamina);
        }
    }

    public bool TryUseStamina(int amount)
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