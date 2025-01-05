using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthBar;

    private RectTransform _rectTransform;
    private float _originalWidth;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _originalWidth = _rectTransform.sizeDelta.x;

        if (_health == null)
        {
            PlayerStateMachine player = PlayerStateMachine.Instance;
            _health = player.Health;

            player.CharacterStat.OnVigorChanged += UpdateHealthBarSize;
            UpdateHealthBarSize(player.CharacterStat.Vigor);
        }

        _health.OnHealthChanged += UpdateUI;

    }

    private void UpdateUI(int health, int maxHealth)
    {
        _healthBar.fillAmount = (float)health / (float)maxHealth;
    }

    private void UpdateHealthBarSize(int vigor)
    {
        _rectTransform.sizeDelta = new Vector2(_originalWidth + vigor * 10, _rectTransform.sizeDelta.y);
        UpdateUI(_health.CurrentHealth, _health.MaxHealth);
    }
}