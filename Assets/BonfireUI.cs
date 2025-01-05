using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonfireUI : MonoBehaviour
{
    [SerializeField] private GameObject _frame;
    [SerializeField] private Color _unchangedColor;
    [SerializeField] private Color _increasedColor;


    [Header("Action")]
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _soulsHeldText;
    [SerializeField] private TMP_Text _soulsRequiredText;
    [SerializeField] private TMP_Text _vigorText;
    [SerializeField] private TMP_Text _enduranceText;
    [SerializeField] private TMP_Text _strengthText;

    [Header("Info")]
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _staminaText;
    [SerializeField] private TMP_Text _weapon1DamageText;
    [SerializeField] private TMP_Text _weapon2DamageText;
    [SerializeField] private TMP_Text _weapon3DamageText;

    PlayerStateMachine _player;
    private int _souls;

    private int _nextLevel;
    private int _nextVigor;
    private int _nextEndurance;
    private int _nextStrength;

    private int _nextHp;
    private float _nextStamina;

    private void Start()
    {
        _player = PlayerStateMachine.Instance;

        _frame.SetActive(false);
    }

    public void Show()
    {
        _player.InputReader.SetControllerMode(ControllerMode.UI);

        // Action
        _nextLevel = _player.CharacterStat.Level;
        _nextVigor = _player.CharacterStat.Vigor;
        _nextEndurance = _player.CharacterStat.Endurance;
        _nextStrength = _player.CharacterStat.Strength;
        _souls = _player.Inventory.Souls;

        _levelText.text = _player.CharacterStat.Level.ToString();
        _soulsHeldText.text = _player.Inventory.Souls.ToString();
        _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel + 1).ToString();

        _vigorText.text = _player.CharacterStat.Vigor.ToString();
        _enduranceText.text = _player.CharacterStat.Endurance.ToString();
        _strengthText.text = _player.CharacterStat.Strength.ToString();

        // Info
        UpdateBaseStats();

        _frame.SetActive(true);
    }

    public void OnClick_IncreaseVigor()
    {
        IncreaseAttribute(ref _nextVigor, ref _vigorText);
    }

    public void OnClick_DecreaseVigor()
    {
        DecreaseAttribute(ref _nextVigor, ref _vigorText, _player.CharacterStat.Vigor);
    }

    public void OnClick_IncreaseEndurance()
    {
        IncreaseAttribute(ref _nextEndurance, ref _enduranceText);
    }

    public void OnClick_DecreaseEndurance()
    {
        DecreaseAttribute(ref _nextEndurance, ref _enduranceText, _player.CharacterStat.Endurance);
    }

    public void OnClick_IncreaseStrength()
    {
        IncreaseAttribute(ref _nextStrength, ref _strengthText);
    }

    public void OnClick_DecreaseStrength()
    {
        DecreaseAttribute(ref _nextStrength, ref _strengthText, _player.CharacterStat.Strength);
    }

    public void OnClick_Confirm()
    {
        _player.SetVigor(_nextVigor);
        _player.SetEndurance(_nextEndurance);
        _player.SetStrength(_nextStrength);

        _player.Inventory.Souls = _souls;

        Hide();
    }

    public void OnClick_Cancel()
    {
        Hide();
    }

    private void IncreaseAttribute(ref int attribute, ref TMP_Text attributeText)
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

    private void DecreaseAttribute(ref int attribute, ref TMP_Text attributeText, int playerAttribute)
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
        _nextHp = _player.Health.InitialMaxHealth * _nextVigor * _player.Health.VigorMultiplier;
        _nextStamina = _player.Stamina.InitialMaxStamina * _nextEndurance;

        _hpText.text = _nextHp.ToString();
        _staminaText.text = _nextStamina.ToString();
        _weapon1DamageText.text = _player.PrimaryWeapons[0].GetDamageBase(_nextStrength).ToString();
        _weapon2DamageText.text = _player.PrimaryWeapons[1].GetDamageBase(_nextStrength).ToString();
        _weapon3DamageText.text = _player.PrimaryWeapons[2].GetDamageBase(_nextStrength).ToString();

        if (_nextHp > _player.Health.MaxHealth)
        {
            _hpText.color = _increasedColor;
        }
        else
        {
            _hpText.color = _unchangedColor;
        }

        if (_nextStamina > _player.Stamina.MaxStamina)
        {
            _staminaText.color = _increasedColor;
        }
        else
        {
            _staminaText.color = _unchangedColor;
        }

        if (_nextVigor > _player.CharacterStat.Vigor)
        {
            _vigorText.color = _increasedColor;
        }
        else
        {
            _vigorText.color = _unchangedColor;
        }

        if (_nextEndurance > _player.CharacterStat.Endurance)
        {
            _enduranceText.color = _increasedColor;
        }
        else
        {
            _enduranceText.color = _unchangedColor;
        }

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

    private void Hide()
    {
        _player.InputReader.SetControllerMode(ControllerMode.Gameplay);
        _player.SwitchState(new PlayerFreeLookState(_player));
        _frame.SetActive(false);
    }
}
