using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthBar;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (_health == null)
        {
            PlayerStateMachine player = PlayerStateMachine.Instance;
            _health = player.Health;

            player.Stat.OnVigorChanged += (vigor) =>
            {
                _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x + vigor, _rectTransform.sizeDelta.y);
                UpdateUI(_health.CurrentHealth, _health.MaxHealth);
            };
        }

        _health.OnHealthChanged += UpdateUI;

    }

    private void UpdateUI(int health, int maxHealth)
    {
        _healthBar.fillAmount = (float)health / (float)maxHealth;
    }
}