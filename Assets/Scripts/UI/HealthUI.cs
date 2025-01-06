using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private UnitType _unitType;
    [SerializeField] private Image _healthBar;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (_unitType == UnitType.Player)
        {
            PlayerStateMachine player = PlayerStateMachine.Instance;
            _health = player.Health;

            player.Health.OnMaxHealthChanged += UpdateHealthBarSize;
            UpdateHealthBarSize(player.Health.CurrentMaxHealth);
        }

        _health.OnHealthChanged += UpdateUI;
    }

    private void UpdateUI(int health, int maxHealth)
    {
        _healthBar.fillAmount = (float)health / (float)maxHealth;
    }

    private void UpdateHealthBarSize(int maxHealth)
    {
        _rectTransform.sizeDelta = new Vector2(maxHealth, _rectTransform.sizeDelta.y);
        UpdateUI(_health.CurrentHealth, _health.CurrentMaxHealth);
    }
}