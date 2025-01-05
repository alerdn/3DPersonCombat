using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOInt", menuName = "SO/Int")]
public class SOInt : ScriptableObject
{
    public event Action<int> OnValueChanged;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }

    [SerializeField] private int _value;
    [SerializeField] private bool _resetOnEnable;

    private void OnEnable()
    {
        if (_resetOnEnable) Value = 0;
    }
}