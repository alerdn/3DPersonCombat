using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;

    private Stamina _stamina;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        PlayerStateMachine player = PlayerStateMachine.Instance;
        _stamina = player.Stamina;

        _stamina.OnStaminaChanged += UpdateUI;

        player.Stamina.OnMaxStaminaChanged += UpdateStaminaBarSize;
        UpdateStaminaBarSize(player.Stamina.CurrentMaxStamina);

    }

    private void UpdateUI(float stamina, float maxStamina)
    {
        _staminaBar.fillAmount = stamina / maxStamina;
    }

    private void UpdateStaminaBarSize(float maxStamina)
    {
        _rectTransform.sizeDelta = new Vector2(maxStamina, _rectTransform.sizeDelta.y);
        UpdateUI(_stamina.CurrentStamina, _stamina.CurrentMaxStamina);
    }
}