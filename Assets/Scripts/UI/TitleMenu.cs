using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainFrame;
    [SerializeField] private GameObject _loadingFrame;


    private void Start()
    {
        _mainFrame.SetActive(true);
        _loadingFrame.SetActive(false);
    }

    public void OnClick_Start()
    {
        StartCoroutine(LoadSceneRoutine("Sandbox_New"));
        _mainFrame.SetActive(false);
        _loadingFrame.SetActive(true);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
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
