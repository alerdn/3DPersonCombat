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

    private void Start()
    {
        CurrentStamina = _maxStamina;
    }

    private void Update()
    {
        _remainingRecoveryTime = Mathf.Max(_remainingRecoveryTime - Time.deltaTime, 0);

        if (_remainingRecoveryTime == 0)
        {
            CurrentStamina = Mathf.Min(CurrentStamina + (_recoveryRate * Time.deltaTime), _maxStamina);
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
}