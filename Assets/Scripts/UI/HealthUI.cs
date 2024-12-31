using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private Health _health;

    private void Start()
    {
        _health = PlayerStateMachine.Instance.Health;

        _health.OnHealthChanged += UpdateUI;
    }

    private void UpdateUI(int health, int maxHealth)
    {
        _healthBar.fillAmount = (float)health / (float)maxHealth;
    }
}