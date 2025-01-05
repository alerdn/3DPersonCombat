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
    }

    private void Hide()
    {
        _player.InputReader.SetControllerMode(ControllerMode.Gameplay);
        _frame.SetActive(false);
    }
}
