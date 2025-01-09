using System;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public event Action<float, float> OnManaChanged;
    public event Action<float> OnMaxManaChanged;

    public float CurrentMaxMana
    {
        get => _currentMaxMana; private set
        {
            _currentMaxMana = value;
            OnMaxManaChanged?.Invoke(_currentMaxMana);
        }
    }
    public float InitialMaxMana => _initialMaxMana;
    public float MindMultiplier => _mindMultiplier;
    public float CurrentMana
    {
        get => _mana; private set
        {
            if (value == _mana) return;

            _mana = value;
            OnManaChanged?.Invoke(_mana, CurrentMaxMana);
        }
    }

    [SerializeField] private float _initialMaxMana = 100f;
    [SerializeField] private float _mindMultiplier = 10f;
    [SerializeField] private float _recoveryTime = 1f;
    [SerializeField] private float _recoveryRate = 1f;

    [Header("Debug")]
    [SerializeField] private float _mana;
    [SerializeField] private float _currentMaxMana;

    private float _remainingRecoveryTime;

    private void Start()
    {
        CurrentMaxMana = InitialMaxMana;
        CurrentMana = CurrentMaxMana;
    }

    private void Update()
    {
        _remainingRecoveryTime = Mathf.Max(_remainingRecoveryTime - Time.deltaTime, 0);

        if (_remainingRecoveryTime == 0)
        {
            float manaPerFrame = CurrentMaxMana * (_recoveryRate / 100f) * Time.deltaTime;
            CurrentMana = Mathf.Min(CurrentMana + manaPerFrame, CurrentMaxMana);
        }
    }

    public void SetMaxMana(float mind, bool restoreMana = false)
    {
        CurrentMaxMana = GetMaxManaByMind(mind);
        if (restoreMana) CurrentMana = CurrentMaxMana;
    }

    public float GetMaxManaByMind(float mind)
    {
        return InitialMaxMana + mind * MindMultiplier;
    }

    public bool TryUseMana(float amount)
    {
        if (CurrentMana < amount)
        {
            return false;
        }

        CurrentMana = Mathf.Max(CurrentMana - amount, 0);
        _remainingRecoveryTime = _recoveryTime;
        return true;
    }

    public void RestoreMana(float mana = 0)
    {
        if (mana == 0) CurrentMana = CurrentMaxMana;
        else CurrentMana = Mathf.Min(CurrentMana + mana, CurrentMaxMana);
    }
}