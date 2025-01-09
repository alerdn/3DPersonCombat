using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private Image _manaBar;

    private PlayerStateMachine _player;
    private Mana _mana;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _player = PlayerStateMachine.Instance;
        _mana = _player.Mana;

        _mana.OnManaChanged += UpdateUI;
        _player.Mana.OnMaxManaChanged += UpdateManaBarSize;

        UpdateManaBarSize(_player.Mana.CurrentMaxMana);
    }

    private void OnDestroy()
    {
        _mana.OnManaChanged -= UpdateUI;
        _player.Mana.OnMaxManaChanged -= UpdateManaBarSize;
    }

    private void UpdateUI(float mana, float maxMana)
    {
        _manaBar.fillAmount = mana / maxMana;
    }

    private void UpdateManaBarSize(float maxMana)
    {
        _rectTransform.sizeDelta = new Vector2(maxMana, _rectTransform.sizeDelta.y);
        UpdateUI(_mana.CurrentMana, _mana.CurrentMaxMana);
    }
}