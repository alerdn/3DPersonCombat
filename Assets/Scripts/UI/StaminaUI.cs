using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;

    private PlayerStateMachine _player;
    private Stamina _stamina;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _player = PlayerStateMachine.Instance;
        _stamina = _player.Stamina;

        _stamina.OnStaminaChanged += UpdateUI;
        _player.Stamina.OnMaxStaminaChanged += UpdateStaminaBarSize;

        UpdateStaminaBarSize(_player.Stamina.CurrentMaxStamina);
    }

    private void OnDestroy()
    {
        _stamina.OnStaminaChanged -= UpdateUI;
        _player.Stamina.OnMaxStaminaChanged -= UpdateStaminaBarSize;
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