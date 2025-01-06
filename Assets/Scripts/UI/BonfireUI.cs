using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonfireUI : MonoBehaviour
{
    public int NextHp
    {
        get => _nextHp; private set
        {
            _nextHp = value;

            _hpText.color = _nextHp > _player.Health.CurrentMaxHealth ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public float NextStamina
    {
        get => _nextStamina; private set
        {
            _nextStamina = value;

            _staminaText.color = _nextStamina > _player.Stamina.CurrentMaxStamina ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public int NextVigor
    {
        get => _nextVigor; private set
        {
            _nextVigor = value;

            _vigorText.color = _nextVigor > _player.CharacterStat.Vigor ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public int NextEndurance
    {
        get => _nextEndurance; private set
        {
            _nextEndurance = value;

            _enduranceText.color = _nextEndurance > _player.CharacterStat.Endurance ?
             _increasedColor :
             _unchangedColor;
        }
    }
    public int NextStrength
    {
        get => _nextStrength; private set
        {
            _nextStrength = value;

            if (_nextStrength > _player.CharacterStat.Strength)
            {
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

    [SerializeField] private GameObject _restFrame;
    [SerializeField] private GameObject _levelUpframe;
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
    private int _nextHp;
    private float _nextStamina;
    private int _nextVigor;
    private int _nextEndurance;
    private int _nextStrength;

    private void Start()
    {
        _player = PlayerStateMachine.Instance;

        _restFrame.SetActive(false);
        _levelUpframe.SetActive(false);
    }

    #region Rest Methods

    public void ShowRestFrame()
    {
        _player.InputReader.SetControllerMode(ControllerMode.UI);
        _restFrame.SetActive(true);
    }

    public void OnClick_Leave()
    {
        HideRestFrame();
    }

    private void HideRestFrame()
    {
        _player.InputReader.SetControllerMode(ControllerMode.Gameplay);
        _player.SwitchState(new PlayerFreeLookState(_player));
        _restFrame.SetActive(false);
    }

    #endregion

    #region Level Up Methods

    public void OnClick_ShowLevelUpFrame()
    {
        // Action
        _nextLevel = _player.CharacterStat.Level;
        NextVigor = _player.CharacterStat.Vigor;
        NextEndurance = _player.CharacterStat.Endurance;
        NextStrength = _player.CharacterStat.Strength;
        _souls = _player.Inventory.Souls;

        _levelText.text = _nextLevel.ToString();
        _soulsHeldText.text = _souls.ToString();
        _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel + 1).ToString();

        _vigorText.text = NextVigor.ToString();
        _enduranceText.text = NextEndurance.ToString();
        _strengthText.text = NextStrength.ToString();

        // Info
        UpdateBaseStats();

        _restFrame.SetActive(false);
        _levelUpframe.SetActive(true);
    }

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

    public void OnClick_IncreaseStrength()
    {
        IncreaseAttribute(ref _nextStrength, _strengthText);
    }

    public void OnClick_DecreaseStrength()
    {
        DecreaseAttribute(ref _nextStrength, _strengthText, _player.CharacterStat.Strength);
    }

    public void OnClick_Confirm()
    {
        _player.SetVigor(NextVigor);
        _player.SetEndurance(NextEndurance);
        _player.SetStrength(NextStrength);

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
        NextStrength = _nextStrength;

        NextHp = _player.Health.GetMaxHealthByVigor(NextVigor);
        NextStamina = _player.Stamina.GetMaxStaminaByEndurance(NextEndurance);

        _hpText.text = NextHp.ToString();
        _staminaText.text = _nextStamina.ToString();
        _weapon1DamageText.text = _player.PrimaryWeapons[0].GetDamageBase(_nextStrength).ToString();
        _weapon2DamageText.text = _player.PrimaryWeapons[1].GetDamageBase(_nextStrength).ToString();
        _weapon3DamageText.text = _player.PrimaryWeapons[2].GetDamageBase(_nextStrength).ToString();
    }

    private void HideLevelUpFrame()
    {
        _levelUpframe.SetActive(false);
        _restFrame.SetActive(true);
    }

    #endregion
}
