using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _recoveryTime = 1f;
    [SerializeField] private float _recoveryRate = 20;

    private float _stamina;
    private float _remainingRecoveryTime;

    private void Start()
    {
        _stamina = _maxStamina;
    }

    private void Update()
    {
        _remainingRecoveryTime = Mathf.Max(_remainingRecoveryTime - Time.deltaTime, 0);

        if (_remainingRecoveryTime == 0)
        {
            _stamina = Mathf.Min(_stamina + (_recoveryRate * Time.deltaTime), _maxStamina);
        }

        Debug.Log($"Stamina: {_stamina}");
    }

    public bool TryUseStamina(int amount)
    {
        if (_stamina < amount)
        {
            return false;
        }

        _stamina = Mathf.Max(_stamina - amount, 0);
        _remainingRecoveryTime = _recoveryTime;
        return true;
    }
}