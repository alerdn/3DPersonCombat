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

        player.Stat.OnFortitudeChanged += (fortitude) =>
        {
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x + fortitude, _rectTransform.sizeDelta.y);
            UpdateUI(_stamina.CurrentStamina, _stamina.MaxStamina);
        };
    }

    private void UpdateUI(float stamina, float maxStamina)
    {
        _staminaBar.fillAmount = stamina / maxStamina;
    }
}