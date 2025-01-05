using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;

    private Stamina _stamina;
    private RectTransform _rectTransform;
    private float _originalWidth;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _originalWidth = _rectTransform.sizeDelta.x;
        
        PlayerStateMachine player = PlayerStateMachine.Instance;
        _stamina = player.Stamina;

        _stamina.OnStaminaChanged += UpdateUI;

        player.CharacterStat.OnEnduranceChanged += UpdateStaminaBarSize;
        UpdateStaminaBarSize(player.CharacterStat.Endurance);

    }

    private void UpdateUI(float stamina, float maxStamina)
    {
        _staminaBar.fillAmount = stamina / maxStamina;
    }

    private void UpdateStaminaBarSize(int endurance)
    {
        _rectTransform.sizeDelta = new Vector2(_originalWidth + endurance * 10, _rectTransform.sizeDelta.y);
        UpdateUI(_stamina.CurrentStamina, _stamina.MaxStamina);
    }
}