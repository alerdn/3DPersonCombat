using TMPro;
using UnityEngine;

public class BonfireLevelUpUI : MonoBehaviour
{
    public int NextHp
    {
        get => _nextHp;
        private set
        {
            _nextHp = value;

            _hpText.color = _nextHp > _player.Health.CurrentMaxHealth ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public float NextStamina
    {
        get => _nextStamina;
        private set
        {
            _nextStamina = value;

            _staminaText.color = _nextStamina > _player.Stamina.CurrentMaxStamina ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public float NextMana
    {
        get => _nextMana;
        private set
        {
            _nextMana = value;

            _manaText.color = _nextMana > _player.Mana.CurrentMaxMana ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public int NextVigor
    {
        get => _nextVigor;
        private set
        {
            _nextVigor = value;

            _vigorText.color = _nextVigor > _player.CharacterStat.Vigor ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public int NextEndurance
    {
        get => _nextEndurance;
        private set
        {
            _nextEndurance = value;

            _enduranceText.color = _nextEndurance > _player.CharacterStat.Endurance ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public int NextMind
    {
        get => _nextMind;
        private set
        {
            _nextMind = value;

            _mindText.color = _nextMind > _player.CharacterStat.Mind ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public int NextStrength
    {
        get => _nextStrength;
        private set
        {
            _nextStrength = value;

            if (_nextStrength > _player.CharacterStat.Strength)
            {
                _strengthText.color = _increasedColor;
                _weapon1DamageText.color = _increasedColor;
                _weapon2DamageText.color = _increasedColor;
                _weapon3DamageText.color = _increasedColor;
            }
            else
            {
                _strengthText.color = _unchangedColor;
                _weapon1DamageText.color = _unchangedColor;
                _weapon2DamageText.color = _unchangedColor;
                _weapon3DamageText.color = _unchangedColor;
            }
        }
    }
    public int NextIntelligence
    {
        get => _nextIntelligence;
        private set
        {
            _nextIntelligence = value;

            if (_nextIntelligence > _player.CharacterStat.Intelligence)
            {
                _intelligenceText.color = _increasedColor;
                _weapon4DamageText.color = _increasedColor;
            }
            else
            {
                _intelligenceText.color = _unchangedColor;
                _weapon4DamageText.color = _unchangedColor;
            }
        }

    }

    [SerializeField] private GameObject _frame;
    [SerializeField] private Color _unchangedColor;
    [SerializeField] private Color _increasedColor;

    [Header("Action")]
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _soulsHeldText;
    [SerializeField] private TMP_Text _soulsRequiredText;
    [SerializeField] private TMP_Text _vigorText;
    [SerializeField] private TMP_Text _enduranceText;
    [SerializeField] private TMP_Text _mindText;
    [SerializeField] private TMP_Text _strengthText;
    [SerializeField] private TMP_Text _intelligenceText;

    [Header("Info")]
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _manaText;
    [SerializeField] private TMP_Text _staminaText;
    [SerializeField] private TMP_Text _weapon1DamageText;
    [SerializeField] private TMP_Text _weapon2DamageText;
    [SerializeField] private TMP_Text _weapon3DamageText;
    [SerializeField] private TMP_Text _weapon4DamageText;

    PlayerStateMachine _player;
    private int _souls;

    private int _nextLevel;
    private int _nextHp;
    private float _nextMana;
    private float _nextStamina;
    private int _nextVigor;
    private int _nextEndurance;
    private int _nextMind;
    private int _nextStrength;
    private int _nextIntelligence;

    private BonfireUI _bonfireUI;

    public void Init(PlayerStateMachine player, BonfireUI bonfireUI)
    {
        _player = player;
        _bonfireUI = bonfireUI;
    }

    public void ShowLevelUpFrame()
    {
        // Action
        _nextLevel = _player.CharacterStat.Level;
        NextVigor = _player.CharacterStat.Vigor;
        NextEndurance = _player.CharacterStat.Endurance;
        NextMind = _player.CharacterStat.Mind;
        NextStrength = _player.CharacterStat.Strength;
        NextIntelligence = _player.CharacterStat.Intelligence;
        _souls = _player.Inventory.Souls;

        _levelText.text = _nextLevel.ToString();
        _soulsHeldText.text = _souls.ToString();
        _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel + 1).ToString();

        _vigorText.text = NextVigor.ToString();
        _enduranceText.text = NextEndurance.ToString();
        _mindText.text = NextMind.ToString();
        _strengthText.text = NextStrength.ToString();
        _intelligenceText.text = NextIntelligence.ToString();

        // Info
        UpdateBaseStats();

        _frame.SetActive(true);
    }

    #region Increase / Decrease Methods

    public void OnClick_IncreaseVigor()
    {
        IncreaseAttribute(ref _nextVigor, _vigorText);
    }

    public void OnClick_DecreaseVigor()
    {
        DecreaseAttribute(ref _nextVigor, _vigorText, _player.CharacterStat.Vigor);
    }

    public void OnClick_IncreaseEndurance()
    {
        IncreaseAttribute(ref _nextEndurance, _enduranceText);
    }

    public void OnClick_DecreaseEndurance()
    {
        DecreaseAttribute(ref _nextEndurance, _enduranceText, _player.CharacterStat.Endurance);
    }

    public void OnClick_IncreaseMind()
    {
        IncreaseAttribute(ref _nextMind, _mindText);
    }

    public void OnClick_DecreaseMind()
    {
        DecreaseAttribute(ref _nextMind, _mindText, _player.CharacterStat.Mind);
    }

    public void OnClick_IncreaseStrength()
    {
        IncreaseAttribute(ref _nextStrength, _strengthText);
    }

    public void OnClick_DecreaseStrength()
    {
        DecreaseAttribute(ref _nextStrength, _strengthText, _player.CharacterStat.Strength);
    }

    public void OnClick_IncreaseIntelligence()
    {
        IncreaseAttribute(ref _nextIntelligence, _intelligenceText);
    }

    public void OnClick_DecreaseIntelligence()
    {
        DecreaseAttribute(ref _nextIntelligence, _intelligenceText, _player.CharacterStat.Intelligence);
    }

    #endregion

    public void OnClick_Confirm()
    {
        _player.SetVigor(NextVigor);
        _player.SetEndurance(NextEndurance);
        _player.SetMind(NextMind);
        _player.SetStrength(NextStrength);
        _player.SetIntelligence(NextIntelligence);

        _player.Inventory.Souls = _souls;

        HideLevelUpFrame();
    }

    public void OnClick_Cancel()
    {
        HideLevelUpFrame();
    }

    private void IncreaseAttribute(ref int attribute, TMP_Text attributeText)
    {
        if (attribute == 50) return;

        if (_player.CharacterStat.GetSoulsToNextLevel(_nextLevel + 1) > _souls) return;

        attribute++;
        _nextLevel++;
        _souls -= _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);

        _levelText.text = _nextLevel.ToString();
        attributeText.text = attribute.ToString();
        _soulsHeldText.text = _souls.ToString();
        _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel + 1).ToString();

        UpdateBaseStats();
    }

    private void DecreaseAttribute(ref int attribute, TMP_Text attributeText, int playerAttribute)
    {
        if (attribute <= playerAttribute) return;

        attribute--;
        _souls += _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);
        _nextLevel--;

        _levelText.text = _nextLevel.ToString();
        attributeText.text = attribute.ToString();
        _soulsHeldText.text = _souls.ToString();
        _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel + 1).ToString();

        UpdateBaseStats();
    }

    private void UpdateBaseStats()
    {
        NextVigor = _nextVigor;
        NextEndurance = _nextEndurance;
        NextMind = _nextMind;
        NextStrength = _nextStrength;
        NextIntelligence = _nextIntelligence;

        NextHp = _player.Health.GetMaxHealthByVigor(NextVigor);
        NextMana = _player.Mana.GetMaxManaByMind(NextMind);
        NextStamina = _player.Stamina.GetMaxStaminaByEndurance(NextEndurance);

        _hpText.text = NextHp.ToString();
        _manaText.text = _nextMana.ToString();
        _staminaText.text = _nextStamina.ToString();
        _weapon1DamageText.text = GetWeaponDamageText(_player.Weapons[0]);
        _weapon2DamageText.text = GetWeaponDamageText(_player.Weapons[1]);
        _weapon3DamageText.text = GetWeaponDamageText(_player.Weapons[2]);
        _weapon4DamageText.text = GetWeaponDamageText(_player.Weapons[3]);
    }

    private void HideLevelUpFrame()
    {
        _bonfireUI.ShowMainFrame();
        HideFrame();
    }

    public void HideFrame()
    {
        _frame.SetActive(false);
    }

    private string GetWeaponDamageText(Weapon weapon)
    {
        if (!weapon) return "-";

        return weapon.DamageAttribute switch
        {
            DamageAttribute.Strength => weapon.GetDamageBase(NextStrength).ToString(),
            DamageAttribute.Intelligence => weapon.GetDamageBase(NextIntelligence).ToString(),
            _ => "-"
        };
    }
}