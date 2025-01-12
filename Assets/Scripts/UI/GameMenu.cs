using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject _mainFrame;
    [SerializeField] private GameObject _loadingFrame;

    private void Start()
    {
        _inputReader.PauseEvent += OnPause;

        _mainFrame.SetActive(false);
        _loadingFrame.SetActive(false);
    }

    private void OnDestroy()
    {
        _inputReader.PauseEvent -= OnPause;
    }

    private void OnPause()
    {
        bool isActive = _mainFrame.activeInHierarchy;

        if (isActive)
        {
            _mainFrame.SetActive(false);
            _inputReader.SetControllerMode(ControllerMode.Gameplay);
        }
        else
        {
            _mainFrame.SetActive(true);
            _inputReader.SetControllerMode(ControllerMode.UI);
        }
    }

    public void OnClick_Continue()
    {
        _mainFrame.SetActive(false);
        _inputReader.SetControllerMode(ControllerMode.Gameplay);
    }

    public void OnClick_Quit()
    {
        LoadSceneRoutine("SCN_Menu");
        _mainFrame.SetActive(false);
        _loadingFrame.SetActive(true);
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
