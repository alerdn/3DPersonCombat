using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;

    private Stamina _stamina;

    private void Start()
    {
        _stamina = PlayerStateMachine.Instance.Stamina;

        _stamina.OnStaminaChanged += UpdateUI;
    }

    private void UpdateUI(float stamina, float maxStamina)
    {
        _staminaBar.fillAmount = stamina / maxStamina;
    }
}