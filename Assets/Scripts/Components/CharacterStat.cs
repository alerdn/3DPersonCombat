using System;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public event Action<int> OnStrengthChanged;
    public event Action<int> OnVigorChanged;
    public event Action<int> OnFortitudeChanged;

    public int Strength
    {
        get => _strength; set
        {
            _strength = value;
            OnStrengthChanged?.Invoke(value);
        }
    }

    public int Vigor
    {
        get => _vigor; set
        {
            _vigor = value;
            OnVigorChanged?.Invoke(value);
        }
    }

    public int Fortitude
    {
        get => _fortitude; set
        {
            _fortitude = value;
            OnFortitudeChanged?.Invoke(value);
        }
    }

    [SerializeField] private int _strength = 10;
    [SerializeField] private int _vigor = 10;
    [SerializeField] private int _fortitude = 10;

}