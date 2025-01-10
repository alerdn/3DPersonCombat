using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _frame;
    [SerializeField] private InputReader _inputReader;

    private void Start()
    {
        _inputReader.PauseEvent += OnPause;

        _frame.SetActive(false);
    }

    private void OnDestroy()
    {
        _inputReader.PauseEvent -= OnPause;
    }

    private void OnPause()
    {
        _frame.SetActive(!_frame.activeInHierarchy);
        _inputReader.SetControllerMode(ControllerMode.UI);
    }

    public void OnClick_Continue()
    {
        _frame.SetActive(false);
        _inputReader.SetControllerMode(ControllerMode.Gameplay);
    }

    public void OnClick_Quit()
    {
        SceneManager.LoadScene("SCN_Menu");
    }
}
