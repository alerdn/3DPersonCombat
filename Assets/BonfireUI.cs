using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonfireUI : MonoBehaviour
{
    [SerializeField] private GameObject _frame;

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _soulsHeldText;
    [SerializeField] private TMP_Text _soulsRequiredText;
    [SerializeField] private TMP_Text _vigorText;
    [SerializeField] private TMP_Text _enduranceText;
    [SerializeField] private TMP_Text _strengthText;

    PlayerStateMachine _player;
    private int _souls;

    private int _nextLevel;
    private int _nextVigor;
    private int _nextEndurance;
    private int _nextStrength;

    private void Start()
    {
        _player = PlayerStateMachine.Instance;

        _frame.SetActive(false);
    }

    public void Show()
    {
        _player.InputReader.SetControllerMode(ControllerMode.UI);
        _nextLevel = _player.CharacterStat.Level + 1;
        _nextVigor = _player.CharacterStat.Vigor;
        _nextEndurance = _player.CharacterStat.Endurance;
        _nextStrength = _player.CharacterStat.Strength;
        _souls = _player.Inventory.Souls;

        _levelText.text = _player.CharacterStat.Level.ToString();
        _soulsHeldText.text = _player.Inventory.Souls.ToString();
        _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel).ToString();

        _vigorText.text = _player.CharacterStat.Vigor.ToString();
        _enduranceText.text = _player.CharacterStat.Endurance.ToString();
        _strengthText.text = _player.CharacterStat.Strength.ToString();

        _frame.SetActive(true);
    }

    public void OnClick_IncreaseVigor()
    {
        if (_nextVigor == 50) return;

        if (_player.CharacterStat.GetSoulsToNextLevel(_nextLevel) <= _souls)
        {
            _nextVigor++;
            _nextLevel++;
            _souls -= _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);

            _levelText.text = _nextLevel.ToString();
            _vigorText.text = _nextVigor.ToString();
            _soulsHeldText.text = _souls.ToString();
            _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel).ToString();
        }
    }

    public void OnClick_DecreaseVigor()
    {
        if (_nextVigor > _player.CharacterStat.Vigor)
        {
            _nextVigor--;
            _nextLevel--;
            _souls += _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);

            _levelText.text = _nextLevel.ToString();
            _vigorText.text = _nextVigor.ToString();
            _soulsHeldText.text = _souls.ToString();
            _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel).ToString();
        }
    }

    public void OnClick_IncreaseEndurance()
    {
        if (_nextEndurance == 50) return;

        if (_player.CharacterStat.GetSoulsToNextLevel(_nextLevel) <= _souls)
        {
            _nextEndurance++;
            _nextLevel++;
            _souls -= _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);

            _levelText.text = _nextLevel.ToString();
            _enduranceText.text = _nextEndurance.ToString();
            _soulsHeldText.text = _souls.ToString();
            _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel).ToString();
        }
    }

    public void OnClick_DecreaseEndurance()
    {
        if (_nextEndurance > _player.CharacterStat.Endurance)
        {
            _nextEndurance--;
            _nextLevel--;
            _souls += _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);

            _levelText.text = _nextLevel.ToString();
            _enduranceText.text = _nextEndurance.ToString();
            _soulsHeldText.text = _souls.ToString();
            _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel).ToString();
        }
    }

    public void OnClick_IncreaseStrength()
    {
        if (_nextStrength == 50) return;

        if (_player.CharacterStat.GetSoulsToNextLevel(_nextLevel) <= _souls)
        {
            _nextStrength++;
            _nextLevel++;
            _souls -= _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);

            _levelText.text = _nextLevel.ToString();
            _strengthText.text = _nextStrength.ToString();
            _soulsHeldText.text = _souls.ToString();
            _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel).ToString();
        }
    }

    public void OnClick_DecreaseStrength()
    {
        if (_nextStrength > _player.CharacterStat.Strength)
        {
            _nextStrength--;
            _nextLevel--;
            _souls += _player.CharacterStat.GetSoulsToNextLevel(_nextLevel);

            _levelText.text = _nextLevel.ToString();
            _strengthText.text = _nextStrength.ToString();
            _soulsHeldText.text = _souls.ToString();
            _soulsRequiredText.text = _player.CharacterStat.GetSoulsToNextLevel(_nextLevel).ToString();
        }
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

    private void Hide()
    {
        _player.InputReader.SetControllerMode(ControllerMode.Gameplay);
        _frame.SetActive(false);
    }
}
